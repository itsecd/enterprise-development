using FitnessGym.Domain.Entities;

namespace FitnessGym.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с записями на занятия
/// </summary>
public interface ITrainingSessionRepository
{
    /// <summary>
    /// Получить список всех занятий
    /// </summary>
    public Task<IReadOnlyList<TrainingSession>> GetAllAsync();

    /// <summary>
    /// Получить занятие по идентификатору
    /// </summary>
    public Task<TrainingSession?> GetByIdAsync(int id);

    /// <summary>
    /// Добавить новую запись на занятие
    /// </summary>
    public Task<TrainingSession> AddAsync(TrainingSession session);

    /// <summary>
    /// Обновить данные записи на занятие
    /// </summary>
    public Task<bool> UpdateAsync(TrainingSession session);

    /// <summary>
    /// Удалить запись о занятии
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}