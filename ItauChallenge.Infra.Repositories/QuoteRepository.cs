using ItauChallenge.Application.Repositories;
using ItauChallenge.Domain.Models;
using ItauChallenge.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ItauChallenge.Infra.Repositories;

public class QuoteRepository : IQuoteRepository
{
    private readonly ItauChallengeContext _db;

    public QuoteRepository(ItauChallengeContext db)
    {
        _db = db;
    }

    public async Task<Cotacao> CreateAsync(Cotacao request)
    {
        _db.Cotacaos.Add(request);
        await _db.SaveChangesAsync();

        var created = await _db.Cotacaos
            .Include(c => c.Ativo)
            .FirstAsync(c => c.Id == request.Id);

        return created;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _db.Cotacaos.FindAsync(id);
        if (entity == null) return false;

        _db.Cotacaos.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Cotacao>> GetAllAsync()
    {
        var entities = await _db.Cotacaos
            .Include(c => c.Ativo)
            .ToListAsync();

        return entities;
    }

    public async Task<Cotacao?> GetByIdAsync(int id)
    {
        var entity = await _db.Cotacaos
            .Include(c => c.Ativo)
            .FirstOrDefaultAsync(c => c.Id == id);

        return entity;
    }

    public async Task<bool> UpdateAsync(int id, Cotacao request)
    {
        var entity = await _db.Cotacaos.FindAsync(id);
        if (entity == null) return false;

        entity.PrecoUnit = request.PrecoUnit;
        entity.DataHora = request.DataHora;
        entity.IsActive = request.IsActive;
        entity.CreatedAt = entity.CreatedAt ?? DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }
}
