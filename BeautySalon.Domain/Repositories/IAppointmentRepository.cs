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
    Appointment Add(AppointmentDetails details);

    /// <summary>
    /// Обновить запись по идентификаторам связанных сущностей
    /// </summary>
    bool Update(int id, AppointmentDetails details);

    /// <summary>
    /// Получить записи посетителя
    /// </summary>
    IReadOnlyList<Appointment> GetByCustomer(int customerId);

    /// <summary>
    /// Получить записи специалиста
    /// </summary>
    IReadOnlyList<Appointment> GetBySpecialist(int specialistId);

    /// <summary>
    /// Получить записи на услугу
    /// </summary>
    IReadOnlyList<Appointment> GetByService(int serviceId);

    /// <summary>
    /// Получить записи специалиста, пересекающие заданный период
    /// </summary>
    IReadOnlyList<Appointment> GetSchedule(int specialistId, DateTime from, DateTime to);

    /// <summary>
    /// Получить самые популярные услуги по количеству записей
    /// </summary>
    IReadOnlyList<ServicePopularity> GetPopularServices(int limit);

    /// <summary>
    /// Посчитать повторные записи с учётом истории до начала периода
    /// </summary>
    int CountRepeatAppointments(DateTime from, DateTime to);

    /// <summary>
    /// Получить посетителей нескольких специалистов по дате рождения
    /// </summary>
    IReadOnlyList<Customer> GetCustomersWithMultipleSpecialists();
}
