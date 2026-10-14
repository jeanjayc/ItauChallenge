using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ItauChallenge.Messaging;

public class KafkaConsumer : IKafkaConsumer
{
    private readonly KafkaOptions _options;
    private readonly ILogger<KafkaConsumer> _logger;

    public KafkaConsumer(IOptions<KafkaOptions> options, ILogger<KafkaConsumer> logger)
    {
        _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task ConsumeAsync(
        string topic,
        Func<string, CancellationToken, Task> messageHandler,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(topic);
        ArgumentNullException.ThrowIfNull(messageHandler);

        return Task.Run(() => ConsumeMessagesAsync(topic, messageHandler, cancellationToken), cancellationToken);
    }

    private async Task ConsumeMessagesAsync(
        string topic,
        Func<string, CancellationToken, Task> messageHandler,
        CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _options.BootstrapServers,
            GroupId = _options.ConsumerGroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(topic);

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var result = consumer.Consume(cancellationToken);
                await messageHandler(result.Message.Value, cancellationToken).ConfigureAwait(false);
                consumer.Commit(result);
                _logger.LogDebug("Committed Kafka offset {Offset} for {Topic}", result.Offset, result.Topic);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
        finally
        {
            consumer.Close();
        }
    }
}
