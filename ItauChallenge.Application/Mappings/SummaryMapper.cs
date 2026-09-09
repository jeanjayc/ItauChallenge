using ItauChallenge.Application.Dtos.Shared;
using ItauChallenge.Domain.Models;

namespace ItauChallenge.Application.Mappings;

public static class SummaryMapper
{
    public static AtivoSummaryResponse? ToAtivoSummary(Ativo? ativo)
    {
        if (ativo == null) return null;

        return new AtivoSummaryResponse
        {
            Id = ativo.Id,
            Codigo = ativo.Codigo,
            Nome = ativo.Nome
        };
    }

    public static InvestorSummaryResponse? ToInvestorSummary(Usuario? usuario)
    {
        if (usuario == null) return null;

        return new InvestorSummaryResponse
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email
        };
    }
}
