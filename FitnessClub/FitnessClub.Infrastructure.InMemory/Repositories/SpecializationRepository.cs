using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.InMemory.Data;
using FitnessClub.Infrastructure.InMemory.Repositories.Base;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий специализаций с хранением данных в памяти.
/// </summary>
/// <param name="dataStore">Общее хранилище данных.</param>
public class SpecializationRepository(FitnessClubDataStore dataStore)
    : InMemoryRepository<Specialization>(dataStore.Specializations), ISpecializationRepository;
