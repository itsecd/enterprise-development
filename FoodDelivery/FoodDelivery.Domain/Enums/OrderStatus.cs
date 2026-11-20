namespace FoodDelivery.Domain.Enums;

/// <summary>
/// Статус выполнения заказа доставки
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Заказ оформлен и ожидает подтверждения рестораном
    /// </summary>
    New,

    /// <summary>
    /// Ресторан готовит заказ
    /// </summary>
    Preparing,

    /// <summary>
    /// Курьер доставляет заказ клиенту
    /// </summary>
    Delivering,

    /// <summary>
    /// Заказ доставлен
    /// </summary>
    Completed,

    /// <summary>
    /// Заказ отменен
    /// </summary>
    Cancelled
}