using FoodDelivery.Domain.Entities;
using Xunit;

namespace FoodDelivery.Domain.Tests;

/// <summary>
/// Unit-тесты LINQ-запросов.
/// </summary>
public class QueriesTests(FoodDeliveryFixture fixture) : IClassFixture<FoodDeliveryFixture>
{
    private readonly DataSeeder _data = fixture.Data;

    /// <summary>
    /// Проверяет наличие минимум 10 экземпляров каждого класса.
    /// </summary>
    [Fact]
    public void DataSet_ShouldContainAtLeastTenInstancesOfEachEntity()
    {
        const int expectedMinimumCount = 10;

        Assert.True(_data.DishCategories.Count >= expectedMinimumCount);
        Assert.True(_data.Dishes.Count >= expectedMinimumCount);
        Assert.True(_data.Restaurants.Count >= expectedMinimumCount);
        Assert.True(_data.Clients.Count >= expectedMinimumCount);
        Assert.True(_data.Orders.Count >= expectedMinimumCount);
        Assert.True(_data.OrderItems.Count >= expectedMinimumCount);
    }

    /// <summary>
    /// Проверяет выборку пяти ресторанов с наибольшим количеством заказов.
    /// </summary>
    [Fact]
    public void GetTopFiveRestaurantsByOrderCount_ReturnsExpectedRestaurants()
    {
        var expected = new[]
        {
            (RestaurantId: 1, OrderCount: 6),
            (RestaurantId: 2, OrderCount: 4),
            (RestaurantId: 3, OrderCount: 3),
            (RestaurantId: 4, OrderCount: 1),
            (RestaurantId: 5, OrderCount: 1)
        };

        const int expectedRestaurantCount = 5;

        var result = _data.Orders
            .GroupBy(order => order.RestaurantId)
            .Select(group => new
            {
                RestaurantId = group.Key,
                OrderCount = group.Count()
            })
            .OrderByDescending(item => item.OrderCount)
            .ThenBy(item => item.RestaurantId)
            .Take(expectedRestaurantCount)
            .Select(item => (item.RestaurantId, item.OrderCount))
            .ToArray();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет поиск всех заказов с минимальным временем доставки.
    /// </summary>
    [Fact]
    public void GetOrdersWithMinimumDeliveryTime_ReturnsAllShortestDeliveries()
    {
        var expectedDeliveryTime = TimeSpan.FromMinutes(15);
        var expectedOrderIds = new[] { 4, 16 };

        var minimumDeliveryTime = _data.Orders.Min(order => order.DeliveryTime);

        var result = _data.Orders
            .Where(order => order.DeliveryTime == minimumDeliveryTime)
            .Select(order => order.Id)
            .OrderBy(orderId => orderId)
            .ToArray();

        Assert.Equal(expectedDeliveryTime, minimumDeliveryTime);
        Assert.Equal(expectedOrderIds, result);
    }

    /// <summary>
    /// Проверяет получение уникальных клиентов выбранного ресторана в порядке ФИО.
    /// </summary>
    [Fact]
    public void GetClientsByRestaurant_ReturnsDistinctClientsOrderedByFullName()
    {
        var expected = new[]
        {
            (ClientId: 7, FullName: "Васильева Елена Викторовна"),
            (ClientId: 1, FullName: "Иванов Иван Иванович"),
            (ClientId: 6, FullName: "Попов Дмитрий Олегович")
        };

        const int restaurantId = 1;

        var result = _data.Orders
            .Where(order => order.RestaurantId == restaurantId && order.Client is not null)
            .Select(order => order.Client!)
            .DistinctBy(client => client.Id)
            .OrderBy(client => client.FullName)
            .Select(client => (client.Id, client.FullName))
            .ToArray();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет статистику заказов по категориям блюд за указанный период.
    /// </summary>
    [Fact]
    public void GetOrderStatisticsByDishCategoryForPeriod_ReturnsExpectedStatistics()
    {
        var expected = new[]
        {
            (CategoryId: 1, CategoryName: "Пицца", OrderCount: 4, AverageOrderAmount: 1825m, TotalOrderAmount: 7300m),
            (CategoryId: 2, CategoryName: "Суши", OrderCount: 3, AverageOrderAmount: 2980m / 3m, TotalOrderAmount: 2980m),
            (CategoryId: 3, CategoryName: "Бургеры", OrderCount: 4, AverageOrderAmount: 925m, TotalOrderAmount: 3700m),
            (CategoryId: 4, CategoryName: "Салаты", OrderCount: 2, AverageOrderAmount: 800m, TotalOrderAmount: 1600m),
            (CategoryId: 5, CategoryName: "Супы", OrderCount: 2, AverageOrderAmount: 900m, TotalOrderAmount: 1800m),
            (CategoryId: 6, CategoryName: "Паста", OrderCount: 2, AverageOrderAmount: 700m, TotalOrderAmount: 1400m),
            (CategoryId: 7, CategoryName: "Десерты", OrderCount: 1, AverageOrderAmount: 750m, TotalOrderAmount: 750m),
            (CategoryId: 8, CategoryName: "Напитки", OrderCount: 1, AverageOrderAmount: 500m, TotalOrderAmount: 500m),
            (CategoryId: 9, CategoryName: "Завтраки", OrderCount: 1, AverageOrderAmount: 450m, TotalOrderAmount: 450m),
            (CategoryId: 10, CategoryName: "Выпечка", OrderCount: 1, AverageOrderAmount: 550m, TotalOrderAmount: 550m)
        };

        var periodStart = new DateTimeOffset(2026, 2, 1, 0, 0, 0, TimeSpan.Zero);
        var periodEnd = new DateTimeOffset(2026, 5, 1, 0, 0, 0, TimeSpan.Zero);

        var result = _data.OrderItems
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
                    .ToArray();

                return new
                {
                    group.Key.CategoryId,
                    group.Key.CategoryName,
                    OrderCount = orders.Length,
                    AverageOrderAmount = orders.Average(order => order.TotalAmount),
                    TotalOrderAmount = orders.Sum(order => order.TotalAmount)
                };
            })
            .OrderBy(item => item.CategoryId)
            .Select(item => (
                item.CategoryId,
                item.CategoryName,
                item.OrderCount,
                item.AverageOrderAmount,
                item.TotalOrderAmount))
            .ToArray();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет получение всех клиентов с максимальной суммой заказов, включая равенство сумм.
    /// </summary>
    [Fact]
    public void GetClientsWithMaximumSpentAmount_ReturnsAllClientsWhenMaximumIsTied()
    {
        const decimal expectedMaximumSpent = 7300m;
        const decimal additionalOrderAmount = 5900m;
        const int additionalOrderId = 21;
        const int tiedClientId = 2;
        var expectedClientIds = new[] { 1, tiedClientId };

        var clientWithTie = _data.Clients.Single(client => client.Id == tiedClientId);
        var orders = _data.Orders
            .Append(new Order
            {
                Id = additionalOrderId,
                ClientId = clientWithTie.Id,
                Client = clientWithTie,
                TotalAmount = additionalOrderAmount
            })
            .ToArray();

        var spendingByClient = orders
            .Where(order => order.Client is not null)
            .GroupBy(order => order.ClientId)
            .Select(group => new
            {
                ClientId = group.Key,
                TotalSpent = group.Sum(order => order.TotalAmount)
            })
            .ToArray();

        var maximumSpent = spendingByClient.Max(item => item.TotalSpent);

        var result = spendingByClient
            .Where(item => item.TotalSpent == maximumSpent)
            .OrderBy(item => item.ClientId)
            .ToArray();

        Assert.Equal(expectedMaximumSpent, maximumSpent);
        Assert.Equal(expectedClientIds, result.Select(item => item.ClientId).ToArray());
        Assert.All(result, item => Assert.Equal(expectedMaximumSpent, item.TotalSpent));
    }
}
