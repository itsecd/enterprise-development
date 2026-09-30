using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;
using Xunit;

namespace FoodDelivery.Tests;

/// <summary>
/// Тесты запросов к данным службы доставки
/// </summary>
/// <param name="fixture">Тестовые данные</param>
public class QueriesTest(QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
{
    private readonly List<Restaurant> _restaurants = fixture.Restaurants;
    private readonly List<Order> _orders = fixture.Orders;

    /// <summary>
    /// Проверка получения клиентом рестораном
    /// </summary>
    public void GetCustomersByRestaurant()
    {
        var targetRestaurantId = 0;
        var expectedCustomerName = new[] { "Анна Иванова", "Сергей Громов", "Михаил Тарасов" };

        var resultCustomers = _orders
            .Where(o => o.Restaurant?.Id == targetRestaurantId)
            .Select(o => o.Customer)
            .Distinct()
            .OrderBy(c => c?.Name)
            .ToList();

        Assert.NotEmpty(resultCustomers);
        Assert.Equal(expectedCustomerName[0], resultCustomers[0]?.Name);
        Assert.Equal(expectedCustomerName[1], resultCustomers[1]?.Name);
        Assert.Equal(expectedCustomerName[2], resultCustomers[2]?.Name);
    }

    /// <summary>
    /// Проверка получения идентификатора заказа
    /// </summary>
    [Fact]
    public void GetOrdersByStatus()
    {
        var orderStatus = orderStatus.Delivering;
        var expectedOrderIds = new HashSet<int> { 1, 3, 6, 8 };

        var resultOrdersIds = _orders
            .Where(o => o.Status == orderStatus)
            .Select(o => o.Id)
            .ToHashSet();

        Assert.Equal(expectedOrderIds, resultOrdersIds);
    }

    /// <summary>
    /// Проверка подсчёта количества заказов для каждого ресторана
    /// </summary>
    [Fact]
    public void GetOrderCountByRestaurant()
    {
        var expectedRestaurantCount = 10;
        var restaurant1Id = 1;
        var expectedRestaurant1OrderCount = 3;
        var restaurant8Id = 8;
        var expectedRestaurant8OrderCount = 0;

        var restaurantOrderCounts = _restaurants
            .Select(r => new
            {
                Restaurant = r,
                OrderCount = _orders.Count(o => o.RestaurantId == r.Id)
            })
            .ToList();

        Assert.Equal(expectedRestaurantCount, restaurantOrderCounts.Count);

        var restaurant1Stats = restaurantOrderCounts.First(r => r.Restaurant.Id == restaurant1Id);
        Assert.Equal(expectedRestaurant1OrderCount, restaurant1Stats.OrderCount);

        var restaurant8Stats = restaurantOrderCounts.First(r => r.Restaurant.Id == restaurant8Id);
        Assert.Equal(expectedRestaurant8OrderCount, restaurant8Stats.OrderCount);
    }

    /// <summary>
    /// Проверка получения топ-5 курьеров по количеству заказов
    /// </summary>
    [Fact]
    public void GetTopFiveCouriersByOrderCount()
    {
        var expectedTopCourierTripCount = 3;

        var topCouriers = _orders
            .GroupBy(o => o.Courier)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new { Courier = g.Key, OrderCount = g.Count() })
            .ToList();

        Assert.NotEmpty(topCouriers);
        Assert.Equal(expectedTopCourierTripCount, topCouriers.First().OrderCount);
    }

    /// <summary>
    /// Проверка получения топ-5 клиентов по суммарной стоимости заказов
    /// </summary>
    [Fact]
    public void GetTopFiveCustomersByTotalSpent()
    {
        var expectedTopCustomerId = 4;
        var expectedTopCustomerTotalSpent = 4200;

        var topCustomers = _orders
            .GroupBy(o => o.Customer)
            .OrderByDescending(g => g.Sum(o => o.TotalAmount))
            .Take(5)
            .Select(g => new { Customer = g.Key, TotalSpent = g.Sum(o => o.TotalAmount) })
            .ToList();

        Assert.NotEmpty(topCustomers);
        Assert.Equal(expectedTopCustomerId, topCustomers.First().Customer?.Id);
        Assert.Equal(expectedTopCustomerTotalSpent, topCustomers.First().TotalSpent);
    }
}