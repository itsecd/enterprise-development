using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Аналитические запросы фитнес-клуба
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Тренеры, стаж которых не менее заданного, упорядоченные по ФИО
    /// </summary>
    /// <param name="minYears">Минимальный стаж</param>
    public Task<IReadOnlyList<TrainerDto>> GetTrainersWithExperienceAsync(int minYears = 5);

    /// <summary>
    /// Клиенты с просроченным абонементом, упорядоченные по ФИО
    /// </summary>
    public Task<IReadOnlyList<ClientDto>> GetClientsWithExpiredSubscriptionAsync();

    /// <summary>
    /// Проверяет, доступен ли зал для записи в указанный момент времени
    /// </summary>
    public Task<HallAvailabilityDto> CheckHallAvailabilityAsync(string hall, DateTime time);

    /// <summary>
    /// Занятия за текущий месяц в выбранном зале, упорядоченные по дате
    /// </summary>
    public Task<IReadOnlyList<LessonDto>> GetHallLessonsForCurrentMonthAsync(string hall);

    /// <summary>
    /// Топ наиболее популярных тренеров по числу занятий
    /// </summary>
    /// <param name="count">Размер выборки</param>
    public Task<IReadOnlyList<PopularTrainerDto>> GetTopPopularTrainersAsync(int count = 5);
}