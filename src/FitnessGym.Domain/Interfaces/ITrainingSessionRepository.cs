using FitnessGym.Domain.Entities;

namespace FitnessGym.Domain.Interfaces;

/// <summary>
/// Репозиторий записей на занятия фитнес-клуба.
/// </summary>
public interface ITrainingSessionRepository : IRepository<TrainingSession>
{
    /// <summary>
    /// Возвращает занятия в зале за указанный месяц.
    /// </summary>
    public Task<IReadOnlyList<TrainingSession>> GetForMonthAsync(string hallName, DateTime month);

    /// <summary>
    /// Проверяет наличие занятий у клиента.
    /// </summary>
    public Task<bool> HasClientAsync(int clientId);

    /// <summary>
    /// Проверяет наличие занятий у тренера.
    /// </summary>
    public Task<bool> HasTrainerAsync(int trainerId);

    /// <summary>
    /// Возвращает наиболее популярных тренеров по числу занятий.
    /// </summary>
    public Task<IReadOnlyList<Trainer>> GetPopularTrainersAsync(int count);

    /// <summary>
    /// Возвращает названия залов из записей на занятия.
    /// </summary>
    public Task<IReadOnlyList<string>> GetHallsAsync();

    /// <summary>
    /// Проверяет доступность зала; занятия без времени окончания не учитываются.
    /// </summary>
    public Task<bool> IsHallAvailableAsync(string hallName, DateTime at);
}
