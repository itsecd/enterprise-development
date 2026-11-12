namespace BeautySalon.Domain.Entities;

/// <summary>
/// Запись посетителя на услугу к специалисту
/// </summary>
public class Appointment
{
    /// <summary>
    /// Уникальный идентификатор записи
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Посетитель
    /// </summary>
    public required Customer Customer { get; set; }

    /// <summary>
    /// Специалист
    /// </summary>
    public required BeautySpecialist Specialist { get; set; }

    /// <summary>
    /// Услуга
    /// </summary>
    public required Service Service { get; set; }

    /// <summary>
    /// Дата и время начала услуги
    /// </summary>
    public required DateTime AppointmentDateTime { get; set; }

    /// <summary>
    /// Признак постоянного посетителя
    /// </summary>
    public bool IsRegularCustomer { get; set; }
}
