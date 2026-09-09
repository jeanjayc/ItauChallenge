using ItauChallenge.Application.Dtos.Shared;

namespace ItauChallenge.Application.Dtos.Positions;

public class PositionResponse
{
    public int Id { get; set; }
    public int? AtivoId { get; set; }
    public int? UsuarioId { get; set; }
    public int? QuantidadeB3 { get; set; }
    public decimal? QuantidadeGlobal { get; set; }
    public decimal? PrecoMedio { get; set; }
    public decimal? Pl { get; set; }
    public DateTime? UpdateAt { get; set; }
    public AtivoSummaryResponse? Ativo { get; set; }
    public InvestorSummaryResponse? Usuario { get; set; }
}
