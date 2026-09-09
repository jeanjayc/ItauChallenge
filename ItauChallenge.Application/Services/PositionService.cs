using ItauChallenge.Application.Dtos.Positions;
using ItauChallenge.Application.Mappings;
using ItauChallenge.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ItauChallenge.Application.Services;

public class PositionService : IPositionService
{
    private readonly ItauChallengeContext _db;

    public PositionService(ItauChallengeContext db)
    {
        _db = db;
    }

    public async Task<PositionResponse> CreateAsync(CreatePositionRequest request)
    {
        var entity = PositionMapper.ToEntity(request);
        _db.Posicaos.Add(entity);
        await _db.SaveChangesAsync();

        var created = await _db.Posicaos
            .Include(p => p.Ativo)
            .Include(p => p.Usuario)
            .FirstAsync(p => p.Id == entity.Id);

        return PositionMapper.ToResponse(created);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _db.Posicaos.FindAsync(id);
        if (entity == null) return false;

        _db.Posicaos.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<PositionResponse>> GetAllAsync()
    {
        var entities = await _db.Posicaos
            .Include(p => p.Ativo)
            .Include(p => p.Usuario)
            .ToListAsync();

        return entities.Select(PositionMapper.ToResponse);
    }

    public async Task<PositionResponse?> GetByIdAsync(int id)
    {
        var entity = await _db.Posicaos
            .Include(p => p.Ativo)
            .Include(p => p.Usuario)
            .FirstOrDefaultAsync(p => p.Id == id);

        return entity == null ? null : PositionMapper.ToResponse(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdatePositionRequest request)
    {
        var entity = await _db.Posicaos.FindAsync(id);
        if (entity == null) return false;

        PositionMapper.ApplyUpdate(entity, request);
        await _db.SaveChangesAsync();
        return true;
    }
}
