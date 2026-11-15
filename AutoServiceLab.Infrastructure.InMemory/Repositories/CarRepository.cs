using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class CarRepository : ICarRepository
{
    private readonly List<Car> _cars = AutoServiceData.Cars;

    /// <inheritdoc />
    public Car? Get(int id) => _cars.FirstOrDefault(c => c.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<Car> GetAll() => _cars;

    /// <inheritdoc />
    public void Add(Car entity)
    {
        entity.Id = _cars.Count == 0 ? 1 : _cars.Max(c => c.Id) + 1;
        _cars.Add(entity);
    }

    /// <inheritdoc />
    public void Update(Car entity)
    {
        var index = _cars.FindIndex(c => c.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException($"Автомобиль {entity.Id} не найден");
        _cars[index] = entity;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var car = Get(id);
        return car != null && _cars.Remove(car);
    }
}
