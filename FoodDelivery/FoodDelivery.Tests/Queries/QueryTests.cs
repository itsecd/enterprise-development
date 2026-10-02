using FoodDelivery.Domain.Data;

namespace FoodDelivery.Tests.Queries;

/// <summary>
/// Тесты аналитических запросов по заказам
/// </summary>
public class QueryTests(FoodDeliveryData data)
    : IClassFixture<FoodDeliveryData>
{
    private const int AmountPrecision = 2;

    /// <summary>
    /// Проверяет количество, среднюю и общую сумму заказов выбранной категории за период
    /// </summary>
    [Theory]
    [InlineData(1, 3, 1000, 3000)]
    [InlineData(5, 5, 698, 3490)]
    [InlineData(8, 5, 216, 1080)]
    public void GetCategoryOrderInfo(
        int categoryId,
        int expectedOrderCount,
        decimal expectedAverageAmount,
        decimal expectedTotalAmount)
    {
        var startDate = new DateTime(2026, 9, 1);
        var endDate = new DateTime(2026, 9, 20);

        var category = data.Categories
            .Single(item => item.Id == categoryId);

        var orders = data.Orders
            .Where(order =>
                order.CreatedAt >= startDate &&
                order.CreatedAt <= endDate &&
                order.Items.Any(item =>
                    item.Dish.Category.Id == category.Id))
            .ToList();

        var orderCount = orders.Count;
        var totalAmount = orders.Sum(order => order.TotalAmount);
        var averageAmount = Math.Round(
            orders.Average(order => order.TotalAmount),
            AmountPrecision);

        Assert.Equal(expectedOrderCount, orderCount);
        Assert.Equal(expectedTotalAmount, totalAmount);
        Assert.Equal(expectedAverageAmount, averageAmount);
    }

    /// <summary>
    /// Проверяет клиентов выбранного ресторана, упорядоченных по ФИО
    /// </summary>
    [Theory]
    [InlineData(1, new[] { 1, 4, 2, 6, 3, 5 })]
    [InlineData(4, new[] { 4, 8, 2, 3, 5 })]
    [InlineData(7, new[] { 15, 1 })]
    public void GetRestaurantCustomers(
        int restaurantId,
        int[] expectedCustomerIds)
    {
        var actualCustomerIds = data.Orders
            .Where(order => order.Restaurant.Id == restaurantId)
            .Select(order => order.Customer)
            .DistinctBy(customer => customer.Id)
            .OrderBy(customer => customer.LastName)
            .ThenBy(customer => customer.FirstName)
            .ThenBy(customer => customer.Patronymic)
            .Select(customer => customer.Id)
            .ToArray();

        Assert.Equal(expectedCustomerIds, actualCustomerIds);
    }

    /// <summary>
    /// Проверяет заказы с минимальным временем доставки
    /// </summary>
    [Fact]
    public void GetOrdersWithMinimumDeliveryTime()
    {
        var expectedMinimumTime = TimeSpan.FromMinutes(20);
        var expectedOrderIds = new[] { 4, 12, 19, 24, 32, 38 };

        var minimumTime = data.Orders
            .Min(order => order.DeliveredAt - order.CreatedAt);

        var actualOrderIds = data.Orders
            .Where(order => order.DeliveredAt - order.CreatedAt == minimumTime)
            .Select(order => order.Id)
            .ToArray();

        Assert.Equal(expectedMinimumTime, minimumTime);
        Assert.Equal(expectedOrderIds, actualOrderIds);
    }

    /// <summary>
    /// Проверяет пять ресторанов с наибольшим количеством заказов
    /// </summary>
    [Fact]
    public void GetTopFiveRestaurants()
    {
        var expected = new[]
        {
            (RestaurantId: 1, OrderCount: 6),
            (RestaurantId: 5, OrderCount: 6),
            (RestaurantId: 2, OrderCount: 5),
            (RestaurantId: 3, OrderCount: 5),
            (RestaurantId: 4, OrderCount: 5)
        };

        var restaurants = data.Orders
            .GroupBy(order => order.Restaurant)
            .Select(group => (
                RestaurantId: group.Key.Id,
                OrderCount: group.Count()))
            .OrderByDescending(item => item.OrderCount)
            .ThenBy(item => item.RestaurantId)
            .Take(5)
            .ToArray();

        Assert.Equal(expected, restaurants);
    }

    /// <summary>
    /// Проверяет клиента с наибольшей суммой заказов за всё время
    /// </summary>
    [Fact]
    public void GetCustomerWithMaximumSpending()
    {
        var customer = data.Orders
            .GroupBy(order => order.Customer)
            .Select(group => new
            {
                Customer = group.Key,
                TotalAmount = group.Sum(order => order.TotalAmount)
            })
            .OrderByDescending(item => item.TotalAmount)
            .ThenBy(item => item.Customer.Id)
            .First();

        Assert.Equal("Смирнова", customer.Customer.LastName);
        Assert.Equal("Анна", customer.Customer.FirstName);
        Assert.Equal("Викторовна", customer.Customer.Patronymic);
        Assert.Equal(2820m, customer.TotalAmount);
    }
}