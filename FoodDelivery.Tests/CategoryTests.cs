using FoodDelivery.Domain.Data;

namespace FoodDelivery.Tests;

/// <summary>
/// Тесты для проверки аналитики по категориям блюд
/// </summary>
public class CategoryTests
{
    /// <summary>
    /// Задание: Вывести сводную информацию о заказах (число заказов, средняя сумма заказа, общая сумма заказа) по каждой категории блюд за указанный период.
    /// </summary>
    [Theory]
    [InlineData(1, 2, 1725, 3450)]
    [InlineData(2, 2, 1045, 2090)]
    [InlineData(3, 2, 1045, 2090)]
    public void OrdersByCategoryForSelectedPeriod(
        int categoryId,
        int expectedOrderCount,
        decimal expectedAvgOrderAmount,
        decimal expectedTotalOrderAmount)
    {
        var periodStart = new DateTime(2026, 9, 1);
        var periodEnd = new DateTime(2026, 9, 5);

        var orders = TestData.Orders
            .Where(order =>
                order.CreatedAt >= periodStart &&
                order.CreatedAt < periodEnd)
            .SelectMany(order => order.Items.Select(item => new
            {
                Order = order,
                CategoryId = item.Dish.CategoryId
            }))
            .Where(item => item.CategoryId == categoryId)
            .DistinctBy(item => item.Order.Id)
            .ToList();

        var orderCount = orders.Count;

        var avgOrderAmount = orders
            .Average(item => item.Order.TotalAmount);

        var totalOrderAmount = orders
            .Sum(item => item.Order.TotalAmount);

        Assert.Equal(expectedOrderCount, orderCount);
        Assert.Equal(expectedAvgOrderAmount, avgOrderAmount);
        Assert.Equal(expectedTotalOrderAmount, totalOrderAmount);
    }
}