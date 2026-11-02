using FitnessClub.Contracts.Dtos.Analytics;
using FitnessClub.Contracts.Dtos.Read;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Операции получения аналитических данных
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получить тренеров с минимальным стажем
    /// </summary>
    /// <param name="minimumExperience">
    /// Минимальный стаж в годах
    /// </param>
    /// <returns>
    /// Список тренеров с указанным минимальным стажем
    /// </returns>
    public Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(
        int minimumExperience = 5);

    /// <summary>
    /// Получить клиентов с просроченными абонементами на дату
    /// </summary>
    /// <param name="asOfDate">
    /// Дата проверки срока абонемента
    /// </param>
    /// <returns>
    /// Список клиентов с просроченными абонементами
    /// </returns>
    public Task<IReadOnlyList<ClientDto>> GetClientsWithOverdueSubscriptionsAsync(
        DateOnly asOfDate);

    /// <summary>
    /// Получить самых популярных тренеров
    /// </summary>
    /// <param name="count">
    /// Максимальное количество тренеров
    /// </param>
    /// <returns>
    /// Список тренеров, упорядоченный по популярности
    /// </returns>
    public Task<IReadOnlyList<PopularTrainerDto>> GetMostPopularTrainersAsync(
        int count = 5);

    /// <summary>
    /// Проверить доступность зала в указанное время
    /// </summary>
    /// <param name="hallId">
    /// Идентификатор зала
    /// </param>
    /// <param name="dateTime">
    /// Дата и время проверки
    /// </param>
    /// <returns>
    /// Признак доступности зала
    /// </returns>
    public Task<bool> IsHallAvailableAsync(int hallId, DateTime dateTime);

    /// <summary>
    /// Получить бронирования зала за месяц
    /// </summary>
    /// <param name="hallId">
    /// Идентификатор зала
    /// </param>
    /// <param name="dateInMonth">
    /// Дата, определяющая месяц выборки
    /// </param>
    /// <returns>
    /// Список бронирований зала за месяц даты
    /// </returns>
    public Task<IReadOnlyList<BookingDto>> GetBookingsInHallForMonthAsync(
        int hallId,
        DateOnly dateInMonth);
}
