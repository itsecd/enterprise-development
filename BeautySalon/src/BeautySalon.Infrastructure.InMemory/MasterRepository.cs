using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// In-memory репозиторий мастеров
/// </summary>
public class MasterRepository(BeautySalonDataStore dataStore)
    : InMemoryRepository<Master>(dataStore.Masters), IMasterRepository;