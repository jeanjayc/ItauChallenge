namespace ItauChallenge.Application.Dtos.Investors;

public class InvestorResponse
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string Email { get; set; } = null!;
    public decimal? Corretagem { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
}
