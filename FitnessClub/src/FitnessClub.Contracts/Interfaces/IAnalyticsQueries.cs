using FitnessClub.Contracts.Dtos.Analytics;
using FitnessClub.Contracts.Dtos.Read;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Предоставляет запросы для получения аналитических данных
/// </summary>
public interface IAnalyticsQueries
{
    /// <summary>
    /// Получает тренеров, стаж которых не меньше заданного.
    /// </summary>
    /// <param name="minimumExperience">Минимальный стаж тренера в годах</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список тренеров, соответствующих минимальному стажу</returns>
    public Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(
        int minimumExperience,
        CancellationToken cancellationToken);

    /// <summary>
    /// Получает клиентов с абонементом, срок действия которого истёк до указанной даты
    /// </summary>
    /// <param name="asOfDate">Дата, относительно которой проверяется срок действия абонемента</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список клиентов с просроченными абонементами</returns>
    public Task<IReadOnlyList<ClientDto>> GetOverdueClientsAsync(
        DateOnly asOfDate,
        CancellationToken cancellationToken);

    /// <summary>
    /// Получает тренеров с наибольшим количеством бронирований
    /// </summary>
    /// <param name="count">Количество тренеров, которое мы хотим получить в результате</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список популярных тренеров с количеством их бронирований</returns>
    public Task<IReadOnlyList<PopularTrainerDto>> GetPopularTrainersAsync(
        int count,
        CancellationToken cancellationToken);

    /// <summary>
    /// Проверяет, свободен ли зал в указанные дату и время
    /// </summary>
    /// <param name="hallId">Идентификатор зала</param>
    /// <param name="dateTime">Дата и время проверки</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>
    /// <see langword="true"/>, если на указанное время нет бронирования зала;
    /// иначе <see langword="false"/>
    /// </returns>
    public Task<bool> IsHallAvailableAsync(
        int hallId,
        DateTime dateTime,
        CancellationToken cancellationToken);

    /// <summary>
    /// Получает бронирования зала за календарный месяц, указанный датой.
    /// </summary>
    /// <param name="hallId">Идентификатор зала</param>
    /// <param name="dateInMonth"> Месяц и год выборки</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    /// <returns>Список бронирований зала за указанный месяц</returns>
    public Task<IReadOnlyList<BookingDto>> GetHallBookingsForMonthAsync(
        int hallId,
        DateOnly dateInMonth,
        CancellationToken cancellationToken);
}