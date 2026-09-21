using ItauChallenge.Application.Dtos.Quotes;
using ItauChallenge.Application.Mappings;
using ItauChallenge.Application.Repositories;
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

    public async Task<QuoteResponse> CreateAsync(CreateQuoteRequest request)
    {
        var entity = QuoteMapper.ToEntity(request);
        _db.Cotacaos.Add(entity);
        await _db.SaveChangesAsync();

        var created = await _db.Cotacaos
            .Include(c => c.Ativo)
            .FirstAsync(c => c.Id == entity.Id);

        return QuoteMapper.ToResponse(created);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _db.Cotacaos.FindAsync(id);
        if (entity == null) return false;

        _db.Cotacaos.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<QuoteResponse>> GetAllAsync()
    {
        var entities = await _db.Cotacaos
            .Include(c => c.Ativo)
            .ToListAsync();

        return entities.Select(QuoteMapper.ToResponse);
    }

    public async Task<QuoteResponse?> GetByIdAsync(int id)
    {
        var entity = await _db.Cotacaos
            .Include(c => c.Ativo)
            .FirstOrDefaultAsync(c => c.Id == id);

        return entity == null ? null : QuoteMapper.ToResponse(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateQuoteRequest request)
    {
        var entity = await _db.Cotacaos.FindAsync(id);
        if (entity == null) return false;

        QuoteMapper.ApplyUpdate(entity, request);
        await _db.SaveChangesAsync();
        return true;
    }
}
