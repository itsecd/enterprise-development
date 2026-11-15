using Microsoft.Extensions.Logging;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Interfaces;
using MuseumSystem.Services.Mappers;

namespace MuseumSystem.Services.Services;

public class ExcursionService(
    IRepository<Excursion> repository,
    IRepository<Exhibition> exhibitionRepository,
    ILogger<ExcursionService> logger) : IExcursionService
{
    public async Task<IEnumerable<ExcursionResponse>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();
        return entities.Select(e => e.ToDto());
    }

    public async Task<ExcursionResponse?> GetByIdAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity?.ToDto();
    }

    public async Task<ExcursionResponse> CreateAsync(ExcursionCreateRequest request)
    {
        logger.LogInformation("Создание экскурсии на {StartsAt}", request.StartsAt);
        var exhibitions = await GetExhibitionsAsync(request.ExhibitionIds);
        var entity = request.ToEntity(exhibitions);
        var created = await repository.AddAsync(entity);
        return created.ToDto();
    }

    public async Task<bool> UpdateAsync(int id, ExcursionUpdateRequest request)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;

        var exhibitions = await GetExhibitionsAsync(request.ExhibitionIds);
        entity.UpdateEntity(request, exhibitions);
        var updated = await repository.UpdateAsync(entity);
        return updated != null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await repository.DeleteAsync(id);
    }

    private async Task<List<Exhibition>> GetExhibitionsAsync(IEnumerable<int> exhibitionIds)
    {
        var exhibitions = new List<Exhibition>();
        foreach (var exhibitionId in exhibitionIds)
        {
            var exhibition = await exhibitionRepository.GetByIdAsync(exhibitionId);
            if (exhibition != null) exhibitions.Add(exhibition);
        }
        return exhibitions;
    }
}