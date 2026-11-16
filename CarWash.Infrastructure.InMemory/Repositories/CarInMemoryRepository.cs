using CarWash.Domain;
using CarWash.Domain.Data;
using CarWash.Domain.Entities;

namespace CarWash.Infrastructure.InMemory.Repositories;

/// <summary>
/// Имплементация репозитория для авто
/// </summary>
public class CarInMemoryRepository(CarWashData data)
    : IRepository<Car, int>
{
    public Task<Car> Create(Car entity)
    {
        data.Cars.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<Car?> Read(int entityId)
    {
        return Task.FromResult<Car?>(
            data.Cars.FirstOrDefault(car => car.Id == entityId));
    }

    public Task<IList<Car>> ReadAll()
    {
        return Task.FromResult<IList<Car>>([.. data.Cars]);
    }

    public Task<Car> Update(Car entity)
    {
        var existingCar = data.Cars.FirstOrDefault(
            car => car.Id == entity.Id)
            ?? throw new KeyNotFoundException(
                $"Автомобиль с идентификатором {entity.Id} не найден");

        existingCar.CarCategory = entity.CarCategory;
        existingCar.LicensePlate = entity.LicensePlate;
        existingCar.Brand = entity.Brand;
        existingCar.CustomerId = entity.CustomerId;
        existingCar.Customer = entity.Customer;

        return Task.FromResult(existingCar);
    }

    public async Task<bool> Delete(int entityId)
    {
        var car = await Read(entityId);

        return car is not null && data.Cars.Remove(car);
    }
}