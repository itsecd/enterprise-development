using FitnessGym.Domain.Entities;

namespace FitnessGym.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с записями на занятия
/// </summary>
public interface ITrainingSessionRepository
{
    public Task<IReadOnlyList<TrainingSession>> GetAllAsync();
    public Task<TrainingSession?> GetByIdAsync(int id);
    public Task<TrainingSession> AddAsync(TrainingSession session);
    public Task<bool> DeleteAsync(int id);
}