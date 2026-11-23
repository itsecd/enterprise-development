using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class CarRepository : ICarRepository
{
    private readonly List<Car> _cars = new(AutoServiceData.Cars);

    /// <inheritdoc />
    public Task<Car?> GetAsync(int id) => Task.FromResult(_cars.FirstOrDefault(c => c.Id == id));

    /// <inheritdoc />
    public Task<IReadOnlyList<Car>> GetAllAsync() => Task.FromResult<IReadOnlyList<Car>>(_cars);

    /// <inheritdoc />
    public Task AddAsync(Car entity)
    {
        entity.Id = _cars.Count == 0 ? 1 : _cars.Max(c => c.Id) + 1;
        _cars.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task UpdateAsync(Car entity)
    {
        var index = _cars.FindIndex(c => c.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException($"Автомобиль {entity.Id} не найден");
        _cars[index] = entity;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var car = await GetAsync(id);
        return car != null && _cars.Remove(car);
    }
}
