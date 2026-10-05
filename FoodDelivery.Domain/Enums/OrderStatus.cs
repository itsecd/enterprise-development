namespace FoodDelivery.Domain.Enums;

/// <summary>
/// Статус заказа
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Заказ создан
    /// </summary>
    Created,

    /// <summary>
    /// Заказ готовится
    /// </summary>
    Preparing,

    /// <summary>
    /// Заказ доставляется
    /// </summary>
    Delivering,

    /// <summary>
    /// Заказ доставлен
    /// </summary>
    Delivered,

    /// <summary>
    /// Заказ отменён
    /// </summary>
    Cancelled
}
