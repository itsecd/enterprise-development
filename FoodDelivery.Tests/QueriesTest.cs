namespace FoodDelivery.Tests;

/// <summary>
/// Тесты аналитических запросов к данным службы доставки еды.
/// </summary>
public class QueriesTest(
    QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
{

    /// <summary>
    /// Проверяет получение пяти ресторанов с наибольшим количеством заказов.
    /// </summary>
    [Fact]
    public void Top5RestaurantsByOrderCount()
    {
        var expectedRestaurantNames = new[]
        {
            "Вкусно и точка",
            "Ростикс"
        };

        var expectedOrderCounts = new[]
        {
            3,
            3,
            2,
            2,
            2
        };

        var result = fixture.Orders
            .GroupBy(order => order.Restaurant)
            .Select(group => new
            {
                Restaurant = group.Key!,
                OrderCount = group.Count()
            })
            .OrderByDescending(x => x.OrderCount)
            .ThenBy(x => x.Restaurant.Name)
            .Take(5)
            .ToList();

        Assert.Equal(5, result.Count);

        Assert.Equal(
            expectedRestaurantNames,
            result
                .Where(x => x.OrderCount == 3)
                .Select(x => x.Restaurant.Name));

        Assert.Equal(
            expectedOrderCounts,
            result.Select(x => x.OrderCount));
    }

    /// <summary>
    /// Проверяет поиск заказа с минимальным временем доставки.
    /// </summary>
    [Fact]
    public void OrdersWithMinimumDeliveryTime()
    {
        var expectedOrderId = 11;
        var expectedDeliveryTime = TimeSpan.FromMinutes(25);

        var deliveredOrders = fixture.Orders
            .Where(order => order.DeliveryDateTime.HasValue)
            .ToList();

        var minimumDeliveryTime = deliveredOrders
            .Min(order => order.DeliveryDateTime!.Value - order.OrderDateTime);

        var result = deliveredOrders
            .Where(order =>
                order.DeliveryDateTime!.Value - order.OrderDateTime == minimumDeliveryTime)
            .ToList();

        var order = Assert.Single(result);

        Assert.Equal(expectedOrderId, order.Id);
        Assert.Equal(expectedDeliveryTime, minimumDeliveryTime);
    }

    /// <summary>
    /// Проверяет получение клиентов, которые оформляли заказы
    /// в выбранном ресторане.
    /// </summary>
    [Fact]
    public void ClientsWhoOrderedFromSelectedRestaurant()
    {
        var selectedRestaurantId = 1;

        var expectedClientNames = new[]
        {
            "Зубенко Михаил Петрович",
            "Петушков Илья Викторович"
        };

        var result = fixture.Orders
            .Where(order => order.RestaurantId == selectedRestaurantId)
            .Select(order => order.Client!)
            .DistinctBy(client => client.Id)
            .OrderBy(client => client.FullName)
            .ToList();

        Assert.Equal(
            expectedClientNames,
            result.Select(client => client.FullName));
    }

    /// <summary>
    /// Проверяет получение статистики по категориям блюд
    /// за указанный период.
    /// </summary>
    [Fact]
    public void CategorySummaryForSpecifiedPeriod()
    {
        var startDate = new DateTime(2026, 9, 1);
        var endDate = new DateTime(2026, 9, 5);

        var expectedCategoryCount = 8;
        var expectedCategoryName = "Пицца";
        var expectedOrderCount = 2;
        var expectedAverageOrderPrice = 760m;
        var expectedTotalOrderPrice = 1520m;

        var result = fixture.Orders
            .Where(order => order.OrderDateTime >= startDate &&
                            order.OrderDateTime < endDate)
            .SelectMany(order => order.Dishes
                .Select(dish => new
                {
                    Category = dish.Category!,
                    Order = order
                }))
            .GroupBy(x => x.Category.Id)
            .Select(group => new
            {
                Category = group.First().Category,
                OrderCount = group
                    .Select(x => x.Order.Id)
                    .Distinct()
                    .Count(),
                AverageOrderPrice = group
                    .Select(x => x.Order)
                    .DistinctBy(order => order.Id)
                    .Average(order => order.TotalPrice),
                TotalOrderPrice = group
                    .Select(x => x.Order)
                    .DistinctBy(order => order.Id)
                    .Sum(order => order.TotalPrice)
            })
            .OrderBy(x => x.Category.Id)
            .ToList();

        Assert.Equal(expectedCategoryCount, result.Count);

        var pizza = result.Single(x => x.Category.Id == 1);

        Assert.Equal(expectedCategoryName, pizza.Category.Name);
        Assert.Equal(expectedOrderCount, pizza.OrderCount);
        Assert.Equal(expectedAverageOrderPrice, pizza.AverageOrderPrice);
        Assert.Equal(expectedTotalOrderPrice, pizza.TotalOrderPrice);
    }

    /// <summary>
    /// Проверяет поиск клиентов с максимальной суммой расходов
    /// за всё время.
    /// </summary>
    [Fact]
    public void ClientsWhoSpentTheMost()
    {
        var expectedClientName = "Иванов Иван Иванович";
        var expectedTotalSpent = 2990m;

        var clientSpending = fixture.Orders
            .GroupBy(order => order.ClientId)
            .Select(group => new
            {
                Client = fixture.Clients
                    .Single(client => client.Id == group.Key),
                TotalSpent = group.Sum(order => order.TotalPrice)
            })
            .ToList();

        var maximumSpent = clientSpending
            .Max(x => x.TotalSpent);

        var result = clientSpending
            .Where(x => x.TotalSpent == maximumSpent)
            .ToList();

        Assert.Equal(expectedTotalSpent, maximumSpent);

        Assert.Contains(
            result,
            x => x.Client.FullName == expectedClientName);
    }
}