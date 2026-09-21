using ItauChallenge.Application.Dtos.Investors;
namespace ItauChallenge.Application.Repositories;

public interface IInvestorRepository
{
    Task<InvestorResponse> CreateAsync(CreateInvestorRequest request);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<InvestorResponse>> GetAllAsync();
    Task<InvestorResponse?> GetByIdAsync(int id);
    Task<bool> UpdateAsync(int id, UpdateInvestorRequest request);
}
