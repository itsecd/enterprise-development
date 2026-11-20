using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Tests;

/// <summary>
/// Тесты запросов к данным службы доставки
/// </summary>
/// <param name="fixture">Тестовые данные</param>
public class QueriesTest(QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
{
    private readonly List<Customer> _customers = fixture.Customers;
    private readonly List<Restaurant> _restaurants = fixture.Restaurants;
    private readonly List<Dish> _dishes = fixture.Dishes;
    private readonly List<Courier> _couriers = fixture.Couriers;
    private readonly List<Order> _orders = fixture.Orders;

    /// <summary>
    /// Вывод топ 5 ресторанов по количеству заказов
    /// </summary>
    [Fact]
    public void GetTopFiveRestaurantsByOrderCount()
    {
        var expectedTopIds = new List<int> { 1, 2, 3, 4, 6 };
        var expectedOrderCounts = new Dictionary<int, int>
        {
            [1] = 3,
            [2] = 3,
            [3] = 1,
            [4] = 1,
            [6] = 1
        };

        var topRestaurants = _restaurants
            .Select(r => new
            {
                RestaurantId = r.Id,
                RestaurantName = r.Name,
                OrderCount = _orders.Count(o => o.RestaurantId == r.Id)
            })
            .OrderByDescending(x => x.OrderCount)
            .ThenBy(x => x.RestaurantId)
            .Take(5)
            .ToList();

        Assert.Equal(expectedTopIds, topRestaurants.Select(x => x.RestaurantId).ToList());
        foreach (var item in topRestaurants)
        {
            Assert.Equal(expectedOrderCounts[item.RestaurantId], item.OrderCount);
        }
    }

    /// <summary>
    /// Вывод заказов с минимальным временем доставки
    /// </summary>
    [Fact]
    public void GetOrdersWithMinDeliveryTime()
    {
        var expectedOrderIds = new List<int> { 9 };
        var expectedMinDuration = TimeSpan.FromMinutes(25);

        var minDuration = _orders.Min(o => o.DeliveryTime - o.OrderTime);

        var fastOrders = _orders
            .Where(o => (o.DeliveryTime - o.OrderTime) == minDuration)
            .OrderBy(o => o.Id)
            .ToList();

        Assert.Equal(expectedMinDuration, minDuration);
        Assert.Equal(expectedOrderIds, fastOrders.Select(o => o.Id).ToList());
    }

    /// <summary>
    /// Вывод клиентов, сделавших заказ в выбранном ресторане
    /// </summary>
    [Fact]
    public void GetCustomersByRestaurantOrderedByName()
    {
        var targetRestaurantId = 1;
        var expectedCustomerNames = new List<string>
        {
            "Анна Иванова",
            "Иван Лапшин",
            "Максим Громов"
        };

        var customers = _orders
            .Where(o => o.RestaurantId == targetRestaurantId && o.Customer != null)
            .Select(o => o.Customer!)
            .DistinctBy(c => c.Id)
            .OrderBy(c => c.Name)
            .ToList();

        var actualNames = customers.Select(c => c.Name).ToList();
        Assert.Equal(expectedCustomerNames, actualNames);
    }

    /// <summary>
    /// 4. Вывод информации о заказах за определенный период
    /// </summary>
    [Fact]
    public void GetOrderSummaryByCategoryForPeriod()
    {
        var startDate = new DateTime(2026, 5, 1);
        var endDate = new DateTime(2026, 5, 31, 23, 59, 59);

        var expectedSummary = new List<(string CategoryName, int OrderCount, decimal AverageAmount, decimal TotalAmount)>
    {
        ("Основные блюда", 5, 2260m, 11300m),
        ("Пицца", 1, 1200m, 1200m),
        ("Фастфуд", 4, 1325m, 5300m)
    };

        var summary = _orders
            .Where(o => o.OrderTime >= startDate && o.OrderTime <= endDate)
            .SelectMany(order => fixture.Dishes
                .Where(d => d.RestaurantId == order.RestaurantId && d.Category != null)
                .Select(d => d.Category!.Name)
                .Distinct()
                .Select(categoryName => new { Order = order, CategoryName = categoryName }))
            .GroupBy(x => x.CategoryName)
            .Select(g => new
            {
                CategoryName = g.Key,
                OrderCount = g.Count(),
                AverageAmount = g.Average(x => x.Order.TotalAmount),
                TotalAmount = g.Sum(x => x.Order.TotalAmount)
            })
            .OrderBy(x => x.CategoryName)
            .Select(x => (x.CategoryName, x.OrderCount, x.AverageAmount, x.TotalAmount))
            .ToList();

        Assert.Equal(expectedSummary, summary);
    }

    /// <summary>
    /// 5. Вывод информации о клиенте, который потратил наибольшую сумму
    /// </summary>
    [Fact]
    public void GetCustomerWithMaxTotalSpent()
    {
        var expectedCustomerId = 5;
        var expectedTotalSpent = 4200m;

        var topCustomerStat = _orders
            .Where(o => o.Customer != null)
            .GroupBy(o => o.Customer!)
            .Select(g => new
            {
                Customer = g.Key,
                TotalSpent = g.Sum(o => o.TotalAmount)
            })
            .OrderByDescending(x => x.TotalSpent)
            .FirstOrDefault();

        Assert.NotNull(topCustomerStat);
        Assert.Equal(expectedCustomerId, topCustomerStat.Customer.Id);
        Assert.Equal(expectedTotalSpent, topCustomerStat.TotalSpent);
    }
}