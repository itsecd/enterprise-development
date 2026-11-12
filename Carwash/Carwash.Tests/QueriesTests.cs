namespace Carwash.Tests;

/// <summary>
/// Тесты
/// </summary>
public class QueriesTests(QueriesTestsFixture fixture) : IClassFixture<QueriesTestsFixture>
{
    /// <summary>
    /// Проверяет данные о топ-5 клиентов по количеству посещений.
    /// </summary>
    [Fact]
    public void GetTopFiveClients_ShouldReturnClientsOrderedByVisitCount()
    {
        List<(string FullName, int VisitCount)> expected =
        [
            ("Иван Петров", 6),
            ("Анна Сидорова", 5),
            ("Алексей Смирнов", 4),
            ("Мария Иванова", 3),
            ("Дмитрий Кузнецов", 2)
        ];

        var result = fixture.Orders
            .GroupBy(order => order.Client)
            .Select(group => new
            {
                Client = group.Key,
                VisitCount = group.Count()
            })
            .OrderByDescending(item => item.VisitCount)
            .ThenBy(item => item.Client.Id)
            .Take(5)
            .Select(item => (item.Client.FullName, item.VisitCount))
            .ToList();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет информацию об автомобилях, которые находятся на мойке в текущий момент времени.
    /// </summary>
    [Fact]
    public void GetCarsCurrentlyAtCarWash_ShouldReturnActiveOrders()
    {
        List<int> expected = [1, 2, 3];

        var currentTime = fixture.CurrentTime;

        var result = fixture.Orders
            .Where(order =>
                order.StartTime <= currentTime &&
                order.EndTime > currentTime)
            .Select(order => order.Car)
            .Distinct()
            .Select(car => car.Id)
            .OrderBy(id => id)
            .ToList();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет информацию о топ-5 наиболее популярных услуг.
    /// </summary>
    [Fact]
    public void GetTopFivePopularServices_ShouldReturnServicesOrderedByOrderCount()
    {
        List<(string Name, int OrderCount)> expected =
        [
            ("Экспресс-мойка", 7),
            ("Комплексная мойка", 6),
            ("Премиум-мойка", 5),
            ("Мойка внедорожника", 3),
            ("Люкс-мойка", 2)
        ];

        var result = fixture.Orders
            .GroupBy(order => order.Service)
            .Select(group => new
            {
                Service = group.Key,
                OrderCount = group.Count()
            })
            .OrderByDescending(item => item.OrderCount)
            .ThenBy(item => item.Service.Id)
            .Take(5)
            .Select(item => (item.Service.Name, item.OrderCount))
            .ToList();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет информацию о времени освобождения выбранного бокса.
    /// </summary>
    [Fact]
    public void GetBoxReleaseTime_ShouldReturnLatestActiveOrderEndTime()
    {
        const int boxNumber = 1;
        var currentTime = fixture.CurrentTime;
        var expected = new DateTime(2026, 9, 24, 13, 10, 0);

        var result = fixture.Orders
            .Where(order =>
                order.BoxNumber == boxNumber &&
                order.StartTime <= currentTime &&
                order.EndTime > currentTime)
            .Max(order => order.EndTime);

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет информацию о суммарной выручке по каждой услуге.
    /// </summary>
    [Fact]
    public void GetTotalRevenueByService_ShouldReturnCorrectRevenue()
    {
        List<(int ServiceId, decimal Revenue)> expected =
        [
            (1, 5600m),
            (2, 9000m),
            (3, 12500m),
            (4, 9000m),
            (5, 8000m)
        ];

        var result = fixture.Orders
            .GroupBy(order => order.Service)
            .Select(group => new
            {
                Service = group.Key,
                Revenue = group.Sum(order => order.Service.Price)
            })
            .OrderBy(item => item.Service.Id)
            .Select(item => (item.Service.Id, item.Revenue))
            .ToList();

        Assert.Equal(expected, result);
    }
}
