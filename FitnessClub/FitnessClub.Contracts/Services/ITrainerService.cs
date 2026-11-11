using FitnessClub.Contracts.Dtos;

namespace FitnessClub.Contracts.Services;

/// <summary>
/// Определяет операции для работы с тренерами фитнес-клуба.
/// </summary>
public interface ITrainerService
{
    /// <summary>
    /// Возвращает список всех тренеров.
    /// </summary>
    public Task<IReadOnlyCollection<TrainerDto>> GetAllAsync();

    /// <summary>
    /// Возвращает тренера по ID.
    /// </summary>
    /// <param name="id">ID тренера.</param>
    /// <returns>Данные тренера или null, если он не найден.</returns>
    public Task<TrainerDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создаёт нового тренера.
    /// </summary>
    /// <param name="dto">Данные нового тренера.</param>
    /// <returns>Созданный тренер.</returns>
    public Task<TrainerDto> CreateAsync(CreateTrainerDto dto);

    /// <summary>
    /// Обновляет данные тренера.
    /// </summary>
    /// <param name="id">ID тренера.</param>
    /// <param name="dto">Новые данные тренера.</param>
    /// <returns>true, если тренер найден и обновлён.</returns>
    public Task<bool> UpdateAsync(int id, UpdateTrainerDto dto);

    /// <summary>
    /// Удаляет тренера.
    /// </summary>
    /// <param name="id">ID тренера.</param>
    /// <returns>true, если тренер найден и удалён.</returns>
    public Task<bool> DeleteAsync(int id);
}