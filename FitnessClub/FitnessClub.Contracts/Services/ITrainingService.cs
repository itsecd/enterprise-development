using FitnessClub.Contracts.Dtos;

namespace FitnessClub.Contracts.Services;

/// <summary>
/// Определяет операции для работы с записями на тренировки.
/// </summary>
public interface ITrainingService
{
    /// <summary>
    /// Возвращает все записи на тренировки.
    /// </summary>
    public Task<IReadOnlyCollection<TrainingDto>> GetAllAsync();

    /// <summary>
    /// Возвращает запись по ID.
    /// </summary>
    /// <param name="id">ID записи.</param>
    public Task<TrainingDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создаёт запись на тренировку.
    /// </summary>
    /// <param name="dto">Данные новой записи.</param>
    public Task<TrainingDto> CreateAsync(CreateTrainingDto dto);

    /// <summary>
    /// Обновляет запись на тренировку.
    /// </summary>
    /// <param name="id">ID записи.</param>
    /// <param name="dto">Новые данные записи.</param>
    public Task<bool> UpdateAsync(int id, UpdateTrainingDto dto);

    /// <summary>
    /// Удаляет запись на тренировку.
    /// </summary>
    /// <param name="id">ID записи.</param>
    public Task<bool> DeleteAsync(int id);
}