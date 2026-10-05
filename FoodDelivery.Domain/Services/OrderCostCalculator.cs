using FoodDelivery.Domain.Model;

namespace FoodDelivery.Domain.Services;

/// <summary>
/// Сервис для расчета стоимости заказа
/// </summary>
public static class OrderCostCalculator
{
    /// <summary>
    /// Расчет стоимости блюд в заказе
    /// </summary>
    public static decimal CalculateSubtotal(IEnumerable<OrderItem> items)
    {
        return items.Sum(item => item.Quantity * item.UnitPrice);
    }

    /// <summary>
    /// Расчет стоимости заказа c доставкой
    /// </summary>
    public static decimal CalculateTotal(IEnumerable<OrderItem> items, decimal deliveryFee)
    {
        return CalculateSubtotal(items) + deliveryFee;
    }

    /// <summary>
    /// Расчет итоговой стоимости заказа по объекту заказа
    /// </summary>
    public static decimal CalculateTotal(Order order)
    {
        return CalculateTotal(order.Items, order.DeliveryFee);
    }
}