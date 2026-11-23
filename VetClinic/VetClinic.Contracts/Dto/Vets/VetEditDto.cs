using System.ComponentModel.DataAnnotations;

namespace VetClinic.Contracts.Dto.Vets;

/// <summary>
/// Данные врача для создания и изменения
/// </summary>
public class VetEditDto : IValidatableObject
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
    /// Год рождения, не раньше 1900 и не позже текущего года
    /// </summary>
    [Required]
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

    /// <inheritdoc/>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var currentYear = DateTime.Now.Year;
        if (BirthYear < 1900 || BirthYear > currentYear)
        {
            yield return new ValidationResult(
                $"Год рождения должен быть от 1900 до {currentYear}",
                [nameof(BirthYear)]);
        }
    }
}
