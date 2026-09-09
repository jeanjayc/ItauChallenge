namespace ItauChallenge.Domain.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string Email { get; set; } = null!;

    public decimal? Corretagem { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Operacoes> Operacos { get; set; } = new List<Operacoes>();

    public virtual ICollection<Posicao> Posicaos { get; set; } = new List<Posicao>();
}
