namespace ItauChallenge.Application.Dtos.Shared;

public class InvestorSummaryResponse
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string Email { get; set; } = null!;
}
