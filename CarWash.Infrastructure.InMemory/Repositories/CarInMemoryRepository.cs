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
    public void Create(Car entity)
    {
        data.Cars.Add(entity);
    }

    public Car Read(int entityId)
    {
        return data.Cars.FirstOrDefault(car => car.Id == entityId)
        ?? throw new KeyNotFoundException(
            $"Автомобиль с идентификатором {entityId} не найден.");
    }

    public List<Car> ReadAll()
    {
        return [.. data.Cars];
    }

    public void Update(Car entity)
    {
        var existingCar = Read(entity.Id);

        existingCar.CarCategory = entity.CarCategory;
        existingCar.LicensePlate = entity.LicensePlate;
        existingCar.Brand = entity.Brand;
        existingCar.CustomerId = entity.CustomerId;
        existingCar.Customer = entity.Customer;
    }

    public void Delete(int entityId)
    {
        var car = Read(entityId);
        data.Cars.Remove(car);
    }
}