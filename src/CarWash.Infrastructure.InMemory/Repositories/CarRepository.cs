using CarWash.Domain.Data;
using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;

namespace CarWash.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий, хранящий автомобили в памяти
/// </summary>
public class CarRepository : ICarRepository
{
    private readonly List<Car> _cars = [.. DataSeed.Cars];

    /// <inheritdoc />
    public Task<IEnumerable<Car>> GetAllAsync()
        => Task.FromResult<IEnumerable<Car>>([.. _cars]);

    /// <inheritdoc />
    public Task<Car?> GetByIdAsync(int id)
        => Task.FromResult(_cars.FirstOrDefault(c => c.Id == id));

    /// <inheritdoc />
    public Task<Car> AddAsync(Car entity)
    {
        entity.Id = _cars.Count != 0 ? _cars.Max(c => c.Id) + 1 : 1;
        _cars.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc />
    public Task UpdateAsync(Car entity)
    {
        var index = _cars.FindIndex(c => c.Id == entity.Id);
        if (index != -1)
        {
            _cars[index] = entity;
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task DeleteAsync(int id)
    {
        var item = _cars.FirstOrDefault(c => c.Id == id);
        if (item != null)
        {
            _cars.Remove(item);
        }

        return Task.CompletedTask;
    }
}
