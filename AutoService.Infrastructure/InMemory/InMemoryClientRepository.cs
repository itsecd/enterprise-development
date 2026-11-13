using AutoService.Domain.Data;
using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;

namespace AutoService.Infrastructure.InMemory;

/// <summary>
/// Реализация репозитория клиентов в памяти.
/// </summary>
public class InMemoryClientRepository : IClientRepository
{
    private readonly List<Client> _clients = DataSeed.Clients.ToList();

    /// <summary>
    /// Получить список всех клиентов.
    /// </summary>
    public Task<IEnumerable<Client>> GetAllAsync() => Task.FromResult(_clients.AsEnumerable());

    /// <summary>
    /// Получить клиента по идентификатору.
    /// </summary>
    public Task<Client?> GetByIdAsync(Guid id) => Task.FromResult(_clients.FirstOrDefault(c => c.Id == id));

    /// <summary>
    /// Добавить клиента.
    /// </summary>
    public Task AddAsync(Client client)
    {
        _clients.Add(client);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновить данные клиента.
    /// </summary>
    public Task UpdateAsync(Client client)
    {
        var index = _clients.FindIndex(c => c.Id == client.Id);
        if (index != -1) _clients[index] = client;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Удалить клиента.
    /// </summary>
    public Task DeleteAsync(Client client)
    {
        _clients.Remove(client);
        return Task.CompletedTask;
    }
}