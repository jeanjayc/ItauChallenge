using System.ComponentModel.DataAnnotations;

namespace ItauChallenge.Application.Dtos.Quotes;

public class CreateQuoteRequest
{
    [Required]
    public int AtivoId { get; set; }

    public decimal PrecoUnit { get; set; }
    public DateTime DataHora { get; set; }
    public bool IsActive { get; set; }
}
