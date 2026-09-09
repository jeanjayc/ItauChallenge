using System.ComponentModel.DataAnnotations;

namespace ItauChallenge.Application.Dtos.FinancialAssets;

public class UpdateFinancialAssetRequest
{
    [Required]
    [MaxLength(10)]
    public string Codigo { get; set; } = null!;

    [MaxLength(60)]
    public string? Nome { get; set; }

    public bool? IsActive { get; set; }
}
