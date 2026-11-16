using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Domain.Entities;
using EnterpriseDevelopment.Infrastructure.Storage;

namespace EnterpriseDevelopment.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для хранения и управления клиентами
/// </summary>
public class ClientRepository(InMemoryDataStore store)
    : Repository<Client>(
        store.Clients,
        client => client.Id,
        (client, id) => client.Id = id),
      IClientRepository;
