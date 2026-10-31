using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;
using CarWash.Infrastructure.InMemory.Data;

namespace CarWash.Infrastructure.InMemory.Repositories;

public class ClientRepository(InMemoryDataStore dataStore) : IClientRepository
{
    public Task<IEnumerable<Client>> GetAllAsync()
        => Task.FromResult<IEnumerable<Client>>(dataStore.Clients);

    public Task<Client?> GetByIdAsync(int id)
        => Task.FromResult(dataStore.Clients.FirstOrDefault(c => c.Id == id));

    public Task<Client> AddAsync(Client entity)
    {
        entity.Id = dataStore.Clients.Any() ? dataStore.Clients.Max(c => c.Id) + 1 : 1;
        dataStore.Clients.Add(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(Client entity)
    {
        var index = dataStore.Clients.FindIndex(c => c.Id == entity.Id);
        if (index != -1) dataStore.Clients[index] = entity;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var client = dataStore.Clients.FirstOrDefault(c => c.Id == id);
        if (client != null) dataStore.Clients.Remove(client);
        return Task.CompletedTask;
    }
}