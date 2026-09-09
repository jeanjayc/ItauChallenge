using ItauChallenge.Application.Dtos.Positions;
using ItauChallenge.Domain.Models;

namespace ItauChallenge.Application.Mappings;

public static class PositionMapper
{
    public static Posicao ToEntity(CreatePositionRequest request)
    {
        return new Posicao
        {
            AtivoId = request.AtivoId,
            UsuarioId = request.UsuarioId,
            QuantidadeB3 = request.QuantidadeB3,
            QuantidadeGlobal = request.QuantidadeGlobal,
            PrecoMedio = request.PrecoMedio,
            Pl = request.Pl,
            UpdateAt = DateTime.UtcNow
        };
    }

    public static void ApplyUpdate(Posicao entity, UpdatePositionRequest request)
    {
        entity.AtivoId = request.AtivoId;
        entity.UsuarioId = request.UsuarioId;
        entity.QuantidadeB3 = request.QuantidadeB3;
        entity.QuantidadeGlobal = request.QuantidadeGlobal;
        entity.PrecoMedio = request.PrecoMedio;
        entity.Pl = request.Pl;
        entity.UpdateAt = DateTime.UtcNow;
    }

    public static PositionResponse ToResponse(Posicao entity)
    {
        return new PositionResponse
        {
            Id = entity.Id,
            AtivoId = entity.AtivoId,
            UsuarioId = entity.UsuarioId,
            QuantidadeB3 = entity.QuantidadeB3,
            QuantidadeGlobal = entity.QuantidadeGlobal,
            PrecoMedio = entity.PrecoMedio,
            Pl = entity.Pl,
            UpdateAt = entity.UpdateAt,
            Ativo = SummaryMapper.ToAtivoSummary(entity.Ativo),
            Usuario = SummaryMapper.ToInvestorSummary(entity.Usuario)
        };
    }
}
