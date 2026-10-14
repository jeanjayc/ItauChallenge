using ItauChallenge.Application.Repositories;
using ItauChallenge.Domain.Models;
using ItauChallenge.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ItauChallenge.Infra.Repositories;

public class OperationRepository : IOperationRepository
{
    private readonly ItauChallengeContext _db;

    public OperationRepository(ItauChallengeContext db)
    {
        _db = db;
    }

    public async Task<Operacoes> CreateAsync(Operacoes request)
    {
        _db.Operacoes.Add(request);
        await _db.SaveChangesAsync();

        var created = await _db.Operacoes
            .Include(o => o.Ativo)
            .Include(o => o.Usuario)
            .FirstAsync(o => o.Id == request.Id);

        return created;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _db.Operacoes.FindAsync(id);
        if (entity == null) return false;

        _db.Operacoes.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Operacoes>> GetAllAsync()
    {
        var entities = await _db.Operacoes
            .Include(o => o.Ativo)
            .Include(o => o.Usuario)
            .ToListAsync();

        return entities;
    }

    public async Task<Operacoes?> GetByIdAsync(int id)
    {
        var entity = await _db.Operacoes
            .Include(o => o.Ativo)
            .Include(o => o.Usuario)
            .FirstOrDefaultAsync(o => o.Id == id);

        return entity;
    }

    public async Task<bool> UpdateAsync(int id, Operacoes request)
    {
        var entity = await _db.Operacoes.FindAsync(id);
        if (entity == null) return false;

        entity.AtivoId = request.AtivoId;
        entity.UsuarioId = request.UsuarioId;
        entity.QuantidadeB3 = request.QuantidadeB3;
        entity.QuantidadeGlobal = request.QuantidadeGlobal;
        entity.PrecoUnit = request.PrecoUnit;
        entity.TipoOperacao = request.TipoOperacao;
        entity.Corretagem = request.Corretagem;
        entity.DataHora = request.DataHora;
        entity.CreatedAt = entity.CreatedAt ?? DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }

    public decimal CalculateWeightedAveragePrice(int assetId)
    {
        if (assetId <= 0)
        {
            throw new ArgumentException("Invalid asset ID.");
        }

        var operations = _db.Operacoes
            .Where(a => a.AtivoId == assetId && a.TipoOperacao == OperationType.Buy)
            .ToList();

        if (operations == null || !operations.Any())
        {
            throw new InvalidOperationException("No buy operations found for the specified asset.");
        }

        var totalQuantity = operations!.Sum(o => o.QuantidadeB3);
        var totalCost = operations!.Sum(o => o.QuantidadeB3 * o.PrecoUnit);

        if (totalQuantity <= 0)
        {
            throw new InvalidOperationException("Invalid total quantity.");
        }

        return totalCost / totalQuantity;
    }
}
