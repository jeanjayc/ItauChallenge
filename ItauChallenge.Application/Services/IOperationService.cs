using ItauChallenge.Application.Dtos.Operations;

namespace ItauChallenge.Application.Services;

public interface IOperationService
{
    Task<IEnumerable<OperationResponse>> GetAllAsync();
    Task<OperationResponse?> GetByIdAsync(int id);
    Task<OperationResponse> CreateAsync(CreateOperationRequest request);
    Task<bool> UpdateAsync(int id, UpdateOperationRequest request);
    Task<bool> DeleteAsync(int id);
}
