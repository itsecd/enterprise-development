using FitnessGym.Contracts.Dtos;

namespace FitnessGym.Contracts.Interfaces;

/// <summary>
/// Сервис для управления записями клиентов на персональные занятия
/// </summary>
public interface ITrainingSessionService
{
    /// <summary>
    /// Получить список всех занятий
    /// </summary>
    /// <returns>Коллекция DTO занятий.</returns>
    public Task<IReadOnlyList<TrainingSessionDto>> GetAllAsync();

    /// <summary>
    /// Получить занятие по его идентификатору
    /// </summary>
    public Task<TrainingSessionDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создать новую запись на занятие
    /// </summary>
    public Task<TrainingSessionDto> CreateAsync(TrainingSessionDto dto);

    /// <summary>
    /// Удалить запись о занятии по идентификатору
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}