using ItauChallenge.Application.Dtos.Investors;
using ItauChallenge.Domain.Models;

namespace ItauChallenge.Application.Mappings;

public static class InvestorMapper
{
    public static Usuario ToEntity(CreateInvestorRequest request)
    {
        return new Usuario
        {
            Nome = request.Nome,
            Email = request.Email,
            Corretagem = request.Corretagem,
            IsActive = request.IsActive ?? true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static void ApplyUpdate(Usuario entity, UpdateInvestorRequest request)
    {
        entity.Nome = request.Nome;
        entity.Email = request.Email;
        entity.Corretagem = request.Corretagem;
        entity.IsActive = request.IsActive;
        entity.UpdateAt = DateTime.UtcNow;
    }

    public static InvestorResponse ToResponse(Usuario entity)
    {
        return new InvestorResponse
        {
            Id = entity.Id,
            Nome = entity.Nome,
            Email = entity.Email,
            Corretagem = entity.Corretagem,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            UpdateAt = entity.UpdateAt
        };
    }
}
