using BeautySalon.Domain.Entities;

namespace BeautySalon.Domain.Repositories;

/// <summary>
/// Хранение записей
/// </summary>
public interface IAppointmentRepository : IRepository<Appointment>
{
    /// <summary>
    /// Создать запись по идентификаторам связанных сущностей
    /// </summary>
    Task<Appointment> AddAsync(AppointmentDetails details, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить запись по идентификаторам связанных сущностей
    /// </summary>
    Task<bool> UpdateAsync(int id, AppointmentDetails details, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить записи посетителя
    /// </summary>
    Task<IReadOnlyList<Appointment>> GetByCustomerAsync(int customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить записи специалиста
    /// </summary>
    Task<IReadOnlyList<Appointment>> GetBySpecialistAsync(int specialistId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить записи на услугу
    /// </summary>
    Task<IReadOnlyList<Appointment>> GetByServiceAsync(int serviceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Найти свободные промежутки между записями специалиста
    /// </summary>
    Task<IReadOnlyList<ScheduleWindow>> GetFreeWindowsAsync(int specialistId, DateTime from, DateTime to, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить самые популярные услуги по количеству записей
    /// </summary>
    Task<IReadOnlyList<ServicePopularity>> GetPopularServicesAsync(int limit, CancellationToken cancellationToken = default);

    /// <summary>
    /// Посчитать повторные записи с учётом истории до начала периода
    /// </summary>
    Task<int> CountRepeatAppointmentsAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить посетителей нескольких специалистов по дате рождения
    /// </summary>
    Task<IReadOnlyList<Customer>> GetCustomersWithMultipleSpecialistsAsync(CancellationToken cancellationToken = default);
}
