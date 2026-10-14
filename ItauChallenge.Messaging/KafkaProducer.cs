using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ItauChallenge.Messaging;

public class KafkaProducer : IKafkaProducer, IDisposable
{
    private readonly IProducer<Null, string> _producer;
    private readonly ILogger<KafkaProducer> _logger;

    public KafkaProducer(IOptions<KafkaOptions> options, ILogger<KafkaProducer> logger)
    {
        var kafkaOptions = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        var config = new ProducerConfig
        {
            BootstrapServers = kafkaOptions.BootstrapServers
        };

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task ProduceAsync<T>(string topic, T message, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(topic);

        var payload = JsonSerializer.Serialize(message);

        try
        {
            var msg = new Message<Null, string> { Value = payload };
            var delivery = await _producer.ProduceAsync(topic, msg, cancellationToken).ConfigureAwait(false);
            _logger.LogDebug("Message delivered to {TopicPartitionOffset}", delivery.TopicPartitionOffset);
        }
        catch (ProduceException<Null, string> ex)
        {
            _logger.LogError(ex, "Failed to produce message to Kafka topic {Topic}", topic);
            throw;
        }
    }

    public void Dispose()
    {
        try
        {
            _producer.Flush(TimeSpan.FromSeconds(5));
            _producer.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Exception while disposing Kafka producer");
        }
    }
}
