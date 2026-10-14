using ItauChallenge.Application.Services;
using ItauChallenge.Domain.Models;
using ItauChallenge.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ItauChallenge.Application.Tests;

public class OperationServiceTests
{
    [Fact]
    public void CalculateWeightedAveragePrice_ReturnsExpectedValue_ForBuyOperations()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ItauChallengeContext>()
            .UseInMemoryDatabase(databaseName: "CalculateWeightedAveragePrice_TestDB")
            .Options;

        using var context = new ItauChallengeContext(options);
        // Asset id to test
        var assetId = 1;

        // Create buy operations for the same asset
        context.Operacoes.Add(new Operacoes { AtivoId = assetId, QuantidadeB3 = 100, PrecoUnit = 30.0m, TipoOperacao = OperationType.Buy });
        context.Operacoes.Add(new Operacoes { AtivoId = assetId, QuantidadeB3 = 50, PrecoUnit = 32.0m, TipoOperacao = OperationType.Buy });
        context.Operacoes.Add(new Operacoes { AtivoId = assetId, QuantidadeB3 = 150, PrecoUnit = 28.5m, TipoOperacao = OperationType.Buy });

        // Add a sell operation that should be ignored
       // context.Operacoes.Add(new Operacoes { AtivoId = assetId, QuantidadeB3 = 10, PrecoUnit = 1.0m, TipoOperacao = OperationType.Sell });

        context.SaveChanges();

        var service = new OperationService(context);

        // Act
        var result = service.CalculateWeightedAveragePrice(assetId);

        // Assert
        var expected = (100m * 30.0m + 50m * 32.0m + 150m * 28.5m) / (100m + 50m + 150m);
        Assert.Equal(expected, result);
    }
}
