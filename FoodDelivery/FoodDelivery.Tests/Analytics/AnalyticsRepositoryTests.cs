using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Analytics;

/// <summary>
/// Проверяет аналитические запросы к текущим данным приложения
/// </summary>
public sealed class AnalyticsRepositoryTests
{
    /// <summary>
    /// Проверяет топ ресторанов и его изменение после добавления заказа
    /// </summary>
    [Fact]
    public void TopRestaurants_ReflectsAddedOrder()
    {
        var data = new FoodDeliveryData();
        var analytics = new AnalyticsRepository(data);
        var orderRepository = new OrderRepository(data);

        var initial = analytics.GetTopRestaurants();

        Assert.Equal(
            new[] { 1, 5, 2, 3, 4 },
            initial.Select(item => item.Restaurant.Id).ToArray());

        Assert.Equal(
            new[] { 6, 6, 5, 5, 5 },
            initial.Select(item => item.OrderCount).ToArray());

        var restaurant = data.Restaurants.Single(item => item.Id == 5);

        orderRepository.Add(new Order
        {
            Id = 0,
            Customer = data.Customers.First(),
            Restaurant = restaurant,
            CreatedAt = new DateTime(2026, 9, 21, 12, 0, 0),
            DeliveredAt = null,
            Items = []
        });

        var updated = analytics.GetTopRestaurants();

        Assert.Equal(5, updated[0].Restaurant.Id);
        Assert.Equal(7, updated[0].OrderCount);
    }

    /// <summary>
    /// Проверяет все заказы с одинаковым минимальным временем доставки
    /// </summary>
    [Fact]
    public void MinimumDelivery_ReturnsAllMatchingOrders()
    {
        var data = new FoodDeliveryData();
        var repository = new AnalyticsRepository(data);

        var orders = repository.GetOrdersWithMinimumDeliveryTime();

        Assert.Equal(
            new[] { 4, 12, 19, 24, 32, 38 },
            orders.Select(order => order.Id).ToArray());
    }

    /// <summary>
    /// Проверяет сортировку клиентов ресторана без повторений
    /// </summary>
    [Fact]
    public void RestaurantCustomers_ReturnsSortedDistinctCustomers()
    {
        var data = new FoodDeliveryData();
        var repository = new AnalyticsRepository(data);

        var customers = repository.GetRestaurantCustomers(1);

        Assert.Equal(
            new[] { 1, 4, 2, 6, 3, 5 },
            customers.Select(customer => customer.Id).ToArray());
    }

    /// <summary>
    /// Проверяет статистику категории до полуночи 20 сентября включительно
    /// </summary>
    [Fact]
    public void CategoryStatistics_ReturnsExpectedAmounts()
    {
        var data = new FoodDeliveryData();
        var repository = new AnalyticsRepository(data);

        var statistics = repository.GetCategoryStatistics(
            new DateTime(2026, 9, 1),
            new DateTime(2026, 9, 20, 0, 0, 0));

        var category = statistics.Single(item => item.Category.Id == 1);

        Assert.Equal(3, category.OrderCount);
        Assert.Equal(1000m, category.AverageAmount);
        Assert.Equal(3000m, category.TotalAmount);
        Assert.Equal(data.Categories.Count, statistics.Count);
    }

    /// <summary>
    /// Проверяет нулевую статистику периода без заказов
    /// </summary>
    [Fact]
    public void CategoryStatistics_ReturnsZerosForEmptyPeriod()
    {
        var data = new FoodDeliveryData();
        var repository = new AnalyticsRepository(data);

        var statistics = repository.GetCategoryStatistics(
            new DateTime(2020, 1, 1),
            new DateTime(2020, 1, 2));

        Assert.Equal(data.Categories.Count, statistics.Count);

        Assert.All(statistics, item =>
        {
            Assert.Equal(0, item.OrderCount);
            Assert.Equal(0m, item.AverageAmount);
            Assert.Equal(0m, item.TotalAmount);
        });
    }

    /// <summary>
    /// Проверяет клиента с наибольшей суммой заказов
    /// </summary>
    [Fact]
    public void TopSpendingCustomer_ReturnsExpectedCustomer()
    {
        var data = new FoodDeliveryData();
        var repository = new AnalyticsRepository(data);

        var result = repository.GetTopSpendingCustomer();

        Assert.NotNull(result);
        Assert.Equal("Смирнова", result.Customer.LastName);
        Assert.Equal("Анна", result.Customer.FirstName);
        Assert.Equal("Викторовна", result.Customer.Patronymic);
        Assert.Equal(2820m, result.TotalAmount);
    }

    /// <summary>
    /// Проверяет отсутствие результатов доставки и лучшего клиента без заказов
    /// </summary>
    [Fact]
    public void Queries_HandleEmptyOrders()
    {
        var data = new FoodDeliveryData();
        data.Orders.Clear();

        var repository = new AnalyticsRepository(data);

        Assert.Empty(repository.GetOrdersWithMinimumDeliveryTime());
        Assert.Null(repository.GetTopSpendingCustomer());
        Assert.Empty(repository.GetRestaurantCustomers(1));

        Assert.All(
            repository.GetTopRestaurants(),
            item => Assert.Equal(0, item.OrderCount));
    }
}
