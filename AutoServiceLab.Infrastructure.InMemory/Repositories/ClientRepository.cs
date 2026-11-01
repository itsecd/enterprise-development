using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory репозиторий клиентов
/// </summary>
public class ClientRepository : IClientRepository
{
    /// <inheritdoc />
    public Client? Get(int id) => AutoServiceData.Clients.FirstOrDefault(c => c.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<Client> GetAll() => AutoServiceData.Clients;

    /// <inheritdoc />
    public void Add(Client entity)
    {
        entity.Id = AutoServiceData.Clients.Count == 0 ? 1 : AutoServiceData.Clients.Max(c => c.Id) + 1;
        AutoServiceData.Clients.Add(entity);
    }

    /// <inheritdoc />
    public void Update(Client entity)
    {
        var index = AutoServiceData.Clients.FindIndex(c => c.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException($"Клиент {entity.Id} не найден");
        AutoServiceData.Clients[index] = entity;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var client = Get(id);
        return client != null && AutoServiceData.Clients.Remove(client);
    }
}
