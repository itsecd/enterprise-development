namespace FoodDelivery.Domain.Enums;

/// <summary>
/// Способ оплаты заказа
/// </summary>
public enum PaymentMethod
{
    /// <summary>
    /// Оплата наличным курьеру
    /// </summary>
    Cash,

    /// <summary>
    /// Оплата картой курьеру
    /// </summary>
    CardOnDelivery,

    /// <summary>
    /// Онлайн-оплата в приложении
    /// </summary>
    Online
}