using ItauChallenge.Application.Dtos.Quotes;
using ItauChallenge.Application.Services;
using ItauChallenge.Messaging;
using Microsoft.Extensions.Options;

namespace ItauChallenge.QuotePublish
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IKafkaProducer _producer;
        private readonly KafkaOptions _kafkaOptions;

        private static readonly Random _random = new Random();

        public Worker(
            ILogger<Worker> logger,
            IServiceProvider serviceProvider,
            IKafkaProducer producer,
            IOptions<KafkaOptions> kafkaOptions)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
            _kafkaOptions = kafkaOptions?.Value ?? throw new ArgumentNullException(nameof(kafkaOptions));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (_logger.IsEnabled(LogLevel.Information))
                    {
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    }

                    // Create a scope to resolve scoped services (DbContext, repositories, etc.)
                    using var scope = _serviceProvider.CreateScope();
                    var quoteService = scope.ServiceProvider.GetRequiredService<IQuoteService>();

                    // Fetch all quotesqsfn 
                    var quotes = await quoteService.GetAllAsync();

                    foreach (var q in quotes)
                    {
                        var novoPreco = GerarNovoPreco(q.PrecoUnit, 28.00m, 35.00m);
                        if (stoppingToken.IsCancellationRequested) break;

                        // Prepare a lightweight message with the updated price
                        var message = new QuotePriceUpdate
                        {
                            QuoteId = q.Id,
                            AtivoId = q.AtivoId,
                            OldPrice = q.PrecoUnit,
                            NewPrice = novoPreco,
                            Timestamp = DateTimeOffset.UtcNow
                        };

                        try
                        {
                            await _producer.ProduceAsync(_kafkaOptions.Topic, message, stoppingToken);
                            _logger.LogInformation("Published updated price for Quote {QuoteId}", q.Id);
                        }
                        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                        {
                            // shutdown
                            break;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Failed to publish Kafka message for Quote {QuoteId}", q.Id);
                        }
                    }
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    // graceful shutdown
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing quotes");
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

        private decimal GerarNovoPreco(decimal precoAntigo, decimal valorMinimo, decimal valorMaximo)
        {
            var novoPreco = precoAntigo + (decimal)(_random.NextDouble() - 0.5) * 0.02m;

            if (novoPreco < valorMinimo)
            {
                novoPreco = valorMinimo;

            }
            else if (novoPreco > valorMaximo)
            {
                novoPreco = valorMaximo;
            }

            return Math.Round(novoPreco, 2);
        }
    }
}
