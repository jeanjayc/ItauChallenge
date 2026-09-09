namespace ItauChallenge.Domain.Models;

public partial class Posicao
{
    public int Id { get; set; }

    public int? AtivoId { get; set; }

    public int? UsuarioId { get; set; }

    public int? QuantidadeB3 { get; set; }

    public decimal? QuantidadeGlobal { get; set; }

    public decimal? PrecoMedio { get; set; }

    public decimal? Pl { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual Ativo? Ativo { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
