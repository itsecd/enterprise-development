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
    public Client? Get(int id) => _clients.FirstOrDefault(c => c.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<Client> GetAll() => _clients;

    /// <inheritdoc />
    public void Add(Client entity)
    {
        entity.Id = _clients.Count == 0 ? 1 : _clients.Max(c => c.Id) + 1;
        _clients.Add(entity);
    }

    /// <inheritdoc />
    public void Update(Client entity)
    {
        var index = _clients.FindIndex(c => c.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException($"Клиент {entity.Id} не найден");
        _clients[index] = entity;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var client = Get(id);
        return client != null && _clients.Remove(client);
    }
}
