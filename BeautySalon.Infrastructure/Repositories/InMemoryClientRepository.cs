using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.Repositories;

/// <summary>
/// In memory реализация репозитория клиентов.
/// </summary>
public class InMemoryClientRepository : IClientRepository
{
    public IEnumerable<Client> GetAll() => SeederData.Clients;

    public Client? GetById(int id) => SeederData.Clients.FirstOrDefault(c => c.Id == id);

    public void Add(Client client)
    {
        client.Id = SeederData.Clients.Count > 0 ? SeederData.Clients.Max(c => c.Id) + 1 : 1;
        SeederData.Clients.Add(client);
    }

    public void Update(Client client) { }

    public void Delete(Client client) => SeederData.Clients.Remove(client);
}