using DryCleaning.Domain.Models;

namespace DryCleaning.Tests;

public class Tests
{
    /// <summary>
    /// Тест 1: Заказы в обработке, упорядоченные по дате приема
    /// </summary>
    [Fact]
    public void Orders_WhenFilteredByInProgress_ReturnsOrderedByAcceptDate()
    {
        List<int> expectedOrderIds = [4, 11, 3, 5];


        var result = DataTest.Orders
            .Where(o => o.Status == Status.InProgress)
            .OrderBy(o => o.AcceptDate)
            .Select(o => o.Id)
            .ToList();


        Assert.Equal(expectedOrderIds, result);
    }

    /// <summary>
    /// Тест 2: Топ 5 клиентов, сдавших больше всего изделий за заданный период
    /// </summary>
    [Fact]
    public void Orders_WhenGroupedByClientForPeriod_ReturnsTop5ClientsByItemsCount()
    {
        var startDate = DataTest.BaseDate.AddDays(-30);
        var endDate = DataTest.BaseDate;


        var result = DataTest.Orders
            .Where(o => o.AcceptDate >= startDate && o.AcceptDate <= endDate)
            .GroupBy(o => o.Client)
            .Select(group => new
            {
                Client = group.Key,
                ItemsCount = group.Count()
            })
            .OrderByDescending(x => x.ItemsCount)
            .Take(5)
            .ToList();


        Assert.Equal(5, result.Count);
        Assert.Equal(1, result[0].Client.Id);
        Assert.Equal("Иванов Иван Иванович", result[0].Client.FullName);
        Assert.Equal(3, result[0].ItemsCount);
        Assert.Equal(2, result[1].ItemsCount);
        Assert.Equal(2, result[2].ItemsCount);
    }

    /// <summary>
    /// Тест 3: Клиенты, чьи заказы обрабатывались дольше всего, упорядоченные по ФИО
    /// </summary>
    [Fact]
    public void Orders_WhenFilteredByMaxExecutionDays_ReturnsClientsOrderedByFullName()
    {
        List<string> expectedNames = ["Иванов Иван Иванович"];
        var maxDays = DataTest.Orders.Max(o => o.ExeDays);


        var result = DataTest.Orders
            .Where(o => o.ExeDays == maxDays)
            .Select(o => o.Client.FullName)
            .Distinct()
            .OrderBy(name => name)
            .ToList();


        Assert.Equal(expectedNames, result);
    }

    /// <summary>
    /// Тест 4: Топ-5 наиболее и наименее популярных категорий изделий за последний год
    /// </summary>
    [Fact]
    public void Orders_WhenGroupedByCategoryForLastYear_ReturnsTopAndLeastPopularCategories()
    {
        var oneYearAgo = DataTest.BaseDate.AddDays(-365);


        var categoryPopularity = DataTest.Orders
            .Where(o => o.AcceptDate >= oneYearAgo)
            .GroupBy(o => o.Item.Category)
            .Select(group => new
            {
                Category = group.Key,
                OrdersCount = group.Count()
            })
            .ToList();

        var mostPopular = categoryPopularity
            .OrderByDescending(x => x.OrdersCount)
            .Take(5)
            .ToList();

        var leastPopular = categoryPopularity
            .OrderBy(x => x.OrdersCount)
            .Take(5)
            .ToList();


        Assert.Equal(5, mostPopular.Count);
        Assert.Equal(5, leastPopular.Count);
        Assert.Equal(2, mostPopular[0].OrdersCount);
        Assert.Equal(2, mostPopular[1].OrdersCount);
        Assert.All(leastPopular, item => Assert.Equal(1, item.OrdersCount));
    }

    /// <summary>
    /// Тест 5: Клиент, который потратил наибольшую сумму за весь период
    /// </summary>
    [Fact]
    public void Orders_WhenGroupedByClientForTotalSpent_ReturnsClientsWithMaxTotalSpent()
    {
        var clientSpending = DataTest.Orders
            .GroupBy(o => o.Client)
            .Select(group => new
            {
                Client = group.Key,
                TotalSpent = group.Sum(o => o.Item.Category.Price)
            })
            .ToList();


        var maxSpent = clientSpending.Max(x => x.TotalSpent);
        var result = clientSpending
            .Where(x => x.TotalSpent == maxSpent)
            .OrderBy(x => x.Client.FullName)
            .ToList();


        Assert.Equal(2, result.Count);
        Assert.Equal("Иванов Иван Иванович", result[0].Client.FullName);
        Assert.Equal(7500m, result[0].TotalSpent);
        Assert.Equal("Федоров Сергей Александрович", result[1].Client.FullName);
        Assert.Equal(7500m, result[1].TotalSpent);
    }
}
