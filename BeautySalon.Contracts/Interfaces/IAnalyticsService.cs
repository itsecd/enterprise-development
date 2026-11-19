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
    IReadOnlyList<SpecialistResponse> GetExperiencedSpecialists();

    /// <summary>
    /// Получить промежутки между записями специалиста за день
    /// </summary>
    /// <param name="specialistId">Идентификатор специалиста</param>
    /// <param name="date">День расписания</param>
    IReadOnlyList<FreeWindowResponse> GetFreeWindows(
        int specialistId, DateOnly date);

    /// <summary>
    /// Получить пять услуг с наибольшим количеством записей
    /// </summary>
    IReadOnlyList<PopularServiceResponse> GetPopularServices();

    /// <summary>
    /// Посчитать повторные записи за месяц с учётом всей истории посетителя
    /// </summary>
    /// <param name="asOf">Конец периода по местному времени салона</param>
    RepeatAppointmentsResponse GetRepeatAppointments(DateTime asOf);

    /// <summary>
    /// Получить посетителей нескольких специалистов по дате рождения
    /// </summary>
    IReadOnlyList<CustomerResponse> GetCustomersWithMultipleSpecialists();
}