using ItauChallenge.Domain.Models;

namespace ItauChallenge.Infra.Persistence.Repositories;

public interface IFinancialAssetRepository
{
    Task<Ativo> CreateAsync(Ativo request);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Ativo>> GetAllAsync();
    Task<Ativo?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, Ativo request);
}
