using Microsoft.Extensions.Logging;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Interfaces;
using MuseumSystem.Services.Mappers;

namespace MuseumSystem.Services.Services;

public class ExhibitionService(
    IRepository<Exhibition> repository,
    ILogger<ExhibitionService> logger) : IExhibitionService
{
    public async Task<IEnumerable<ExhibitionResponse>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();
        return entities.Select(e => e.ToDto());
    }

    public async Task<ExhibitionResponse?> GetByIdAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity?.ToDto();
    }

    public async Task<ExhibitionResponse> CreateAsync(ExhibitionCreateRequest request)
    {
        logger.LogInformation("Создание выставки {Name}", request.Name);
        var entity = request.ToEntity();
        var created = await repository.AddAsync(entity);
        return created.ToDto();
    }

    public async Task<bool> UpdateAsync(int id, ExhibitionUpdateRequest request)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;

        entity.UpdateEntity(request);
        var updated = await repository.UpdateAsync(entity);
        return updated != null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await repository.DeleteAsync(id);
    }
}