namespace VetClinic.Application.DTOs;

/// <summary>
/// Данные для создания приема
/// </summary>
public class CreateAppointmentDto
{
    /// <summary>
    /// Дата и время приема
    /// </summary>
    public DateTime DateTime { get; set; }

    /// <summary>
    /// Номер кабинета
    /// </summary>
    public int RoomNumber { get; set; }

    /// <summary>
    /// Флаг повторного приема
    /// </summary>
    public bool IsFollowUp { get; set; }

    /// <summary>
    /// Идентификатор питомца
    /// </summary>
    public int PetId { get; set; }

    /// <summary>
    /// Идентификатор врача
    /// </summary>
    public int VeterinarianId { get; set; }
}