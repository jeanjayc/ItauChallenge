using ItauChallenge.Application.Dtos.Operations;

namespace ItauChallenge.Application.Repositories;

public interface IOperationRepository
{
    Task<OperationResponse> CreateAsync(CreateOperationRequest request);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<OperationResponse>> GetAllAsync();
    Task<OperationResponse?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, UpdateOperationRequest request);
    decimal CalculateWeightedAveragePrice(int assetId);
}
