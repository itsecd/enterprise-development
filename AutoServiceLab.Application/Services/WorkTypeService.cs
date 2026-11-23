using AutoServiceLab.Application.Contracts.Dtos.WorkTypes;
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
    public async Task<IList<WorkTypeDto>> GetAllAsync()
    {
        logger.LogInformation("Получение списка всех видов работ");
        return (await repository.GetAllAsync())
            .Select(w => new WorkTypeDto(w.Id, w.Name, w.Category, w.Duration, w.LaborCost))
            .ToList();
    }

    /// <inheritdoc />
    public async Task<WorkTypeDto?> GetAsync(int id)
    {
        logger.LogInformation("Получение вида работ с ID: {Id}", id);
        var w = await repository.GetAsync(id);
        return w == null ? null : new WorkTypeDto(w.Id, w.Name, w.Category, w.Duration, w.LaborCost);
    }

    /// <inheritdoc />
    public async Task<WorkTypeDto> CreateAsync(WorkTypeCreateUpdateDto dto)
    {
        logger.LogInformation("Создание вида работ: {Name}", dto.Name);
        var w = new WorkType { Name = dto.Name, Category = dto.Category, Duration = dto.Duration, LaborCost = dto.LaborCost };
        await repository.AddAsync(w);
        return new WorkTypeDto(w.Id, w.Name, w.Category, w.Duration, w.LaborCost);
    }

    /// <inheritdoc />
    public async Task<WorkTypeDto> UpdateAsync(WorkTypeCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление вида работ с ID: {Id}", id);
        var w = await repository.GetAsync(id) ?? throw new KeyNotFoundException();
        w.Name = dto.Name;
        w.Category = dto.Category;
        w.Duration = dto.Duration;
        w.LaborCost = dto.LaborCost;
        await repository.UpdateAsync(w);
        return new WorkTypeDto(w.Id, w.Name, w.Category, w.Duration, w.LaborCost);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        logger.LogInformation("Удаление вида работ с ID: {Id}", id);
        return await repository.DeleteAsync(id);
    }
}
