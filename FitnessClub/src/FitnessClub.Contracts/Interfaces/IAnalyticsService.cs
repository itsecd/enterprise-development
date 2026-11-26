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
    /// <param name="minimumExperience">Минимальный стаж в годах</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список тренеров с указанным минимальным стажем</returns>
    public Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(
        int minimumExperience = 5,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить клиентов с просроченными абонементами на дату
    /// </summary>
    /// <param name="asOfDate">Дата проверки срока абонемента</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список клиентов с просроченными абонементами</returns>
    public Task<IReadOnlyList<ClientDto>> GetClientsWithOverdueSubscriptionsAsync(
        DateOnly asOfDate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить самых популярных тренеров
    /// </summary>
    /// <param name="count">Максимальное количество тренеров</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список тренеров, упорядоченный по популярности</returns>
    public Task<IReadOnlyList<PopularTrainerDto>> GetMostPopularTrainersAsync(
        int count = 5,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Проверить доступность зала в указанное время
    /// </summary>
    /// <param name="hallId">Идентификатор зала</param>
    /// <param name="dateTime">Дата и время проверки</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Признак доступности зала</returns>
    public Task<bool> IsHallAvailableAsync(int hallId, DateTime dateTime, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить бронирования зала за месяц
    /// </summary>
    /// <param name="hallId">Идентификатор зала</param>
    /// <param name="dateInMonth">Дата, определяющая месяц выборки</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список бронирований зала за месяц даты</returns>
    public Task<IReadOnlyList<BookingDto>> GetBookingsInHallForMonthAsync(
        int hallId,
        DateOnly dateInMonth,
        CancellationToken cancellationToken = default);
}
