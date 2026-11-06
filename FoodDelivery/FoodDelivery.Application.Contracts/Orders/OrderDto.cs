namespace FoodDelivery.Application.Contracts.Orders;

/// <summary>
/// Сведения о заказе
/// </summary>
public sealed class OrderDto
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int CustomerId { get; init; }

    /// <summary>
    /// Идентификатор ресторана
    /// </summary>
    public required int RestaurantId { get; init; }

    /// <summary>
    /// Время оформления заказа
    /// </summary>
    public required DateTime CreatedAt { get; init; }

    /// <summary>
    /// Время доставки заказа
    /// </summary>
    public DateTime? DeliveredAt { get; init; }

    /// <summary>
    /// Итоговая сумма заказа
    /// </summary>
    public required decimal TotalAmount { get; init; }
}
