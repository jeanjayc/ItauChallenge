using ItauChallenge.Application.Dtos.Investors;

namespace ItauChallenge.Application.Services;

public interface IInvestorService
{
    Task<IEnumerable<InvestorResponse>> GetAllAsync();
    Task<InvestorResponse?> GetByIdAsync(int id);
    Task<InvestorResponse> CreateAsync(CreateInvestorRequest request);
    Task<bool> UpdateAsync(int id, UpdateInvestorRequest request);
    Task<bool> DeleteAsync(int id);
}
