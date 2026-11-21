using FoodDelivery.Domain.Data;

namespace FoodDelivery.Tests;

/// <summary>
/// Тесты для проверки аналитики по заказам
/// </summary>
public class OrderTests
{
    /// <summary>
    /// Задание: Вывести список заказов с минимальным временем доставки.
    /// </summary>
    [Fact]
    public void OrdersWithMinDeliveryTime()
    {
        var orders = TestData.Orders;

        var expected = new[] { 8, 9 };

        var minimumDeliveryTime = orders
            .Select(order => order.DeliveredAt - order.CreatedAt)
            .Min();

        var orderIds = orders
            .Where(order => order.DeliveredAt - order.CreatedAt == minimumDeliveryTime)
            .Select(order => order.Id)
            .OrderBy(orderId => orderId)
            .ToList();

        Assert.Equal(expected, orderIds);
    }
}