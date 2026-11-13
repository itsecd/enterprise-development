using FoodDelivery.Domain.Data;

namespace FoodDelivery.Tests;

/// <summary>
/// Тесты для проверки аналитики по ресторанам
/// </summary>
public class RestaurantTests
{
    /// <summary>
    /// Задание: Вывести топ 5 ресторанов по количеству заказов. 
    /// </summary>
    [Fact]
    public void Top5RestaurantsByOrderCount()
    {
        var orders = TestData.Orders;

        var expected = new[] { 7, 2, 1, 3, 8 };

        var restaurantIds = orders
            .CountBy(order => order.RestaurantId)
            .Select(group => new
            {
                RestaurantId = group.Key,
                OrderCount = group.Value
            })
            .OrderByDescending(item => item.OrderCount)
            .ThenBy(item => item.RestaurantId)
            .Take(5)
            .Select(item => item.RestaurantId)
            .ToList();

        Assert.Equal(expected, restaurantIds);
    }
}