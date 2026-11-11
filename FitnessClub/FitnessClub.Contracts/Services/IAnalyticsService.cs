using FitnessClub.Contracts.Dtos;

namespace FitnessClub.Contracts.Services;

/// <summary>
/// Определяет аналитические операции фитнес-клуба.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Возвращает тренеров со стажем не менее пяти лет.
    /// </summary>
    public Task<IReadOnlyCollection<TrainerDto>> GetExperiencedTrainersAsync();

    /// <summary>
    /// Проверяет, свободен ли зал в текущий момент.
    /// </summary>
    /// <param name="hallId">ID зала.</param>
    public Task<HallAvailabilityDto?> CheckHallAvailabilityAsync(int hallId);

    /// <summary>
    /// Возвращает клиентов с истёкшими абонементами,
    /// отсортированных по ФИО.
    /// </summary>
    public Task<IReadOnlyCollection<ClientDto>> GetClientsWithExpiredSubscriptionsAsync();

    /// <summary>
    /// Возвращает тренировки выбранного зала за текущий календарный месяц.
    /// </summary>
    /// <param name="hallId">ID зала.</param>
    public Task<IReadOnlyCollection<TrainingDto>> GetCurrentMonthTrainingsByHallAsync(
        int hallId);

    /// <summary>
    /// Возвращает пять тренеров с наибольшим количеством тренировок.
    /// </summary>
    public Task<IReadOnlyCollection<TrainerDto>> GetTopFiveTrainersAsync();
}