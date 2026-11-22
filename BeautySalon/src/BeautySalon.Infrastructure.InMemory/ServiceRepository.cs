using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// In-memory репозиторий услуг
/// </summary>
public class ServiceRepository(BeautySalonDataStore dataStore)
    : InMemoryRepository<Service>(dataStore.Services), IServiceRepository;