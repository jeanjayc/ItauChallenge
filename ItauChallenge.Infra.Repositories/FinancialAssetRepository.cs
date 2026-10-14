using ItauChallenge.Application.Repositories;
using ItauChallenge.Domain.Models;
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

    public async Task<Ativo> CreateAsync(Ativo request)
    {
        _db.Ativos.Add(request);
        await _db.SaveChangesAsync();
        return request;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _db.Ativos.FindAsync(id);
        if (entity == null) return false;

        _db.Ativos.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Ativo>> GetAllAsync()
    {
        var entities = await _db.Ativos.ToListAsync();
        return entities;
    }

    public async Task<Ativo?> GetByIdAsync(int id)
    {
        var entity = await _db.Ativos.FindAsync(id);
        return entity;
    }

    public async Task<bool> UpdateAsync(int id, Ativo request)
    {
        var entity = await _db.Ativos.FindAsync(id);
        if (entity == null) return false;

        entity.Codigo = request.Codigo;
        entity.Nome = request.Nome;
        entity.IsActive = request.IsActive;
        entity.CreatedAt = entity.CreatedAt ?? DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }
}
