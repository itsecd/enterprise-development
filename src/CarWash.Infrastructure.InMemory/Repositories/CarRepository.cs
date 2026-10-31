using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;
using CarWash.Infrastructure.InMemory.Data;

namespace CarWash.Infrastructure.InMemory.Repositories;

public class CarRepository(InMemoryDataStore dataStore) : ICarRepository
{
    public Task<IEnumerable<Car>> GetAllAsync()
        => Task.FromResult<IEnumerable<Car>>(dataStore.Cars);

    public Task<Car?> GetByIdAsync(int id)
        => Task.FromResult(dataStore.Cars.FirstOrDefault(c => c.Id == id));

    public Task<Car> AddAsync(Car entity)
    {
        entity.Id = dataStore.Cars.Any() ? dataStore.Cars.Max(c => c.Id) + 1 : 1;
        dataStore.Cars.Add(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(Car entity)
    {
        var index = dataStore.Cars.FindIndex(c => c.Id == entity.Id);
        if (index != -1) dataStore.Cars[index] = entity;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var car = dataStore.Cars.FirstOrDefault(c => c.Id == id);
        if (car != null) dataStore.Cars.Remove(car);
        return Task.CompletedTask;
    }
}