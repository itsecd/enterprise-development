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
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task<IReadOnlyList<AppointmentResponse>> GetByCustomerAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить записи специалиста
    /// </summary>
    /// <param name="id">Идентификатор специалиста</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task<IReadOnlyList<AppointmentResponse>> GetBySpecialistAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить записи на услугу
    /// </summary>
    /// <param name="id">Идентификатор услуги</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task<IReadOnlyList<AppointmentResponse>> GetByServiceAsync(int id, CancellationToken cancellationToken = default);
}
