using System.ComponentModel.DataAnnotations;

namespace ItauChallenge.Application.Dtos.Positions;

public class UpdatePositionRequest
{
    [Required]
    public int AtivoId { get; set; }

    [Required]
    public int UsuarioId { get; set; }

    public int? QuantidadeB3 { get; set; }
    public decimal? QuantidadeGlobal { get; set; }
    public decimal? PrecoMedio { get; set; }
    public decimal? Pl { get; set; }
}
