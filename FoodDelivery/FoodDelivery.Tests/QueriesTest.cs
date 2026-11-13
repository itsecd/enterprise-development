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
    private readonly IReadOnlyList<Restaurant> _restaurants = fixture.Restaurants;
    private readonly IReadOnlyList<Order> _orders = fixture.Orders;

    /// <summary>
    /// Проверка получения клиентом рестораном
    /// </summary>
    [Fact]
    public void GetCustomersByRestaurant()
    {
        var targetRestaurantId = 0;
        var expectedCustomerName = new[] 
        { 
            "Анна Иванова", 
            "Иван Лапшин", 
            "Максим Громов" 
        };

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
        const OrderStatus targetStatus = OrderStatus.Delivering;
        var expectedOrderIds = new HashSet<int> 
        { 
            1, 3, 6, 8 
        };

        var resultOrdersIds = _orders
            .Where(o => o.Status == targetStatus)
            .Select(o => o.Id)
            .ToHashSet();

        Assert.True(
            expectedOrderIds.SetEquals(resultOrdersIds),
            $"Ожидались заказы [{string.Join(", ", expectedOrderIds)}], " +
            $"получены [{string.Join(", ", resultOrdersIds)}].");
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
            .Where(order => order.CourierId.HasValue)
            .GroupBy(order => order.CourierId.GetValueOrDefault())
            .Select(group => new 
            { 
                Courier = group.Key, OrderCount = group.Count() 
            })
            .OrderByDescending(item => item.OrderCount)
            .ThenBy(item => item.Courier)
            .Take(5)
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
            .GroupBy(order => order.Customer)
            .Select(group => new
            {
                CustomerId = group.Key,
                TotalSpent = group.Sum(order => order.TotalAmount)
            })
            .OrderByDescending(item => item.TotalSpent)
            .Take(5)
            .ToList();

        Assert.NotEmpty(topCustomers);
        Assert.Equal(expectedTopCustomerId, topCustomers.First().CustomerId?.Id);
        Assert.Equal(expectedTopCustomerTotalSpent, topCustomers.First().TotalSpent);
    }
}