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
    public IEnumerable<Client> GetAll() => _clients;

    /// <inheritdoc />
    public Client? GetById(int id) => _clients.FirstOrDefault(c => c.Id == id);

    /// <inheritdoc />
    public void Add(Client client)
    {
        client.Id = _clients.Count > 0 ? _clients.Max(c => c.Id) + 1 : 1;
        _clients.Add(client);
    }

    /// <inheritdoc />
    public void Update(Client client) { }

    /// <inheritdoc />
    public void Delete(Client client) => _clients.Remove(client);
}