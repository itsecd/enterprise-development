using System.ComponentModel.DataAnnotations;
using Museum.Domain;

namespace Museum.Contracts.Dto;

/// <summary>
/// Хранит данные для создания и изменения выставки
/// </summary>
public sealed class ExhibitionEditDto : IValidatableObject
{
    /// <summary>
    /// Название выставки
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Тематика выставки
    /// </summary>
    [EnumDataType(typeof(ExhibitionTheme))]
    public ExhibitionTheme Theme { get; init; }

    /// <summary>
    /// Номер зала
    /// </summary>
    [Range(1, int.MaxValue)]
    public int HallNumber { get; init; }

    /// <summary>
    /// Дата начала выставки
    /// </summary>
    public DateTime StartDate { get; init; }

    /// <summary>
    /// Дата окончания выставки
    /// </summary>
    public DateTime EndDate { get; init; }

    /// <summary>
    /// Проверяет даты выставки
    /// </summary>
    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (StartDate == default)
        {
            yield return new ValidationResult(
                "Укажите дату начала выставки",
                [nameof(StartDate)]);
        }

        if (EndDate <= StartDate)
        {
            yield return new ValidationResult(
                "Дата окончания должна быть позже даты начала",
                [nameof(EndDate)]);
        }
    }
}
