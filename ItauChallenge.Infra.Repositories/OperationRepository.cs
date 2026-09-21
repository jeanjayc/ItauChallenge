using ItauChallenge.Application.Dtos.Operations;
using ItauChallenge.Application.Mappings;
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

    public async Task<OperationResponse> CreateAsync(CreateOperationRequest request)
    {
        var entity = OperationMapper.ToEntity(request);
        _db.Operacoes.Add(entity);
        await _db.SaveChangesAsync();

        var created = await _db.Operacoes
            .Include(o => o.Ativo)
            .Include(o => o.Usuario)
            .FirstAsync(o => o.Id == entity.Id);

        return OperationMapper.ToResponse(created);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _db.Operacoes.FindAsync(id);
        if (entity == null) return false;

        _db.Operacoes.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<OperationResponse>> GetAllAsync()
    {
        var entities = await _db.Operacoes
            .Include(o => o.Ativo)
            .Include(o => o.Usuario)
            .ToListAsync();

        return entities.Select(OperationMapper.ToResponse);
    }

    public async Task<OperationResponse?> GetByIdAsync(int id)
    {
        var entity = await _db.Operacoes
            .Include(o => o.Ativo)
            .Include(o => o.Usuario)
            .FirstOrDefaultAsync(o => o.Id == id);

        return entity == null ? null : OperationMapper.ToResponse(entity);
    }

    public async Task<bool> UpdateAsync(int id, UpdateOperationRequest request)
    {
        var entity = await _db.Operacoes.FindAsync(id);
        if (entity == null) return false;

        OperationMapper.ApplyUpdate(entity, request);
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
