using ItauChallenge.Application.Dtos.FinancialAssets;

namespace ItauChallenge.Application.Services;

public interface IFinancialAssetService
{
    Task<IEnumerable<FinancialAssetResponse>> GetAllAsync();
    Task<FinancialAssetResponse?> GetByIdAsync(int id);
    Task<FinancialAssetResponse> CreateAsync(CreateFinancialAssetRequest request);
    Task<bool> UpdateAsync(int id, UpdateFinancialAssetRequest request);
    Task<bool> DeleteAsync(int id);
}
