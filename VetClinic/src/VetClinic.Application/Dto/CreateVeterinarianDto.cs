using System.ComponentModel.DataAnnotations;

namespace VetClinic.Application.Dto;

/// <summary>
/// Данные для создания или обновления ветеринарного врача
/// </summary>
public class CreateVeterinarianDto
{
    /// <summary>
    /// Номер паспорта врача
    /// </summary>
    [Required]
    [StringLength(20, MinimumLength = 6)]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО врача
    /// </summary>
    [Required]
    [StringLength(150, MinimumLength = 2)]
    public required string FullName { get; set; }

    /// <summary>
    /// Год рождения
    /// </summary>
    [Range(1920, 2015)]
    public int BirthYear { get; set; }

    /// <summary>
    /// Стаж работы в годах
    /// </summary>
    [Range(0, 80)]
    public int ExperienceYears { get; set; }

    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    [Range(1, int.MaxValue)]
    public int SpecializationId { get; set; }
}