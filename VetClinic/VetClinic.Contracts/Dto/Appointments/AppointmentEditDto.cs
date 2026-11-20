using System.ComponentModel.DataAnnotations;

namespace VetClinic.Contracts.Dto.Appointments;

/// <summary>
/// Данные записи на прием для создания и изменения
/// </summary>
public class AppointmentEditDto
{
    /// <summary>
    /// Дата и время приема
    /// </summary>
    [Required]
    public required DateTime DateTime { get; init; }

    /// <summary>
    /// Номер кабинета
    /// </summary>
    [Required]
    [MaxLength(10)]
    public required string RoomNumber { get; init; }

    /// <summary>
    /// Повторный прием (true) или первичный (false)
    /// </summary>
    [Required]
    public required bool IsRepeat { get; init; }

    /// <summary>
    /// Идентификатор питомца
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public required int PetId { get; init; }

    /// <summary>
    /// Идентификатор врача
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public required int VetId { get; init; }
}
