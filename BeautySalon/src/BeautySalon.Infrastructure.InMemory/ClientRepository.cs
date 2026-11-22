using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// In-memory репозиторий клиентов
/// </summary>
public class ClientRepository(BeautySalonDataStore dataStore)
    : InMemoryRepository<Client>(dataStore.Clients), IClientRepository;