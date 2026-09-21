using ItauChallenge.Application.Dtos.FinancialAssets;

namespace ItauChallenge.Application.Repositories;

public interface IFinancialAssetRepository
{
    Task<FinancialAssetResponse> CreateAsync(CreateFinancialAssetRequest request);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<FinancialAssetResponse>> GetAllAsync();
    Task<FinancialAssetResponse?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, UpdateFinancialAssetRequest request);
}
