using ItauChallenge.Application.Dtos.Shared;

namespace ItauChallenge.Application.Dtos.Quotes;

public class QuoteResponse
{
    public int Id { get; set; }
    public int? AtivoId { get; set; }
    public decimal? PrecoUnit { get; set; }
    public DateTime? DataHora { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool? IsActive { get; set; }
    public AtivoSummaryResponse? Ativo { get; set; }
}
