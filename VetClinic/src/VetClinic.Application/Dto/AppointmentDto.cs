namespace VetClinic.Application.DTOs;

/// <summary>
/// DTO приема
/// </summary>
public class AppointmentDto
{
    /// <summary>
    /// Идентификатор записи на прием
    /// </summary>
    public int Id { get; set; }

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
    /// Кличка питомца
    /// </summary>
    public string? PetName { get; set; }

    /// <summary>
    /// Идентификатор врача
    /// </summary>
    public int VeterinarianId { get; set; }

    /// <summary>
    /// ФИО врача
    /// </summary>
    public string? VeterinarianName { get; set; }
}