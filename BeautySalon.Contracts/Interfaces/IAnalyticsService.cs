using BeautySalon.Contracts.Analytics;
using BeautySalon.Contracts.Customers;
using BeautySalon.Contracts.Specialists;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Контракт сервиса AnalyticsService
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получить специалистов со стажем не менее пяти лет
    /// </summary>
    Task<IReadOnlyList<SpecialistResponse>> GetExperiencedSpecialistsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить промежутки между записями специалиста за день
    /// </summary>
    /// <param name="specialistId">Идентификатор специалиста</param>
    /// <param name="date">День расписания</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task<IReadOnlyList<FreeWindowResponse>> GetFreeWindowsAsync(
        int specialistId, DateOnly date, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить пять услуг с наибольшим количеством записей
    /// </summary>
    Task<IReadOnlyList<PopularServiceResponse>> GetPopularServicesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Посчитать повторные записи за месяц с учётом всей истории посетителя
    /// </summary>
    /// <param name="asOf">Конец периода по местному времени салона</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task<RepeatAppointmentsResponse> GetRepeatAppointmentsAsync(DateTime asOf, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить посетителей нескольких специалистов по дате рождения
    /// </summary>
    Task<IReadOnlyList<CustomerResponse>> GetCustomersWithMultipleSpecialistsAsync(CancellationToken cancellationToken = default);
}
