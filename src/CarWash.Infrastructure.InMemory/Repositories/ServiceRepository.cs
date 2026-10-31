using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;
using CarWash.Infrastructure.InMemory.Data;

namespace CarWash.Infrastructure.InMemory.Repositories;

public class ServiceRepository(InMemoryDataStore dataStore) : IServiceRepository
{
    public Task<IEnumerable<Service>> GetAllAsync()
        => Task.FromResult<IEnumerable<Service>>(dataStore.Services);

    public Task<Service?> GetByIdAsync(int id)
        => Task.FromResult(dataStore.Services.FirstOrDefault(s => s.Id == id));

    public Task<Service> AddAsync(Service entity)
    {
        entity.Id = dataStore.Services.Any() ? dataStore.Services.Max(s => s.Id) + 1 : 1;
        dataStore.Services.Add(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(Service entity)
    {
        var index = dataStore.Services.FindIndex(s => s.Id == entity.Id);
        if (index != -1) dataStore.Services[index] = entity;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var service = dataStore.Services.FirstOrDefault(s => s.Id == id);
        if (service != null) dataStore.Services.Remove(service);
        return Task.CompletedTask;
    }
}