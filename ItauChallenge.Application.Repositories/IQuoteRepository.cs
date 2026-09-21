using ItauChallenge.Application.Dtos.Quotes;

namespace ItauChallenge.Application.Repositories;

public interface IQuoteRepository
{
    Task<QuoteResponse> CreateAsync(CreateQuoteRequest request);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<QuoteResponse>> GetAllAsync();
    Task<QuoteResponse?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, UpdateQuoteRequest request);
}
