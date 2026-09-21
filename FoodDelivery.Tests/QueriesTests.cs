using FoodDelivery.Tests.TestData;

namespace FoodDelivery.Tests;

public class QueriesTests
{
    private readonly FoodDeliveryTestData _data = new();

    /// <summary>
    /// Проверяет получение пяти ресторанов с наибольшим количеством заказов
    /// </summary>
    [Fact]
    public void GetTopFiveRestaurantsByOrderCount_ShouldReturnExpectedRestaurants()
    {
        var result = _data.Orders
            .CountBy(order => order.RestaurantId)
            .OrderByDescending(item => item.Value)
            .Take(5)
            .Select(item => _data.Restaurants.Single(
                restaurant => restaurant.Id == item.Key))
            .ToList();

        var expectedNames = new[]
        {
            "Pizza House",
            "Burger Point",
            "Sushi Time",
            "Pasta Place",
            "Food Corner"
        };

        Assert.Equal(
            expectedNames,
            result.Select(restaurant => restaurant.Name));
    }

    /// <summary>
    /// Проверяет получение всех заказов с минимальным временем доставки
    /// </summary>
    [Fact]
    public void GetOrdersWithMinimumDeliveryTime_ShouldReturnAllFastestOrders()
    {
        var minimumDeliveryTime = _data.Orders
            .Min(order => order.DeliveryTime - order.OrderTime);

        var result = _data.Orders
            .Where(order =>
                order.DeliveryTime - order.OrderTime == minimumDeliveryTime)
            .ToList();

        var expectedOrderIds = new[] { 3, 10 };

        Assert.Equal(
            expectedOrderIds,
            result.Select(order => order.Id));
    }

    /// <summary>
    /// Проверяет получение клиентов выбранного ресторана в алфавитном порядке
    /// </summary>
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void GetCustomersByRestaurant_ShouldReturnSortedUniqueCustomers(
        int restaurantId)
    {
        var result = _data.Orders
            .Where(order => order.RestaurantId == restaurantId)
            .Select(order => order.Customer)
            .Distinct()
            .OrderBy(customer => customer.LastName)
            .ThenBy(customer => customer.FirstName)
            .ThenBy(customer => customer.Patronymic)
            .ToList();

        var expectedLastNames = restaurantId switch
        {
            1 => new[] { "Ivanov", "Petrov", "Sidorov" },
            2 => new[] { "Ivanov", "Kuznetsov", "Popov", "Smirnov" },
            _ => throw new ArgumentOutOfRangeException(nameof(restaurantId))
        };

        Assert.Equal(
            expectedLastNames,
            result.Select(customer => customer.LastName));
    }
}