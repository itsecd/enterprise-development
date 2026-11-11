using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.InMemory.Data;
using FitnessClub.Infrastructure.InMemory.Repositories.Base;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий клиентов с хранением данных в памяти.
/// </summary>
/// <param name="dataStore">Общее хранилище данных.</param>
public class ClientRepository(FitnessClubDataStore dataStore)
    : InMemoryRepository<Client>(dataStore.Clients), IClientRepository;
