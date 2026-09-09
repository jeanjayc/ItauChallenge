namespace ItauChallenge.Domain.Models;

public partial class Cotacao
{
    public int Id { get; set; }

    public int? AtivoId { get; set; }

    public decimal? PrecoUnit { get; set; }

    public DateTime? DataHora { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual Ativo? Ativo { get; set; }
}
