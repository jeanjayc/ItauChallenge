using ItauChallenge.Application.Dtos.Positions;

namespace ItauChallenge.Application.Services;

public interface IPositionService
{
    Task<IEnumerable<PositionResponse>> GetAllAsync();
    Task<PositionResponse?> GetByIdAsync(int id);
    Task<PositionResponse> CreateAsync(CreatePositionRequest request);
    Task<bool> UpdateAsync(int id, UpdatePositionRequest request);
    Task<bool> DeleteAsync(int id);
}
