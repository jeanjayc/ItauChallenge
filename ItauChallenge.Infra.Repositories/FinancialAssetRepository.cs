using ItauChallenge.Application.Dtos.FinancialAssets;
using ItauChallenge.Application.Mappings;
using ItauChallenge.Application.Repositories;
using ItauChallenge.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ItauChallenge.Infra.Repositories;

public class FinancialAssetRepository : IFinancialAssetRepository
{
    private readonly ItauChallengeContext _db;

    public FinancialAssetRepository(ItauChallengeContext db)
    {
        _db = db;
    }

    public async Task<FinancialAssetResponse> CreateAsync(CreateFinancialAssetRequest request)
    {
        var entity = FinancialAssetMapper.ToEntity(request);
        _db.Ativos.Add(entity);
        await _db.SaveChangesAsync();
        return FinancialAssetMapper.ToResponse(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _db.Ativos.FindAsync(id);
        if (entity == null) return false;

        _db.Ativos.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<FinancialAssetResponse>> GetAllAsync()
    {
        var entities = await _db.Ativos.ToListAsync();
        return entities.Select(FinancialAssetMapper.ToResponse);
    }

    public async Task<FinancialAssetResponse?> GetByIdAsync(int id)
    {
        var entity = await _db.Ativos.FindAsync(id);
        return entity == null ? null : FinancialAssetMapper.ToResponse(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateFinancialAssetRequest request)
    {
        var entity = await _db.Ativos.FindAsync(id);
        if (entity == null) return false;

        FinancialAssetMapper.ApplyUpdate(entity, request);
        await _db.SaveChangesAsync();
        return true;
    }
}
