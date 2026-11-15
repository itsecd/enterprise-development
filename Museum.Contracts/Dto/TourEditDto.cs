using System.ComponentModel.DataAnnotations;

namespace Museum.Contracts.Dto;

/// <summary>
/// Хранит данные для создания и изменения экскурсии
/// </summary>
public sealed class TourEditDto : IValidatableObject
{
    /// <summary>
    /// Дата проведения экскурсии
    /// </summary>
    public DateTime Date { get; init; }

    /// <summary>
    /// Время начала экскурсии
    /// </summary>
    public TimeSpan StartTime { get; init; }

    /// <summary>
    /// Продолжительность экскурсии в минутах
    /// </summary>
    [Range(1, 1440)]
    public int DurationMinutes { get; init; }

    /// <summary>
    /// Идентификаторы выставок для экскурсии
    /// </summary>
    [Required]
    [MinLength(1)]
    public List<int> ExhibitionIds { get; init; } = [];

    /// <summary>
    /// Проверяет данные экскурсии
    /// </summary>
    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (Date == default)
        {
            yield return new ValidationResult(
                "Укажите дату проведения экскурсии",
                [nameof(Date)]);
        }

        if (ExhibitionIds.Any(id => id <= 0))
        {
            yield return new ValidationResult(
                "Идентификаторы выставок должны быть положительными",
                [nameof(ExhibitionIds)]);
        }
    }
}
