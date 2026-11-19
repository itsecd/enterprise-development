using FoodDelivery.Tests.Fixtures;

namespace FoodDelivery.Tests;

/// <summary>
/// Тесты аналитических запросов сервиса доставки еды
/// </summary>
public class DeliveryAnalyticsTests(FoodDeliveryFixture fixture) : IClassFixture<FoodDeliveryFixture>
{
    /// <summary>
    /// Выводит топ 5 ресторанов по количеству заказов
    /// </summary>
    [Fact]
    public void Top5RestaurantsByOrderCount()
    {
        var expectedRestaurantsCount = 5;
        var expectedTopRestaurantName = "Тануки";
        var expectedTopRestaurantOrderCount = 5;

        var topRestaurants = fixture.Orders
            .GroupBy(o => o.Restaurant)
            .Select(g => new { Restaurant = g.Key, OrderCount = g.Count() })
            .OrderByDescending(x => x.OrderCount)
            .Take(5)
            .ToList();

        Assert.Equal(expectedRestaurantsCount, topRestaurants.Count);
        Assert.Equal(expectedTopRestaurantName, topRestaurants.First().Restaurant.Name);
        Assert.Equal(expectedTopRestaurantOrderCount, topRestaurants.First().OrderCount);
    }

    /// <summary>
    /// Выводит список заказов с минимальным временем доставки
    /// </summary>
    [Fact]
    public void OrdersWithMinimumDeliveryTime()
    {
        var expectedMinDuration = 20;
        var expectedOrdersCount = 2;
        int[] expectedOrderIds = [10, 11];

        var minDuration = fixture.Orders
            .Where(o => o.DeliveredAt.HasValue)
            .Min(o => (o.DeliveredAt!.Value - o.CookedAt).TotalMinutes);

        var fastestOrders = fixture.Orders
            .Where(o => o.DeliveredAt.HasValue
                && Math.Abs((o.DeliveredAt.Value - o.CookedAt).TotalMinutes - minDuration) < 0.01)
            .ToList();

        Assert.Equal(expectedMinDuration, minDuration);
        Assert.Equal(expectedOrdersCount, fastestOrders.Count);
        Assert.Equal(expectedOrderIds, fastestOrders.Select(o => o.Id));
    }

    /// <summary>
    /// Выводит сведения обо всех клиентах, заказывавших в выбранном ресторане, упорядочить по ФИО
    /// </summary>
    [Fact]
    public void ClientsOfSelectedRestaurantOrderedByName()
    {
        // Ресторан "Тануки"
        var targetRestaurantId = 2;
        string[] expectedClientNames =
        [
            "Алексеева Ольга Игоревна",
            "Васильева Елена Николаевна",
            "Кузнецов Сергей Петрович",
            "Попов Игорь Сергеевич"
        ];

        var orderedClients = fixture.Orders
            .Where(o => o.RestaurantId == targetRestaurantId)
            .Select(o => o.Client)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.Equal(expectedClientNames, orderedClients.Select(c => c.FullName));
    }

    /// <summary>
    /// Выводит сводную информацию о заказах по каждой категории блюд за указанный период
    /// </summary>
    [Fact]
    public void CategorySummaryForPeriod()
    {
        var startDate = new DateTime(2026, 10, 1, 0, 0, 0);
        var endDate = new DateTime(2026, 10, 5, 23, 59, 59);
        var expectedCategoryId = 1;
        var expectedCategoryName = "Пицца";
        var expectedOrderCount = 5;
        var expectedTotalCategorySum = 6550m;
        var expectedAverageCategoryAmount = 1310m;

        var report = fixture.Orders
            .Where(o => o.CookedAt >= startDate && o.CookedAt <= endDate)
            .SelectMany(o => o.Items.Select(item => new { Order = o, Item = item }))
            .GroupBy(x => x.Item.Dish.CategoryId)
            .Select(g => new
            {
                CategoryId = g.Key,
                CategoryName = fixture.Categories.First(c => c.Id == g.Key).Name,
                OrderCount = g.Select(x => x.Order.Id).Distinct().Count(),
                TotalCategorySum = g.Sum(x => x.Item.Dish.Price * x.Item.Quantity),
                AverageCategoryAmount = g.Sum(x => x.Item.Dish.Price * x.Item.Quantity) / g.Select(x => x.Order.Id).Distinct().Count()
            })
            .ToList();

        Assert.NotEmpty(report);

        // Проверяем категорию "Пицца" (Id = 1)
        var pizzaStats = report.FirstOrDefault(r => r.CategoryId == expectedCategoryId);
        Assert.NotNull(pizzaStats);
        Assert.Equal(expectedCategoryName, pizzaStats.CategoryName);
        Assert.Equal(expectedOrderCount, pizzaStats.OrderCount);
        Assert.Equal(expectedTotalCategorySum, pizzaStats.TotalCategorySum);
        Assert.Equal(expectedAverageCategoryAmount, pizzaStats.AverageCategoryAmount);
    }

    /// <summary>
    /// Выводит информацию о клиентах, которые потратили наибольшую сумму за все время работы
    /// </summary>
    [Fact]
    public void ClientsWithHighestTotalSpend()
    {
        string[] expectedClientNames = ["Алексеева Ольга Игоревна"];
        var expectedTotalSpent = 11000m;

        var clientSpends = fixture.Orders
            .GroupBy(o => o.Client)
            .Select(g => new { Client = g.Key, TotalSpent = g.Sum(o => o.TotalAmount) })
            .ToList();

        var maxTotalSpent = clientSpends.Max(x => x.TotalSpent);
        var topSpenders = clientSpends
            .Where(x => x.TotalSpent == maxTotalSpent)
            .ToList();

        Assert.Equal(expectedClientNames, topSpenders.Select(x => x.Client.FullName));
        Assert.All(topSpenders, x => Assert.Equal(expectedTotalSpent, x.TotalSpent));
    }
}
