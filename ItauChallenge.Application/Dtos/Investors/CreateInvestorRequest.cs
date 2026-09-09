using System.ComponentModel.DataAnnotations;

namespace ItauChallenge.Application.Dtos.Investors;

public class CreateInvestorRequest
{
    public string? Nome { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    public decimal? Corretagem { get; set; }

    public bool? IsActive { get; set; }
}
