using ItauChallenge.Application.Dtos.Quotes;
using ItauChallenge.Domain.Models;

namespace ItauChallenge.Application.Mappings;

public static class QuoteMapper
{
    public static Cotacao ToEntity(CreateQuoteRequest request)
    {
        return new Cotacao
        {
            AtivoId = request.AtivoId,
            PrecoUnit = request.PrecoUnit,
            DataHora = request.DataHora,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static void ApplyUpdate(Cotacao entity, UpdateQuoteRequest request)
    {
        entity.AtivoId = request.AtivoId;
        entity.PrecoUnit = request.PrecoUnit;
        entity.DataHora = request.DataHora;
        entity.IsActive = request.IsActive;
    }

    public static QuoteResponse ToResponse(Cotacao entity)
    {
        return new QuoteResponse
        {
            Id = entity.Id,
            AtivoId = entity.AtivoId,
            PrecoUnit = entity.PrecoUnit,
            DataHora = entity.DataHora,
            CreatedAt = entity.CreatedAt,
            IsActive = entity.IsActive,
            Ativo = SummaryMapper.ToAtivoSummary(entity.Ativo)
        };
    }
}
