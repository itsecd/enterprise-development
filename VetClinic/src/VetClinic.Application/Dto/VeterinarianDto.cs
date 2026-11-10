namespace VetClinic.Application.DTOs;

/// <summary>
/// DTO ветеринара
/// </summary>
public class VeterinarianDto
{
    /// <summary>
    /// Идентификатор врача
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта врача
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО врача
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Год рождения
    /// </summary>
    public int BirthYear { get; set; }

    /// <summary>
    /// Стаж работы в годах
    /// </summary>
    public int ExperienceYears { get; set; }

    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    public int SpecializationId { get; set; }

    /// <summary>
    /// Название специализации
    /// </summary>
    public string? SpecializationName { get; set; }
}