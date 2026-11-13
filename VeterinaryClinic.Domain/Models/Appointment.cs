namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Запись питомца на приём
/// </summary>
public class Appointment
{
    /// <summary>
    /// Идентификатор приёма
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Питомец на приёме
    /// </summary>
    public required Pet Pet { get; set; }

    /// <summary>
    /// Ветеринар на приёме
    /// </summary>
    public required Veterinarian Veterinarian { get; set; }

    /// <summary>
    /// Дата и время приёма
    /// </summary>
    public DateTime AppointmentDate { get; set; }

    /// <summary>
    /// Номер кабинета
    /// </summary>
    public int RoomNumber { get; set; }

    /// <summary>
    /// Признак повторного приёма
    /// </summary>
    public bool IsRepeated { get; set; }
}
