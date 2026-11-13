using CarWash.Domain.Data;
using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;

namespace CarWash.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий, хранящий клиентов в памяти
/// </summary>
public class ClientRepository : IClientRepository
{
    private readonly List<Client> _clients = [.. DataSeed.Clients];

    /// <inheritdoc />
    public Task<IEnumerable<Client>> GetAllAsync()
        => Task.FromResult<IEnumerable<Client>>([.. _clients]);

    /// <inheritdoc />
    public Task<Client?> GetByIdAsync(int id)
        => Task.FromResult(_clients.FirstOrDefault(c => c.Id == id));

    /// <inheritdoc />
    public Task<Client> AddAsync(Client entity)
    {
        entity.Id = _clients.Count != 0 ? _clients.Max(c => c.Id) + 1 : 1;
        _clients.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc />
    public Task UpdateAsync(Client entity)
    {
        var index = _clients.FindIndex(c => c.Id == entity.Id);
        if (index != -1)
        {
            _clients[index] = entity;
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task DeleteAsync(int id)
    {
        var item = _clients.FirstOrDefault(c => c.Id == id);
        if (item != null)
        {
            _clients.Remove(item);
        }

        return Task.CompletedTask;
    }
}
