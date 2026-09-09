namespace ItauChallenge.Domain.Models;

public partial class Ativo
{
    public int Id { get; set; }

    public string Codigo { get; set; } = null!;

    public string? Nome { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Cotacao> Cotacaos { get; set; } = new List<Cotacao>();

    public virtual ICollection<Operacoes> Operacos { get; set; } = new List<Operacoes>();

    public virtual ICollection<Posicao> Posicaos { get; set; } = new List<Posicao>();
}
