using ItauChallenge.Application.Dtos.Investors;
using ItauChallenge.Application.Mappings;
using ItauChallenge.Infra.Persistence.Repositories;

namespace ItauChallenge.Application.Services;

public class InvestorService : IInvestorService
{
    private readonly IInvestorRepository _repository;

    public InvestorService(IInvestorRepository repository)
    {
        _repository = repository;
    }

    public async Task<InvestorResponse> CreateAsync(CreateInvestorRequest request)
    {
        var entity = InvestorMapper.ToEntity(request);
        var created = await _repository.CreateAsync(entity);
        return InvestorMapper.ToResponse(created);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<InvestorResponse>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(InvestorMapper.ToResponse);
    }

    public async Task<InvestorResponse?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : InvestorMapper.ToResponse(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateInvestorRequest request)
    {
        var updateEntity = new Domain.Models.Usuario
        {
            Nome = request.Nome,
            Email = request.Email,
            Corretagem = request.Corretagem,
            IsActive = request.IsActive
        };

        return await _repository.UpdateAsync(id, updateEntity);
    }
}
