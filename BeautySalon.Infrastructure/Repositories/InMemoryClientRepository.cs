using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.Repositories;

/// <summary>
/// In memory реализация репозитория клиентов.
/// </summary>
public class InMemoryClientRepository : IClientRepository
{
    private readonly List<Client> _clients;

    /// <summary>
    /// Инициализирует репозиторий, копируя данные из SeederData.
    /// </summary>
    public InMemoryClientRepository()
    {
        _clients = SeederData.Clients.ToList();
    }

    /// <inheritdoc />
    public Task<IEnumerable<Client>> GetAllAsync() => Task.FromResult<IEnumerable<Client>>(_clients);

    /// <inheritdoc />
    public Task<Client?> GetByIdAsync(int id) => Task.FromResult(_clients.FirstOrDefault(c => c.Id == id));

    /// <inheritdoc />
    public Task AddAsync(Client entity)
    {
        entity.Id = _clients.Count > 0 ? _clients.Max(c => c.Id) + 1 : 1;
        _clients.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task UpdateAsync(Client entity) => Task.CompletedTask;

    /// <inheritdoc />
    public Task DeleteAsync(Client entity)
    {
        _clients.Remove(entity);
        return Task.CompletedTask;
    }
}