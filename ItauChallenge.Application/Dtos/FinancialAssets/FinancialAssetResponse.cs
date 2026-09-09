namespace ItauChallenge.Application.Dtos.FinancialAssets;

public class FinancialAssetResponse
{
    public int Id { get; set; }
    public string Codigo { get; set; } = null!;
    public string? Nome { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
}
