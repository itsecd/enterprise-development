using System.ComponentModel.DataAnnotations;
using BeautySalon.Shared.Enums;

namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Мастер салона (для создания и изменения)
/// </summary>
public record MasterEditDto
{
    /// <summary>
    /// Полное имя
    /// </summary>
    [Required, MaxLength(200)]
    public required string FullName { get; init; }

    /// <summary>
    /// Пол
    /// </summary>
    [Required, EnumDataType(typeof(Gender))]
    public Gender? Gender { get; init; }

    /// <summary>
    /// Номер паспорта в формате «1234 567890»
    /// </summary>
    [Required, RegularExpression(@"^\d{4} \d{6}$", ErrorMessage = "Номер паспорта должен иметь формат «1234 567890»")]
    public required string PassportNumber { get; init; }

    /// <summary>
    /// Специализация
    /// </summary>
    [Required, EnumDataType(typeof(Specialization))]
    public Specialization? Specialization { get; init; }

    /// <summary>
    /// Стаж работы (лет)
    /// </summary>
    [Required, Range(0, 80)]
    public int? WorkExperience { get; init; }
}