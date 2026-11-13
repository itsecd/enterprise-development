using AutoService.Contracts.Dtos;
using AutoService.Contracts.Interfaces;
using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;

namespace AutoService.Application.Services;

/// <summary>
/// Сервис для управления видами работ.
/// </summary>
public class WorkTypeService(IWorkTypeRepository repository) : IWorkTypeService
{
    /// <summary>
    /// Получить список всех видов работ.
    /// </summary>
    public async Task<IEnumerable<WorkTypeDto>> GetAllAsync()
    {
        var works = await repository.GetAllAsync();
        return works.Select(w => new WorkTypeDto
        {
            Id = w.Id, Name = w.Name, Category = w.Category, Price = w.Price, Duration = w.Duration
        });
    }

    /// <summary>
    /// Получить вид работ по идентификатору.
    /// </summary>
    public async Task<WorkTypeDto?> GetByIdAsync(Guid id)
    {
        var w = await repository.GetByIdAsync(id);
        if (w == null) return null;
        return new WorkTypeDto { Id = w.Id, Name = w.Name, Category = w.Category, Price = w.Price, Duration = w.Duration };
    }

    /// <summary>
    /// Добавить новый вид работ.
    /// </summary>
    public async Task<WorkTypeDto> CreateAsync(WorkTypeDto workDto)
    {
        var work = new WorkType
        {
            Id = Guid.NewGuid(),
            Name = workDto.Name,
            Category = workDto.Category,
            Price = workDto.Price,
            Duration = workDto.Duration
        };
        await repository.AddAsync(work);
        workDto.Id = work.Id;
        return workDto;
    }

    /// <summary>
    /// Обновить данные вида работ.
    /// </summary>
    public async Task UpdateAsync(Guid id, WorkTypeUpdateDto workDto)
    {
        var work = await repository.GetByIdAsync(id);
        if (work == null) return;

        work.Name = workDto.Name;
        work.Category = workDto.Category;
        work.Price = workDto.Price;
        work.Duration = workDto.Duration;
        await repository.UpdateAsync(work);
    }

    /// <summary>
    /// Удалить вид работ.
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var work = await repository.GetByIdAsync(id);
        if (work == null) return;
        await repository.DeleteAsync(work);
    }
}