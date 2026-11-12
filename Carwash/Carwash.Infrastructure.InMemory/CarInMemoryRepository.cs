using Carwash.Domain;
using Carwash.Domain.Data;
using Carwash.Domain.Entities;

namespace Carwash.Infrastructure.InMemory;

/// <summary>
/// Имплементация репозитория для автомобилей
/// </summary>
public class CarInMemoryRepository : IRepository<Car, int>
{
    private readonly List<Car> _cars = DataSeed.Cars;
    private int _lastId = DataSeed.Cars.Count + 1;

    /// <inheritdoc/>
    public Task<Car> Create(Car entity)
    {
        entity.Id = _lastId++;
        _cars.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    public Task<bool> Delete(int entityId)
    {
        var car = Read(entityId).Result;
        if (car == null)
            return Task.FromResult(false);

        var result = _cars.Remove(car);
        return Task.FromResult(result);
    }

    /// <inheritdoc/>
    public Task<List<Car>> ReadAll()
    {
        return Task.FromResult(_cars.ToList());
    }

    /// <inheritdoc/>
    public Task<Car?> Read(int entityId)
    {
        var car = _cars.FirstOrDefault(c => c.Id == entityId);
        return Task.FromResult(car);
    }

    /// <inheritdoc/>
    public Task<Car> Update(Car entity)
    {
        var car = Read(entity.Id).Result;

        if (car == null)
            return Create(entity);

        car.LicensePlate = entity.LicensePlate;
        car.Brand = entity.Brand;
        car.ClientId = entity.ClientId;
        car.Client = entity.Client;

        return Task.FromResult(car);
    }
}
