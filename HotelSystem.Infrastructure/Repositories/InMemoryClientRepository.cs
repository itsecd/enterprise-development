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
    public Task<List<Client>> GetAllAsync() =>
        Task.FromResult(storage.Clients);

    /// <inheritdoc />
    public Task<Client?> GetByIdAsync(int id) =>
        Task.FromResult(
            storage.Clients.FirstOrDefault(client => client.Id == id));

    /// <inheritdoc />
    public Task<Client> AddAsync(Client client)
    {
        client.Id = storage.Clients.Count == 0
            ? 1
            : storage.Clients.Max(existing => existing.Id) + 1;

        storage.Clients.Add(client);

        return Task.FromResult(client);
    }

    /// <inheritdoc />
    public Task<bool> UpdateAsync(int id, Client client)
    {
        var index = storage.Clients.FindIndex(existing => existing.Id == id);

        if (index == -1)
        {
            return Task.FromResult(false);
        }

        client.Id = id;
        storage.Clients[index] = client;

        return Task.FromResult(true);
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int id)
    {
        var client = storage.Clients.FirstOrDefault(
            existing => existing.Id == id);

        return Task.FromResult(
            client is not null && storage.Clients.Remove(client));
    }
}