namespace VeterinaryClinic.Application.Contracts.Appointments;

/// <summary>
/// Операции сервиса приёмов
/// </summary>
public interface IAppointmentService
{
    /// <summary>
    /// Возвращает все приёмы
    /// </summary>
    public IReadOnlyList<AppointmentDto> Appointments();

    /// <summary>
    /// Возвращает приём по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public AppointmentDto? Get(int id);

    /// <summary>
    /// Создаёт приём
    /// </summary>
    /// <param name="request">Данные запроса</param>
    public AppointmentDto Create(SaveAppointmentRequest request);

    /// <summary>
    /// Обновляет приём по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    public AppointmentDto Update(int id, SaveAppointmentRequest request);

    /// <summary>
    /// Удаляет приём по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public void DeleteAppointment(int id);

    /// <summary>
    /// Возвращает приёмы за месяц по кабинету
    /// </summary>
    /// <param name="room">Номер кабинета</param>
    /// <param name="today">Дата, относительно которой выполняется выборка</param>
    public IReadOnlyList<AppointmentDto> AppointmentsThisMonth(int room, DateTime today);
}
