using FitnessGym.Domain.Entities;

namespace FitnessGym.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с записями на занятия
/// </summary>
public interface ITrainingSessionRepository : IRepository<TrainingSession> { }