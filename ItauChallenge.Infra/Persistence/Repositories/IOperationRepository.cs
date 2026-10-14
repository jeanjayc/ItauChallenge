using ItauChallenge.Domain.Models;

namespace ItauChallenge.Infra.Persistence.Repositories;

public interface IOperationRepository
{
    Task<Operacoes> CreateAsync(Operacoes request);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Operacoes>> GetAllAsync();
    Task<Operacoes?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, Operacoes request);
    decimal CalculateWeightedAveragePrice(int assetId);
}
