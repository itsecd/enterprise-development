using AutoService.Domain.Data;
using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;

namespace AutoService.Infrastructure.InMemory;

/// <summary>
/// Реализация репозитория видов работ в памяти.
/// </summary>
public class InMemoryWorkTypeRepository : IWorkTypeRepository
{
    private readonly List<WorkType> _works = DataSeed.Works.ToList();

    /// <summary>
    /// Получить список всех видов работ.
    /// </summary>
    public Task<IEnumerable<WorkType>> GetAllAsync() => Task.FromResult(_works.AsEnumerable());

    /// <summary>
    /// Получить вид работ по идентификатору.
    /// </summary>
    public Task<WorkType?> GetByIdAsync(Guid id) => Task.FromResult(_works.FirstOrDefault(w => w.Id == id));

    /// <summary>
    /// Добавить вид работ.
    /// </summary>
    public Task AddAsync(WorkType workType)
    {
        _works.Add(workType);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновить данные вида работ.
    /// </summary>
    public Task UpdateAsync(WorkType workType)
    {
        var index = _works.FindIndex(w => w.Id == workType.Id);
        if (index != -1) _works[index] = workType;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Удалить вид работ.
    /// </summary>
    public Task DeleteAsync(WorkType workType)
    {
        _works.Remove(workType);
        return Task.CompletedTask;
    }
}