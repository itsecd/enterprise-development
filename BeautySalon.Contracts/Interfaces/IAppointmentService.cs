using BeautySalon.Contracts.Appointments;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Контракт сервиса AppointmentService
/// </summary>
public interface IAppointmentService : ICrudService<SaveAppointmentRequest, AppointmentResponse>
{
    /// <summary>
    /// Получить записи посетителя
    /// </summary>
    /// <param name="id">Идентификатор посетителя</param>
    IReadOnlyList<AppointmentResponse> GetByCustomer(int id);

    /// <summary>
    /// Получить записи специалиста
    /// </summary>
    /// <param name="id">Идентификатор специалиста</param>
    IReadOnlyList<AppointmentResponse> GetBySpecialist(int id);

    /// <summary>
    /// Получить записи на услугу
    /// </summary>
    /// <param name="id">Идентификатор услуги</param>
    IReadOnlyList<AppointmentResponse> GetByService(int id);
}
