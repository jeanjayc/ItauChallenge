using ItauChallenge.Application.Dtos.Investors;
using ItauChallenge.Application.Mappings;
using ItauChallenge.Application.Repositories;
using ItauChallenge.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ItauChallenge.Infra.Repositories;

public class InvestorRepository : IInvestorRepository
{
    private readonly ItauChallengeContext _db;

    public InvestorRepository(ItauChallengeContext db)
    {
        _db = db;
    }

    public async Task<InvestorResponse> CreateAsync(CreateInvestorRequest request)
    {
        var entity = InvestorMapper.ToEntity(request);
        _db.Usuarios.Add(entity);
        await _db.SaveChangesAsync();
        return InvestorMapper.ToResponse(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _db.Usuarios.FindAsync(id);
        if (entity == null) return false;

        _db.Usuarios.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<InvestorResponse>> GetAllAsync()
    {
        var entities = await _db.Usuarios.ToListAsync();
        return entities.Select(InvestorMapper.ToResponse);
    }

    public async Task<InvestorResponse?> GetByIdAsync(int id)
    {
        var entity = await _db.Usuarios.FindAsync(id);
        return entity == null ? null : InvestorMapper.ToResponse(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateInvestorRequest request)
    {
        var entity = await _db.Usuarios.FindAsync(id);
        if (entity == null) return false;

        InvestorMapper.ApplyUpdate(entity, request);
        await _db.SaveChangesAsync();
        return true;
    }
}
