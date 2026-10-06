using FoodDelivery.Tests.Fixtures;

namespace FoodDelivery.Tests;

public class DeliveryAnalyticsTests : IClassFixture<FoodDeliveryFixture>
{
    private readonly FoodDeliveryFixture _fixture;

    public DeliveryAnalyticsTests(FoodDeliveryFixture fixture)
    {
        _fixture = fixture;
    }

    /// <summary>
    /// Выводит топ 5 ресторанов по количеству заказов
    /// </summary>
    [Fact]
    public void Top5RestaurantsByOrderCount()
    {
        var topRestaurants = _fixture.Orders
            .GroupBy(o => o.Restaurant)
            .Select(g => new { Restaurant = g.Key, OrderCount = g.Count() })
            .OrderByDescending(x => x.OrderCount)
            .Take(5)
            .ToList();
        Assert.Equal(5, topRestaurants.Count);
        Assert.Equal("Тануки", topRestaurants.First().Restaurant.Name);
        Assert.Equal(5, topRestaurants.First().OrderCount);
    }
    /// <summary>
    /// Выводит список заказов с минимальным временем доставки
    /// </summary>
    [Fact]
    public void OrdersWithMinimumDeliveryTime()
    {
        var minDuration = _fixture.Orders
            .Where(o => o.DeliveredAt.HasValue)
            .Min(o => (o.DeliveredAt!.Value - o.CookedAt).TotalMinutes);
        var fastestOrders = _fixture.Orders
            .Where(o => o.DeliveredAt.HasValue &&
                        Math.Abs((o.DeliveredAt.Value - o.CookedAt).TotalMinutes - minDuration) < 0.01)
            .ToList();
        Assert.Equal(20, minDuration);
        Assert.Equal(2, fastestOrders.Count);
        Assert.Contains(fastestOrders, o => o.Id == 10);
        Assert.Contains(fastestOrders, o => o.Id == 11);
    }
    /// <summary>
    /// Выводит сведения обо всех клиентах, заказывавших в выбранном ресторане, упорядочить по ФИО
    /// </summary>
    [Fact]
    public void ClientsOfSelectedRestaurantOrderedByName()
    {
        /// <summary>
        /// Ресторан "Тануки"
        /// </summary>
        var targetRestaurantId = 2; 
        var orderedClients = _fixture.Orders
            .Where(o => o.RestaurantId == targetRestaurantId)
            .Select(o => o.Client)
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.NotEmpty(orderedClients);
        for (var i = 0; i < orderedClients.Count - 1; i++)
        {
            Assert.True(string.Compare(orderedClients[i].FullName, orderedClients[i + 1].FullName, StringComparison.OrdinalIgnoreCase) <= 0);
        }
    }
 
    /// <summary>
    /// Выводит сводную информацию о заказах по каждой категории блюд за указанный период
    /// </summary>
    [Fact]
    public void CategorySummaryForPeriod()
    {
        var startDate = new DateTime(2026, 10, 1, 0, 0, 0);
        var endDate = new DateTime(2026, 10, 5, 23, 59, 59);
        var report = _fixture.Orders
            .Where(o => o.CookedAt >= startDate && o.CookedAt <= endDate)
            .SelectMany(o => o.Items.Select(item => new { Order = o, Item = item }))
            .GroupBy(x => x.Item.Dish.CategoryId)
            .Select(g => new
            {
                CategoryId = g.Key,
                CategoryName = _fixture.Categories.First(c => c.Id == g.Key).Name,
                OrderCount = g.Select(x => x.Order.Id).Distinct().Count(),
                TotalCategorySum = g.Sum(x => x.Item.Dish.Price * x.Item.Quantity),
                AverageCategoryAmount = g.Sum(x => x.Item.Dish.Price * x.Item.Quantity) / g.Select(x => x.Order.Id).Distinct().Count()
            })
            .ToList();

        Assert.NotEmpty(report);

        /// <summary>
        /// Проверяем категорию "Пицца" (Id = 1)
        /// </summary> 
        var pizzaStats = report.FirstOrDefault(r => r.CategoryId == 1);
        Assert.NotNull(pizzaStats);
        Assert.Equal("Пицца", pizzaStats.CategoryName);
        Assert.Equal(5, pizzaStats.OrderCount);
        Assert.Equal(6550m, pizzaStats.TotalCategorySum);
        Assert.Equal(1310m, pizzaStats.AverageCategoryAmount);
    }

    /// <summary>
    /// Вывести информацию о клиенте, который потратил наибольшую сумму за все время работы
    /// </summary>
    [Fact]
    public void ClientWithHighestTotalSpend()
    {
        var topSpender = _fixture.Orders
            .GroupBy(o => o.Client)
            .Select(g => new { Client = g.Key, TotalSpent = g.Sum(o => o.TotalAmount) })
            .OrderByDescending(x => x.TotalSpent)
            .FirstOrDefault();

        Assert.NotNull(topSpender);
        Assert.Equal("Алексеева Ольга Игоревна", topSpender.Client.FullName);
        Assert.Equal(11000m, topSpender.TotalSpent);
    }
}
