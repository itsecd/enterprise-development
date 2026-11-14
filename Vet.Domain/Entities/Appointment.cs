namespace Vet.Domain.Entities;

/// <summary>
/// Запись на приём
/// </summary>
public sealed class Appointment
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Животное, записанное на приём
    /// </summary>
    public required Pet Pet { get; set; }

    /// <summary>
    /// Врач, к которому записан питомец
    /// </summary>
    public required Veterinarian Veterinarian { get; set; }

    /// <summary>
    /// Время приёма
    /// </summary>
    public required DateTime AppointmentDateTime { get; set; }

    /// <summary>
    /// Кабинет, в котором проводится приём
    /// </summary>
    public required int RoomNumber { get; set; }

    /// <summary>
    /// Флаг повторного приёма
    /// </summary>
    public bool IsRepeat { get; set; }
}