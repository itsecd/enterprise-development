using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.InMemory.Data;
using FitnessClub.Infrastructure.InMemory.Repositories.Base;

namespace FitnessClub.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий тренеров с хранением данных в памяти.
/// </summary>
/// <param name="dataStore">Общее хранилище данных.</param>
public class TrainerRepository(FitnessClubDataStore dataStore)
    : InMemoryRepository<Trainer>(dataStore.Trainers), ITrainerRepository;
