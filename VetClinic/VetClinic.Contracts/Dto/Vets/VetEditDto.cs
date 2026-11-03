using System.ComponentModel.DataAnnotations;

namespace VetClinic.Contracts.Dto.Vets;

/// <summary>
/// Данные врача для создания и изменения
/// </summary>
public class VetEditDto
{
    /// <summary>
    /// Номер паспорта в формате 0000 000000
    /// </summary>
    [Required]
    [RegularExpression(@"^\d{4} \d{6}$")]
    public required string PassportNumber { get; init; }

    /// <summary>
    /// ФИО врача
    /// </summary>
    [Required]
    public required string FullName { get; init; }

    /// <summary>
    /// Год рождения
    /// </summary>
    [Required]
    [Range(1900, 2200)]
    public required int BirthYear { get; init; }

    /// <summary>
    /// Идентификатор специализации, null если специализация не указана
    /// </summary>
    [Range(1, int.MaxValue)]
    public int? SpecializationId { get; init; }

    /// <summary>
    /// Стаж работы в годах
    /// </summary>
    [Required]
    [Range(0, 80)]
    public required int WorkExperienceYears { get; init; }
}
