using DryCleaner.Shared.Enums;

namespace DryCleaner.Tests;

/// <summary>
/// Тесты запросов к данным сервиса
/// </summary>
/// <param name="fixture">Тестовый набор данных</param>
public class QueriesTests(QueriesTestsFixture fixture) : IClassFixture<QueriesTestsFixture>
{
    /// <summary>
    /// Проверяет информацию полученную о заказах, находящихся в обработке, упорядоченных по дате приема
    /// </summary>
    [Fact]
    public void InProgressOrdersInfoOrderedByAcceptedAt()
    {
        var orders = fixture.Orders.ToList();
        var expectedIds = new[] { 4, 8, 10 };

        var result = orders
            .Where(order => order.Status == OrderStatus.InProgress)
            .OrderBy(order => order.AcceptedAt)
            .ToList();

        Assert.Equal(expectedIds, result.Select(order => order.Id));
    }

    /// <summary>
    /// Проверяет информацию полученную о топ 5 самых активных клиантах за период
    /// </summary>
    [Fact]
    public void Top5MostActiveClientsForPeriod()
    {
        var dateFrom = new DateTime(2025, 1, 1);
        var dateTo = new DateTime(2026, 12, 31);

        var orders = fixture.Orders.ToList();
        var expected = new[]
        {
            new { Name = "Иванов Иван Иванович", Count = 2 },
            new { Name = "Петров Петр Петрович", Count = 2 },
            new { Name = "Сидорова Анна Сергеевна", Count = 2 },
            new { Name = "Кузнецов Алексей Владимирович", Count = 2 },
            new { Name = "Смирнова Елена Андреевна", Count = 2 }
        };

        var result = orders
            .Where(order =>
            order.AcceptedAt >= dateFrom &&
            order.AcceptedAt <= dateTo)
            .GroupBy(order => order.Client)
            .Select(group => new
            {
                Client = group.Key,
                ProductsCount = group.Count()
            })
            .OrderByDescending(x => x.ProductsCount)
            .Take(5)
            .ToList();

        Assert.Equal(expected.Select(x => x.Name), result.Select(x => x.Client.Name));

        Assert.Equal(expected.Select(x => x.Count), result.Select(x => x.ProductsCount));
    }


    /// <summary>
    /// Проверяет информацию полученную о клиентах с самым длительными заказами упорядоченных по имени
    /// </summary>
    [Fact]
    public void ClientsWithLongestProcessingOrdersOrderedByName()
    {
        var orders = fixture.Orders.ToList();
        var expected = new[]
        {
            new { Name = "Иванов Иван Иванович", Average = 6.0 },
            new { Name = "Кузнецов Алексей Владимирович", Average = 17.5 },
            new { Name = "Петров Петр Петрович", Average = 6.5 },
            new { Name = "Сидорова Анна Сергеевна", Average = 7.0 },
            new { Name = "Смирнова Елена Андреевна", Average = 8.0 }
        };

        var result = orders
            .GroupBy(order => order.Client)
            .Select(group => new
            {
                Client = group.Key,
                AverageProcessingDays =
                    group.Average(order => order.ProcessingDays)
            })
            .OrderBy(x => x.Client.Name)
            .ToList();

        Assert.Equal(expected.Select(x => x.Name), result.Select(x => x.Client.Name));

        Assert.Equal(expected.Select(x => x.Average), result.Select(x => x.AverageProcessingDays));
    }

    /// <summary>
    /// Проверяет информацию полученную о топ 5 самых популярных категориях за последний год
    /// </summary>
    [Fact]
    public void Top5MostAndLeastPopularCategoriesForLastYear()
    {
        var dateTo = new DateTime(2026, 9, 18);
        var dateFrom = new DateTime(2025, 1, 1);

        var orders = fixture.Orders.ToList();
        var categories = fixture.Categories.ToList();
        var expectedMostPopular = new[]
        {
            new { Name = "Верхняя одежда", Count = 4 },
            new { Name = "Костюмы", Count = 3 },
            new { Name = "Платья", Count = 1 },
            new { Name = "Рубашки", Count = 1 },
            new { Name = "Брюки", Count = 1 },
        };
        var expectedLeastPopular = new[]
        {
            new { Name = "Пальто", Count = 0 },
            new { Name = "Куртки", Count = 0 },
            new { Name = "Шторы", Count = 0 },
            new { Name = "Одеяла", Count = 0 },
            new { Name = "Обувь", Count = 0 }
        };

        var result = categories
            .GroupJoin(
                orders.Where(order =>
                    order.AcceptedAt >= dateFrom &&
                    order.AcceptedAt <= dateTo),
                category => category.Id,
                order => order.Product.Category.Id,
                (category, categoryOrders) => new
                {
                    Category = category,
                    ProductsCount = categoryOrders.Count()
                })
            .ToList();

        var mostPopular = result
            .OrderByDescending(x => x.ProductsCount)
            .Take(5)
            .ToList();

        var leastPopular = result
            .OrderBy(x => x.ProductsCount)
            .Take(5)
            .ToList();

        Assert.Equal(expectedMostPopular.Select(x => x.Name), mostPopular.Select(x => x.Category.Name));

        Assert.Equal(expectedMostPopular.Select(x => x.Count), mostPopular.Select(x => x.ProductsCount));

        Assert.Equal(expectedLeastPopular.Select(x => x.Name), leastPopular.Select(x => x.Category.Name));

        Assert.Equal(expectedLeastPopular.Select(x => x.Count), leastPopular.Select(x => x.ProductsCount));
    }


    /// <summary>
    /// Проверяет информацию полученную о клиенте потратившем самую большую сумму за весь период работы сервиса
    /// </summary>
    [Fact]
    public void ClientWhoSpentTheMostMoney()
    {
        var orders = fixture.Orders.ToList();
        var expected = new { Name = "Петров Петр Петрович", TotalSpent = 3000m };

        var result = orders
            .GroupBy(order => order.Client)
            .Select(group => new
            {
                Client = group.Key,
                TotalSpent = group.Sum(order =>
                    order.Product.Category.CleaningPrice)
            })
            .OrderByDescending(x => x.TotalSpent)
            .First();

        Assert.Equal(expected.Name, result.Client.Name);
        Assert.Equal(expected.TotalSpent, result.TotalSpent);

    }
}
