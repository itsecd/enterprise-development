using Microsoft.Extensions.Logging;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Interfaces;
using MuseumSystem.Services.Mappers;

namespace MuseumSystem.Services.Services;

public class VisitorService(
    IRepository<Visitor> repository,
    ILogger<VisitorService> logger) : IVisitorService
{
    public async Task<IEnumerable<VisitorResponse>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();
        return entities.Select(v => v.ToDto());
    }

    public async Task<VisitorResponse?> GetByIdAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity?.ToDto();
    }

    public async Task<VisitorResponse> CreateAsync(VisitorCreateRequest request)
    {
        logger.LogInformation("Создание посетителя {FullName}", request.FullName);
        var entity = request.ToEntity();
        var created = await repository.AddAsync(entity);
        return created.ToDto();
    }

    public async Task<bool> UpdateAsync(int id, VisitorUpdateRequest request)
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