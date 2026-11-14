using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория тренеров фитнес-клуба.
/// </summary>
public interface ITrainerRepository : IRepository<Trainer>;