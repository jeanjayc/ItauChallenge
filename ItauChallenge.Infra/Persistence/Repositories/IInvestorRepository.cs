using ItauChallenge.Domain.Models;

namespace ItauChallenge.Infra.Persistence.Repositories;

public interface IInvestorRepository
{
    Task<Usuario> CreateAsync(Usuario request);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, Usuario request);
}
