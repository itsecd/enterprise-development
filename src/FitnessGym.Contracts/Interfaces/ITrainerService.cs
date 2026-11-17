using FitnessGym.Contracts.Dto;

namespace FitnessGym.Contracts.Interfaces;

/// <summary>
/// Сервис тренеров фитнес-клуба.
/// </summary>
public interface ITrainerService
{
    /// <summary>
    /// Возвращает список всех тренеров.
    /// </summary>
    public Task<IReadOnlyList<TrainerDto>> GetTrainersAsync();

    /// <summary>
    /// Возвращает тренера по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор тренера.</param>
    public Task<TrainerDto?> GetTrainerAsync(int id);

    /// <summary>
    /// Добавляет тренера и возвращает созданную сущность с присвоенным идентификатором.
    /// </summary>
    /// <param name="trainer">Данные тренера.</param>
    public Task<TrainerDto> AddTrainerAsync(UpsertTrainerDto trainer);

    /// <summary>
    /// Заменяет тренера с указанным идентификатором.
    /// </summary>
    /// <param name="id">Идентификатор тренера.</param>
    /// <param name="trainer">Новые данные тренера.</param>
    /// <returns>Обновлённый тренер или null, если он не найден.</returns>
    public Task<TrainerDto?> UpdateTrainerAsync(int id, UpsertTrainerDto trainer);

    /// <summary>
    /// Удаляет тренера по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор тренера.</param>
    /// <returns>true, если тренер найден и удалён.</returns>
    public Task<bool> DeleteTrainerAsync(int id);

    /// <summary>
    /// Возвращает тренеров, стаж работы которых не менее указанного количества лет.
    /// </summary>
    /// <param name="minYears">Минимальный стаж работы в годах.</param>
    public Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(int minYears);

    /// <summary>
    /// Возвращает указанное количество наиболее популярных тренеров
    /// по числу записанных занятий, упорядоченных по убыванию популярности.
    /// </summary>
    /// <param name="count">Количество тренеров в результате.</param>
    public Task<IReadOnlyList<TrainerDto>> GetPopularTrainersAsync(int count);
}
