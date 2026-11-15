using Microsoft.Extensions.Logging;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Interfaces;
using MuseumSystem.Services.Mappers;

namespace MuseumSystem.Services.Services;

public class TicketService(
    IRepository<Ticket> repository,
    IRepository<Excursion> excursionRepository,
    IRepository<Visitor> visitorRepository,
    ILogger<TicketService> logger) : ITicketService
{
    public async Task<IEnumerable<TicketResponse>> GetAllAsync()
    {
        var entities = await repository.GetAllAsync();
        return entities.Select(t => t.ToDto());
    }

    public async Task<TicketResponse?> GetByIdAsync(int id)
    {
        var entity = await repository.GetByIdAsync(id);
        return entity?.ToDto();
    }

    public async Task<TicketResponse> CreateAsync(TicketCreateRequest request)
    {
        var excursion = await excursionRepository.GetByIdAsync(request.ExcursionId);
        var visitor = await visitorRepository.GetByIdAsync(request.VisitorId);

        if (excursion == null || visitor == null)
        {
            logger.LogError("Ошибка: Экскурсия {ExcursionId} или посетитель {VisitorId} не найдены", request.ExcursionId, request.VisitorId);
            throw new ArgumentException("Экскурсия или посетитель не найдены");
        }

        logger.LogInformation("Создание билета для экскурсии {ExcursionId}", request.ExcursionId);
        var entity = request.ToEntity(excursion, visitor);
        var created = await repository.AddAsync(entity);
        return created.ToDto();
    }

    public async Task<bool> UpdateAsync(int id, TicketUpdateRequest request)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;

        var excursion = await excursionRepository.GetByIdAsync(request.ExcursionId);
        var visitor = await visitorRepository.GetByIdAsync(request.VisitorId);

        if (excursion == null || visitor == null) return false;

        entity.UpdateEntity(request, excursion, visitor);
        var updated = await repository.UpdateAsync(entity);
        return updated != null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await repository.DeleteAsync(id);
    }
}