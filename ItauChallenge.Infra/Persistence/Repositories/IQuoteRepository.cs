using ItauChallenge.Domain.Models;

namespace ItauChallenge.Infra.Persistence.Repositories;

public interface IQuoteRepository
{
    Task<Cotacao> CreateAsync(Cotacao request);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<Cotacao>> GetAllAsync();
    Task<Cotacao?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, Cotacao request);
}
