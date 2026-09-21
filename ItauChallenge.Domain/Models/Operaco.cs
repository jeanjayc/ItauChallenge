namespace ItauChallenge.Domain.Models;

public partial class Operacoes
{
    public int Id { get; set; }

    public int? UsuarioId { get; set; }

    public int AtivoId { get; set; }

    public int QuantidadeB3 { get; set; }

    public decimal QuantidadeGlobal { get; set; }

    public decimal PrecoUnit { get; set; }

    public OperationType TipoOperacao { get; set; }

    public decimal Corretagem { get; set; }

    public DateTime? DataHora { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Ativo Ativo { get; set; }

    public virtual Usuario Usuario { get; set; }
}
