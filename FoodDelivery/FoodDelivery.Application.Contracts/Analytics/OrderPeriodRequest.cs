using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Application.Contracts.Analytics;

/// <summary>
/// Период формирования статистики заказов
/// </summary>
public sealed class OrderPeriodRequest : IValidatableObject
{
    /// <summary>
    /// Включительная нижняя граница времени оформления заказа
    /// </summary>
    [Required(ErrorMessage = "Укажите начало периода")]
    public DateTime? Start { get; init; }

    /// <summary>
    /// Включительная верхняя граница времени оформления заказа
    /// </summary>
    [Required(ErrorMessage = "Укажите конец периода")]
    public DateTime? End { get; init; }

    /// <inheritdoc/>
    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (Start.HasValue && End.HasValue && Start.Value > End.Value)
        {
            yield return new ValidationResult(
                "Конец периода не может быть раньше начала",
                [nameof(End)]);
        }
    }
}
