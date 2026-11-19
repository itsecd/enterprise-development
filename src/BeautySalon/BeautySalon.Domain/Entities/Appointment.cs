namespace BeautySalon.Domain.Entities;

/// <summary>
/// Запись клиента на услугу к мастеру.
/// </summary>
public class Appointment
{
    /// <summary>
    /// Уникальный идентификатор записи.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Клиент, который записался.
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Мастер, к которому записались.
    /// </summary>
    public required Master Master { get; set; }

    /// <summary>
    /// Услуга, на которую записались.
    /// </summary>
    public required Service Service { get; set; }

    /// <summary>
    /// Дата и время записи.
    /// </summary>
    public required DateTime AppointmentDate { get; set; }

    /// <summary>
    /// Индикатор того, является ли клиент постоянным на момент записи.
    /// </summary>
    public required bool IsRegularClient { get; set; }

    /// <summary>
    /// Дата и время окончания сеанса 
    /// (вычисляется как начало сеанса + длительность услуги).
    /// </summary>
    public DateTime EndAppointmentDate => AppointmentDate + Service.Duration;
}
