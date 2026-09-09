using ItauChallenge.Application.Dtos.Operations;
using ItauChallenge.Domain.Models;

namespace ItauChallenge.Application.Mappings;

public static class OperationMapper
{
    public static Operacoes ToEntity(CreateOperationRequest request)
    {
        return new Operacoes
        {
            UsuarioId = request.UsuarioId,
            AtivoId = request.AtivoId,
            QuantidadeB3 = request.QuantidadeB3,
            QuantidadeGlobal = request.QuantidadeGlobal,
            PrecoUnit = request.PrecoUnit,
            TipoOperacao = request.TipoOperacao,
            Corretagem = request.Corretagem,
            DataHora = request.DataHora ?? DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static void ApplyUpdate(Operacoes entity, UpdateOperationRequest request)
    {
        entity.UsuarioId = request.UsuarioId;
        entity.AtivoId = request.AtivoId;
        entity.QuantidadeB3 = request.QuantidadeB3;
        entity.QuantidadeGlobal = request.QuantidadeGlobal;
        entity.PrecoUnit = request.PrecoUnit;
        entity.TipoOperacao = request.TipoOperacao;
        entity.Corretagem = request.Corretagem;
        entity.DataHora = request.DataHora;
    }

    public static OperationResponse ToResponse(Operacoes entity)
    {
        return new OperationResponse
        {
            Id = entity.Id,
            UsuarioId = entity.UsuarioId,
            AtivoId = entity.AtivoId,
            QuantidadeB3 = entity.QuantidadeB3,
            QuantidadeGlobal = entity.QuantidadeGlobal,
            PrecoUnit = entity.PrecoUnit,
            TipoOperacao = entity.TipoOperacao,
            Corretagem = entity.Corretagem,
            DataHora = entity.DataHora,
            CreatedAt = entity.CreatedAt,
            Ativo = SummaryMapper.ToAtivoSummary(entity.Ativo),
            Usuario = SummaryMapper.ToInvestorSummary(entity.Usuario)
        };
    }
}
