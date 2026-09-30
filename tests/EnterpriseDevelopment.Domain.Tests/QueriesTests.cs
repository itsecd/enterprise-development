using EnterpriseDevelopment.Domain.Entities;
using Xunit;

namespace EnterpriseDevelopment.Domain.Tests;

/// <summary>
/// Unit-тесты LINQ-запросов.
/// </summary>
public class QueriesTests : IClassFixture<FoodDeliveryFixture>
{
    private readonly FoodDeliveryFixture _fixture;

    public QueriesTests(FoodDeliveryFixture fixture)
    {
        _fixture = fixture;
    }

    /// <summary>
    /// Проверяет наличие минимум 10 экземпляров каждого класса.
    /// </summary>
    [Fact]
    public void DataSet_ShouldContainAtLeastTenInstancesOfEachClass()
    {
        Assert.True(_fixture.Data.DishCategories.Count >= 10);
        Assert.True(_fixture.Data.Dishes.Count >= 10);
        Assert.True(_fixture.Data.Restaurants.Count >= 10);
        Assert.True(_fixture.Data.Clients.Count >= 10);
        Assert.True(_fixture.Data.Orders.Count >= 10);
        Assert.True(_fixture.Data.OrderItems.Count >= 10);
    }

    /// <summary>
    /// Проверяет выборку пяти ресторанов с наибольшим количеством заказов.
    /// </summary>
    [Fact]
    public void GetTopFiveRestaurantsByOrderCount()
    {
        var result = _fixture.Data.Orders
            .GroupBy(order => order.RestaurantId)
            .Select(group => new
            {
                RestaurantId = group.Key,
                OrderCount = group.Count()
            })
            .OrderByDescending(item => item.OrderCount)
            .ThenBy(item => item.RestaurantId)
            .Take(5)
            .ToList();

        Assert.Equal(5, result.Count);
        Assert.Equal(1, result[0].RestaurantId);
        Assert.Equal(6, result[0].OrderCount);
    }

    /// <summary>
    /// Проверяет поиск заказов с минимальным временем доставки.
    /// </summary>
    [Fact]
    public void GetOrdersWithMinimumDeliveryTime()
    {
        var minimumTime = _fixture.Data.Orders.Min(order => order.DeliveryTime);

        var result = _fixture.Data.Orders
            .Where(order => order.DeliveryTime == minimumTime)
            .ToList();

        Assert.NotEmpty(result);
        Assert.Equal(TimeSpan.FromMinutes(15), minimumTime);
        Assert.All(result, order => Assert.Equal(minimumTime, order.DeliveryTime));
    }

    /// <summary>
    /// Проверяет получение клиентов выбранного ресторана.
    /// </summary>
    [Fact]
    public void GetClientsByRestaurant()
    {
        const int restaurantId = 1;

        var result = _fixture.Data.Orders
            .Where(order => order.RestaurantId == restaurantId && order.Client is not null)
            .Select(order => order.Client!)
            .DistinctBy(client => client.Id)
            .OrderBy(client => client.FullName)
            .ToList();

        Assert.NotEmpty(result);
        Assert.Equal(result.OrderBy(client => client.FullName), result);
    }

    /// <summary>
    /// Проверяет статистику заказов по категориям блюд за указанный период.
    /// </summary>
    [Fact]
    public void GetOrderStatisticsByDishCategoryForPeriod()
    {
        var periodStart = new DateTimeOffset(2026, 2, 1, 0, 0, 0, TimeSpan.Zero);
        var periodEnd = new DateTimeOffset(2026, 5, 1, 0, 0, 0, TimeSpan.Zero);

        var result = _fixture.Data.OrderItems
            .Where(item => item.Order is not null
                && item.Order.CreatedAt >= periodStart
                && item.Order.CreatedAt < periodEnd
                && item.Dish is not null
                && item.Dish.Category is not null)
            .GroupBy(item => new
            {
                CategoryId = item.Dish!.CategoryId,
                CategoryName = item.Dish.Category!.Name
            })
            .Select(group =>
            {
                var orders = group
                    .Select(item => item.Order!)
                    .DistinctBy(order => order.Id)
                    .ToList();

                return new
                {
                    group.Key.CategoryId,
                    group.Key.CategoryName,
                    OrderCount = orders.Count,
                    AverageOrderAmount = orders.Average(order => order.TotalAmount),
                    TotalOrderAmount = orders.Sum(order => order.TotalAmount)
                };
            })
            .OrderBy(item => item.CategoryId)
            .ToList();

        Assert.Equal(10, result.Count);
        Assert.All(result, item =>
        {
            Assert.True(item.OrderCount > 0);
            Assert.True(item.AverageOrderAmount > 0);
            Assert.True(item.TotalOrderAmount > 0);
        });
    }

    /// <summary>
    /// Проверяет поиск клиента с максимальной суммой заказов.
    /// </summary>
    [Fact]
    public void GetClientWithMaximumSpentAmount()
    {
        var result = _fixture.Data.Orders
            .Where(order => order.Client is not null)
            .GroupBy(order => order.ClientId)
            .Select(group => new
            {
                ClientId = group.Key,
                TotalSpent = group.Sum(order => order.TotalAmount)
            })
            .OrderByDescending(item => item.TotalSpent)
            .First();

        Assert.Equal(1, result.ClientId);
        Assert.Equal(7300m, result.TotalSpent);
    }
}
