using ItauChallenge.Application.Dtos.FinancialAssets;
using ItauChallenge.Domain.Models;

namespace ItauChallenge.Application.Mappings;

public static class FinancialAssetMapper
{
    public static Ativo ToEntity(CreateFinancialAssetRequest request)
    {
        return new Ativo
        {
            Codigo = request.Codigo,
            Nome = request.Nome,
            IsActive = request.IsActive ?? true,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static void ApplyUpdate(Ativo entity, UpdateFinancialAssetRequest request)
    {
        entity.Codigo = request.Codigo;
        entity.Nome = request.Nome;
        entity.IsActive = request.IsActive;
    }

    public static FinancialAssetResponse ToResponse(Ativo entity)
    {
        return new FinancialAssetResponse
        {
            Id = entity.Id,
            Codigo = entity.Codigo,
            Nome = entity.Nome,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt
        };
    }
}
