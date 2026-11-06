using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Application.Contracts.Orders;

/// <summary>
/// Данные для создания или изменения метаданных заказа
/// </summary>
public sealed class OrderRequest : IValidatableObject
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Укажите положительный идентификатор клиента")]
    public required int CustomerId { get; init; }

    /// <summary>
    /// Идентификатор ресторана
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Укажите положительный идентификатор ресторана")]
    public required int RestaurantId { get; init; }

    /// <summary>
    /// Время оформления заказа
    /// </summary>
    public required DateTime CreatedAt { get; init; }

    /// <summary>
    /// Время доставки заказа
    /// </summary>
    public DateTime? DeliveredAt { get; init; }

    /// <inheritdoc/>
    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (CreatedAt == default)
        {
            yield return new ValidationResult(
                "Укажите время оформления заказа",
                [nameof(CreatedAt)]);
        }

        if (DeliveredAt.HasValue && DeliveredAt.Value < CreatedAt)
        {
            yield return new ValidationResult(
                "Время доставки не может быть раньше времени оформления",
                [nameof(DeliveredAt)]);
        }
    }
}
