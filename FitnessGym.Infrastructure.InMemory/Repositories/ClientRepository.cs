using FitnessGym.Domain.Data;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализация репозитория клиентов в памяти
/// </summary>
public class ClientRepository : IClientRepository
{
    private readonly List<Client> _clients = new(DataSeeder.Clients);

    /// <summary>
    /// Получить список всех клиентов
    /// </summary>
    public Task<IReadOnlyList<Client>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Client>>(_clients);
    }

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    public Task<Client?> GetByIdAsync(int id)
    {
        return Task.FromResult(_clients.FirstOrDefault(c => c.Id == id));
    }

    /// <summary>
    /// Добавить нового клиента
    /// </summary>
    public Task<Client> AddAsync(Client client)
    {
        var newId = _clients.Count > 0 ? _clients.Max(c => c.Id) + 1 : 0;
        client.Id = newId;
        _clients.Add(client);
        return Task.FromResult(client);
    }

    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    public Task<bool> UpdateAsync(Client client)
    {
        var existing = _clients.FirstOrDefault(c => c.Id == client.Id);
        if (existing is null) return Task.FromResult(false);

        return Task.FromResult(true);
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var client = _clients.FirstOrDefault(c => c.Id == id);
        if (client is null) return Task.FromResult(false);

        _clients.Remove(client);
        return Task.FromResult(true);
    }
}