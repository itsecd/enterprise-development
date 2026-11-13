using AutoService.Domain.Data;
using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;

namespace AutoService.Infrastructure.InMemory;

/// <summary>
/// Реализация репозитория механиков в памяти.
/// </summary>
public class InMemoryMechanicRepository : IMechanicRepository
{
    private readonly List<Mechanic> _mechanics = DataSeed.Mechanics.ToList();

    /// <summary>
    /// Получить список всех механиков.
    /// </summary>
    public Task<IEnumerable<Mechanic>> GetAllAsync() => Task.FromResult(_mechanics.AsEnumerable());

    /// <summary>
    /// Получить механика по идентификатору.
    /// </summary>
    public Task<Mechanic?> GetByIdAsync(Guid id) => Task.FromResult(_mechanics.FirstOrDefault(m => m.Id == id));

    /// <summary>
    /// Добавить механика.
    /// </summary>
    public Task AddAsync(Mechanic mechanic)
    {
        _mechanics.Add(mechanic);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновить данные механика.
    /// </summary>
    public Task UpdateAsync(Mechanic mechanic)
    {
        var index = _mechanics.FindIndex(m => m.Id == mechanic.Id);
        if (index != -1) _mechanics[index] = mechanic;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Удалить механика.
    /// </summary>
    public Task DeleteAsync(Mechanic mechanic)
    {
        _mechanics.Remove(mechanic);
        return Task.CompletedTask;
    }
}