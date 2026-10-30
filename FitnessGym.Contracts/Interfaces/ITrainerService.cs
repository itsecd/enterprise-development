using FitnessGym.Contracts.Dtos;

namespace FitnessGym.Contracts.Interfaces;

/// <summary>
/// Сервис для управления тренерами фитнес-клуба
/// </summary>
public interface ITrainerService
{
    /// <summary>
    /// Получить список всех тренеров
    /// </summary>
    public Task<IReadOnlyList<TrainerDto>> GetAllAsync();

    /// <summary>
    /// Получить тренера по его идентификатору
    /// </summary>
    public Task<TrainerDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создать нового тренера
    /// </summary>
    public Task<TrainerDto> CreateAsync(TrainerEditDto dto);

    /// <summary>
    /// Обновить данные существующего тренера
    /// </summary>
    public Task<bool> UpdateAsync(int id, TrainerEditDto dto);

    /// <summary>
    /// Удалить тренера по идентификатору
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}