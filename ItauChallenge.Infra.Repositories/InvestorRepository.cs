using ItauChallenge.Application.Repositories;
using ItauChallenge.Domain.Models;
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

    public async Task<Usuario> CreateAsync(Usuario request)
    {
        _db.Usuarios.Add(request);
        await _db.SaveChangesAsync();
        return request;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _db.Usuarios.FindAsync(id);
        if (entity == null) return false;

        _db.Usuarios.Remove(entity);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        var entities = await _db.Usuarios.ToListAsync();
        return entities;
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        var entity = await _db.Usuarios.FindAsync(id);
        return entity;
    }

    public async Task<bool> UpdateAsync(int id, Usuario request)
    {
        var entity = await _db.Usuarios.FindAsync(id);
        if (entity == null) return false;

        // Apply incoming domain values to tracked entity
        entity.Nome = request.Nome;
        entity.Email = request.Email;
        entity.Corretagem = request.Corretagem;
        entity.IsActive = request.IsActive;
        entity.UpdateAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }
}
