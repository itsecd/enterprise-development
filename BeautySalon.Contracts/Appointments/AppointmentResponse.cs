namespace BeautySalon.Contracts.Appointments;

/// <summary>
/// Данные записи клиента к мастеру.
/// </summary>
public class AppointmentResponse
{
    /// <summary>
    /// Идентификатор записи.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата и время начала процедуры.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Является ли клиент постоянным.
    /// </summary>
    public bool IsRegularClient { get; set; }

    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Идентификатор мастера.
    /// </summary>
    public int MasterId { get; set; }

    /// <summary>
    /// Идентификатор услуги.
    /// </summary>
    public int ServiceId { get; set; }
}