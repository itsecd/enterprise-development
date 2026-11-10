using System.ComponentModel.DataAnnotations;

namespace VetClinic.Application.Dto;

/// <summary>
/// Данные для создания приема
/// </summary>
public class CreateAppointmentDto
{
    /// <summary>
    /// Дата и время приема
    /// </summary>
    [Required]
    public DateTime? DateTime { get; set; }

    /// <summary>
    /// Номер кабинета
    /// </summary>
    [Range(1, 1000)]
    public int RoomNumber { get; set; }

    /// <summary>
    /// Флаг повторного приема
    /// </summary>
    public bool IsFollowUp { get; set; }

    /// <summary>
    /// Идентификатор питомца
    /// </summary>
    [Range(1, int.MaxValue)]
    public int PetId { get; set; }

    /// <summary>
    /// Идентификатор врача
    /// </summary>
    [Range(1, int.MaxValue)]
    public int VeterinarianId { get; set; }
}