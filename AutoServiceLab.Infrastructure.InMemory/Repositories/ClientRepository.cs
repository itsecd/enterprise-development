using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory репозиторий клиентов
/// </summary>
public class ClientRepository : IClientRepository
{
    private readonly List<Client> _clients = new(AutoServiceData.Clients);

    /// <inheritdoc />
    public Task<Client?> GetAsync(int id) => Task.FromResult(_clients.FirstOrDefault(c => c.Id == id));

    /// <inheritdoc />
    public Task<IReadOnlyList<Client>> GetAllAsync() => Task.FromResult<IReadOnlyList<Client>>(_clients);

    /// <inheritdoc />
    public Task AddAsync(Client entity)
    {
        entity.Id = _clients.Count == 0 ? 1 : _clients.Max(c => c.Id) + 1;
        _clients.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task UpdateAsync(Client entity)
    {
        var index = _clients.FindIndex(c => c.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException($"Клиент {entity.Id} не найден");
        _clients[index] = entity;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var client = await GetAsync(id);
        return client != null && _clients.Remove(client);
    }
}
