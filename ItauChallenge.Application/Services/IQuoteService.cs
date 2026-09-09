using ItauChallenge.Application.Dtos.Quotes;

namespace ItauChallenge.Application.Services;

public interface IQuoteService
{
    Task<IEnumerable<QuoteResponse>> GetAllAsync();
    Task<QuoteResponse?> GetByIdAsync(int id);
    Task<QuoteResponse> CreateAsync(CreateQuoteRequest request);
    Task<bool> UpdateAsync(int id, UpdateQuoteRequest request);
    Task<bool> DeleteAsync(int id);
}
