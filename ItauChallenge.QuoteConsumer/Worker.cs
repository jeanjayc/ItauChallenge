using System.Text.Json;
using ItauChallenge.Application.Dtos.Quotes;
using ItauChallenge.Application.Services;
using ItauChallenge.Messaging;
using Microsoft.Extensions.Options;

namespace ItauChallenge.QuoteConsumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IKafkaConsumer _consumer;
        private readonly KafkaOptions _kafkaOptions;

        public Worker(
            ILogger<Worker> logger,
            IServiceProvider serviceProvider,
            IKafkaConsumer consumer,
            IOptions<KafkaOptions> kafkaOptions)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _consumer = consumer ?? throw new ArgumentNullException(nameof(consumer));
            _kafkaOptions = kafkaOptions?.Value ?? throw new ArgumentNullException(nameof(kafkaOptions));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Starting Kafka consumer for topic {Topic}", _kafkaOptions.Topic);
                    await _consumer.ConsumeAsync(_kafkaOptions.Topic, ProcessMessageAsync, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while consuming messages. Retrying in 5 seconds...");
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }

        private async Task ProcessMessageAsync(string payload, CancellationToken cancellationToken)
        {
            QuotePriceUpdate? message;
            try
            {
                message = JsonSerializer.Deserialize<QuotePriceUpdate>(payload);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Skipping invalid quote update message");
                return;
            }

            if (message is null || message.QuoteId <= 0 || message.NewPrice <= 0 || message.Timestamp == default)
            {
                _logger.LogWarning("Skipping invalid quote update message: {Payload}", payload);
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            var quoteService = scope.ServiceProvider.GetRequiredService<IQuoteService>();
            var currentQuote = await quoteService.GetByIdAsync(message.QuoteId);

            if (currentQuote is null)
            {
                _logger.LogWarning("Quote {QuoteId} was not found; ignoring update", message.QuoteId);
                return;
            }

            var updated = await quoteService.UpdateAsync(message.QuoteId, new UpdateQuoteRequest
            {
                AtivoId = currentQuote.AtivoId,
                PrecoUnit = message.NewPrice,
                DataHora = message.Timestamp.UtcDateTime,
                IsActive = currentQuote.IsActive
            });

            if (!updated)
            {
                throw new InvalidOperationException($"Quote {message.QuoteId} could not be updated.");
            }

            _logger.LogInformation("Updated quote {QuoteId} price from {OldPrice} to {NewPrice}",
                message.QuoteId, message.OldPrice, message.NewPrice);
        }
    }
}
