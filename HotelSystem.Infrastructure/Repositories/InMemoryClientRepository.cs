using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.Storage;

namespace HotelSystem.Infrastructure.Repositories;

/// <summary>
/// Репозиторий клиентов гостиницы с хранением данных в памяти
/// </summary>
public class InMemoryClientRepository(HotelInMemoryStorage storage)
    : IRepository<Client>
{
    /// <inheritdoc />
    public List<Client> GetAll() => storage.Clients;

    /// <inheritdoc />
    public Client? GetById(int id) =>
        storage.Clients.FirstOrDefault(client => client.Id == id);

    /// <inheritdoc />
    public Client Add(Client client)
    {
        client.Id = storage.Clients.Count == 0
            ? 1
            : storage.Clients.Max(existing => existing.Id) + 1;

        storage.Clients.Add(client);

        return client;
    }

    /// <inheritdoc />
    public bool Update(int id, Client client)
    {
        var index = storage.Clients.FindIndex(existing => existing.Id == id);

        if (index == -1)
        {
            return false;
        }

        client.Id = id;
        storage.Clients[index] = client;

        return true;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var client = GetById(id);

        return client is not null && storage.Clients.Remove(client);
    }
}