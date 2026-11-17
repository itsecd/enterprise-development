using FitnessGym.Contracts.Dto;

namespace FitnessGym.Contracts.Interfaces;

/// <summary>
/// Сервис записей на занятия фитнес-клуба.
/// </summary>
public interface ITrainingSessionService
{
    /// <summary>
    /// Возвращает список всех записей на занятия.
    /// </summary>
    public Task<IReadOnlyList<TrainingSessionDto>> GetSessionsAsync();

    /// <summary>
    /// Возвращает запись на занятие по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    public Task<TrainingSessionDto?> GetSessionAsync(int id);

    /// <summary>
    /// Добавляет запись на занятие и возвращает созданную сущность с присвоенным идентификатором.
    /// </summary>
    /// <param name="session">Данные записи.</param>
    public Task<TrainingSessionDto> AddSessionAsync(UpsertTrainingSessionDto session);

    /// <summary>
    /// Заменяет запись на занятие с указанным идентификатором.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <param name="session">Новые данные записи.</param>
    /// <returns>Обновлённая запись или null, если она не найдена.</returns>
    public Task<TrainingSessionDto?> UpdateSessionAsync(int id, UpsertTrainingSessionDto session);

    /// <summary>
    /// Удаляет запись на занятие по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <returns>true, если запись найдена и удалена.</returns>
    public Task<bool> DeleteSessionAsync(int id);

    /// <summary>
    /// Возвращает занятия в выбранном зале за текущий месяц.
    /// </summary>
    /// <param name="hallName">Название зала.</param>
    public Task<IReadOnlyList<TrainingSessionDto>> GetSessionsForCurrentMonthAsync(string hallName);
}
