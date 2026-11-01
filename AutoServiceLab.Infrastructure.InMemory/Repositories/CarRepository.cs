using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class CarRepository : ICarRepository
{
    /// <inheritdoc />
    public Car? Get(int id) => AutoServiceData.Cars.FirstOrDefault(c => c.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<Car> GetAll() => AutoServiceData.Cars;

    /// <inheritdoc />
    public void Add(Car entity)
    {
        entity.Id = AutoServiceData.Cars.Count == 0 ? 1 : AutoServiceData.Cars.Max(c => c.Id) + 1;
        AutoServiceData.Cars.Add(entity);
    }

    /// <inheritdoc />
    public void Update(Car entity)
    {
        var index = AutoServiceData.Cars.FindIndex(c => c.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException($"Автомобиль {entity.Id} не найден");
        AutoServiceData.Cars[index] = entity;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var car = Get(id);
        return car != null && AutoServiceData.Cars.Remove(car);
    }
}
