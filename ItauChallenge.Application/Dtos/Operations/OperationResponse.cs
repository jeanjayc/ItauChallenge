using ItauChallenge.Application.Dtos.Shared;
using ItauChallenge.Domain.Models;

namespace ItauChallenge.Application.Dtos.Operations;

public class OperationResponse
{
    public int Id { get; set; }
    public int? UsuarioId { get; set; }
    public int? AtivoId { get; set; }
    public int? QuantidadeB3 { get; set; }
    public decimal? QuantidadeGlobal { get; set; }
    public decimal? PrecoUnit { get; set; }
    public OperationType TipoOperacao { get; set; }
    public decimal? Corretagem { get; set; }
    public DateTime? DataHora { get; set; }
    public DateTime? CreatedAt { get; set; }
    public AtivoSummaryResponse? Ativo { get; set; }
    public InvestorSummaryResponse? Usuario { get; set; }
}
