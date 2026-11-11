using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.InMemory.Data;
using FitnessClub.Infrastructure.InMemory.Repositories.Base;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий тренировок с хранением данных в памяти.
/// </summary>
/// <param name="dataStore">Общее хранилище данных.</param>
public class TrainingRepository(FitnessClubDataStore dataStore)
    : InMemoryRepository<Training>(dataStore.Trainings), ITrainingRepository;
