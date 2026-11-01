using AutoServiceLab.Application.Contracts.DTOs.WorkTypes;
using AutoServiceLab.Application.Contracts.Interfaces;
using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace AutoServiceLab.Application.Services;

/// <summary>
/// Сервис для работы с видами работ
/// </summary>
public class WorkTypeService(IWorkTypeRepository repository, ILogger<WorkTypeService> logger) : IWorkTypeService
{
    /// <inheritdoc />
    public IList<WorkTypeDto> GetAll()
    {
        logger.LogInformation("Получение списка всех видов работ");
        return repository.GetAll()
            .Select(w => new WorkTypeDto(w.Id, w.Name, w.Category, w.Duration, w.LaborCost))
            .ToList();
    }

    /// <inheritdoc />
    public WorkTypeDto? Get(int id)
    {
        logger.LogInformation("Получение вида работ с ID: {Id}", id);
        var w = repository.Get(id);
        return w == null ? null : new WorkTypeDto(w.Id, w.Name, w.Category, w.Duration, w.LaborCost);
    }

    /// <inheritdoc />
    public WorkTypeDto Create(WorkTypeCreateUpdateDto dto)
    {
        logger.LogInformation("Создание вида работ: {Name}", dto.Name);
        var w = new WorkType { Name = dto.Name, Category = dto.Category, Duration = dto.Duration, LaborCost = dto.LaborCost };
        repository.Add(w);
        return new WorkTypeDto(w.Id, w.Name, w.Category, w.Duration, w.LaborCost);
    }

    /// <inheritdoc />
    public WorkTypeDto Update(WorkTypeCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление вида работ с ID: {Id}", id);
        var w = repository.Get(id) ?? throw new KeyNotFoundException();
        w.Name = dto.Name;
        w.Category = dto.Category;
        w.Duration = dto.Duration;
        w.LaborCost = dto.LaborCost;
        repository.Update(w);
        return new WorkTypeDto(w.Id, w.Name, w.Category, w.Duration, w.LaborCost);
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        logger.LogInformation("Удаление вида работ с ID: {Id}", id);
        return repository.Delete(id);
    }
}
