using BeautySalon.Domain;
using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Репозиторий для клиентов
/// </summary>
public class ClientInMemoryRepository : IRepository<Client, int>
{
    private readonly List<Client> _clients = DataSeeder.Clients.ToList();
    private int _lastId = DataSeeder.Clients.Count + 1;

    /// <inheritdoc/>
    public Task<Client> Create(Client entity)
    {
        entity.Id = _lastId++;
        _clients.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    public Task<bool> Delete(int entityId)
    {
        var client = Read(entityId).Result;
        if (client == null)
            return Task.FromResult(false);

        var result = _clients.Remove(client);
        return Task.FromResult(result);
    }

    /// <inheritdoc/>
    public Task<List<Client>> ReadAll()
    {
        return Task.FromResult(_clients.ToList());
    }

    /// <inheritdoc/>
    public Task<Client?> Read(int entityId)
    {
        var client = _clients.FirstOrDefault(c => c.Id == entityId);
        return Task.FromResult(client);
    }

    /// <inheritdoc/>
    public Task<Client> Update(Client entity)
    {
        var client = Read(entity.Id).Result;

        if (client == null)
            return Create(entity);

        client.FullName = entity.FullName;
        client.Birthday = entity.Birthday;
        client.Gender = entity.Gender;
        client.PhoneNumber = entity.PhoneNumber;

        return Task.FromResult(client);
    }
}
