namespace VetClinic.Contracts.Dto.Appointments;

/// <summary>
/// Запись питомца на прием к врачу
/// </summary>
public class AppointmentDto
{
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Дата и время приема
    /// </summary>
    public required DateTime DateTime { get; init; }

    /// <summary>
    /// Номер кабинета
    /// </summary>
    public required string RoomNumber { get; init; }

    /// <summary>
    /// Повторный прием (true) или первичный (false)
    /// </summary>
    public required bool IsRepeat { get; init; }

    /// <summary>
    /// Идентификатор питомца
    /// </summary>
    public required int PetId { get; init; }

    /// <summary>
    /// Идентификатор врача
    /// </summary>
    public required int VetId { get; init; }
}
