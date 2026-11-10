namespace BeautySalon.Domain.Entities;

/// <summary>
/// Запись
/// </summary>
public class Appointment
{

    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Услуга
    /// </summary>
    public required Service Service { get; set; }

    /// <summary>
    /// Мастер
    /// </summary>
    public required Master Master { get; set; }

    /// <summary>
    /// Дата и время
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Постоянный клиент
    /// </summary>
    public required bool IsRegular { get; set; }
}
