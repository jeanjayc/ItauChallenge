using System.ComponentModel.DataAnnotations;
using ItauChallenge.Domain.Models;

namespace ItauChallenge.Application.Dtos.Operations;

public class UpdateOperationRequest
{
    [Required]
    public int UsuarioId { get; set; }

    [Required]
    public int AtivoId { get; set; }

    public int? QuantidadeB3 { get; set; }
    public decimal? QuantidadeGlobal { get; set; }
    public decimal? PrecoUnit { get; set; }

    [Required]
    public OperationType TipoOperacao { get; set; }

    public decimal? Corretagem { get; set; }
    public DateTime? DataHora { get; set; }
}
