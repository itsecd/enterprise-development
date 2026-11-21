namespace CarWash.Tests;

/// <summary>
/// Юнит-тесты бизнес-логики автомойки
/// </summary>
public class CarWashTests(CarWashFixture fixture) : IClassFixture<CarWashFixture>
{
    /// <summary>
    /// Вывести топ 5 клиентов по количеству посещений
    /// </summary>
    [Fact]
    public async Task GetTop5Clients_WhenCalled_ReturnsClientsOrderedByVisits()
    {
        List<string> expectedNames =
        [
            "Иванов Иван Иванович",
            "Петров Сергей Алексеевич",
            "Сидоров Алексей Михайлович",
            "Смирнова Елена Павловна",
            "Кузнецов Дмитрий Николаевич"
        ];

        var topClients = await fixture.AnalyticsService.GetTop5ClientsAsync();
        var actualNames = topClients.Select(t => t.Client.FullName).ToList();

        Assert.Equal(expectedNames, actualNames);
    }

    /// <summary>
    /// Вывести информацию об автомобилях, находящихся на мойке в данный момент
    /// </summary>
    [Fact]
    public async Task GetCurrentCarsAtWash_WhenTimeIsSpecified_ReturnsActiveCars()
    {
        var currentTime = new DateTime(2026, 6, 1, 10, 15, 0);

        List<string> expectedPlates =
        [
            "А101АА663",
            "В206ВВ663",
            "Е456ЕЕ163",
            "С302СС363"
        ];

        var currentCars = await fixture.AnalyticsService.GetCurrentCarsAsync(currentTime);
        var actualPlates = currentCars
            .Select(c => c.LicensePlate)
            .OrderBy(plate => plate)
            .ToList();

        Assert.Equal(expectedPlates, actualPlates);
    }

    /// <summary>
    /// Вывести 5 самых популярных услуг
    /// </summary>
    [Fact]
    public async Task GetTop5PopularServices_WhenCalled_ReturnsServicesOrderedByPopularity()
    {
        List<string> expectedServices =
        [
            "Экспресс-мойка",
            "Комплексная мойка",
            "Стандартная мойка SUV",
            "Химчистка салона",
            "Мойка грузовика"
        ];

        var topServices = await fixture.AnalyticsService.GetTop5PopularServicesAsync();
        var actualServices = topServices.Select(t => t.Service.Name).ToList();

        Assert.Equal(expectedServices, actualServices);
    }

    /// <summary>
    /// Узнать для выбранного бокса мойки, начиная с какого времени он освобождается
    /// </summary>
    [Fact]
    public async Task GetBoxFreeTime_WhenBoxIsBusy_ReturnsNextAvailableTime()
    {
        var targetBox = 1;
        var currentTime = new DateTime(2026, 6, 1, 10, 10, 0);
        var expectedFreeTime = new DateTime(2026, 6, 1, 10, 20, 0);

        var result = await fixture.AnalyticsService.GetBoxFreeTimeAsync(targetBox, currentTime);

        Assert.Equal(targetBox, result.BoxNumber);
        Assert.Equal(expectedFreeTime, result.FreeTime);
    }

    /// <summary>
    /// Для каждой услуги вывести суммарную выручку
    /// </summary>
    [Fact]
    public async Task GetRevenueForEachService_WhenCalled_CalculatesTotalRevenueCorrectly()
    {
        var expectedRevenue = new Dictionary<string, decimal>
        {
            ["Экспресс-мойка"] = 3000m,
            ["Комплексная мойка"] = 5600m,
            ["Стандартная мойка SUV"] = 3300m,
            ["Химчистка салона"] = 9000m,
            ["Мойка грузовика"] = 5600m,
            ["Полировка кузова"] = 3500m,
            ["Мойка двигателя"] = 1200m,
            ["Обработка воском"] = 500m,
            ["Мойка днища"] = 900m,
            ["Мойка минивэна"] = 1800m
        };

        var actualRevenue = await fixture.AnalyticsService.GetRevenueForEachServiceAsync();

        Assert.Equal(expected: expectedRevenue, actual: actualRevenue);
    }
}
