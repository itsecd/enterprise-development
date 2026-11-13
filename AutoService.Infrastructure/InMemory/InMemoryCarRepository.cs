using AutoService.Domain.Data;
using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;

namespace AutoService.Infrastructure.InMemory;

/// <summary>
/// Реализация репозитория автомобилей в памяти.
/// </summary>
public class InMemoryCarRepository : ICarRepository
{
    private readonly List<Car> _cars = DataSeed.Cars.ToList();

    /// <summary>
    /// Получить список всех автомобилей.
    /// </summary>
    public Task<IEnumerable<Car>> GetAllAsync() => Task.FromResult(_cars.AsEnumerable());

    /// <summary>
    /// Получить автомобиль по идентификатору.
    /// </summary>
    public Task<Car?> GetByIdAsync(Guid id) => Task.FromResult(_cars.FirstOrDefault(c => c.Id == id));

    /// <summary>
    /// Добавить автомобиль.
    /// </summary>
    public Task AddAsync(Car car)
    {
        _cars.Add(car);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновить данные автомобиля.
    /// </summary>
    public Task UpdateAsync(Car car)
    {
        var index = _cars.FindIndex(c => c.Id == car.Id);
        if (index != -1) _cars[index] = car;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Удалить автомобиль.
    /// </summary>
    public Task DeleteAsync(Car car)
    {
        _cars.Remove(car);
        return Task.CompletedTask;
    }
}