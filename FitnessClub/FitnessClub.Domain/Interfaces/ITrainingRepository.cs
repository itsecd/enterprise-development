using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория занятий фитнес-клуба.
/// </summary>
public interface ITrainingRepository : IRepository<Training>;