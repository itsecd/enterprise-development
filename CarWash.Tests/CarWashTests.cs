using CarWash.Domain.Entities;

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
    public void GetTop5Clients_WhenCalled_ReturnsClientsOrderedByVisits()
    {
        List<string> expectedNames =
        [
            "Иванов Иван Иванович",
            "Петров Сергей Алексеевич",
            "Сидоров Алексей Михайлович",
            "Смирнова Елена Павловна",
            "Кузнецов Дмитрий Николаевич"
        ];

        var actualNames = fixture.Orders
            .GroupBy(o => o.Client)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.Id)
            .Take(5)
            .Select(g => g.Key.FullName)
            .ToList();

        Assert.Equal(expectedNames, actualNames);
    }

    /// <summary>
    /// Вывести информацию об автомобилях, находящихся на мойке в данный момент
    /// </summary>
    [Fact]
    public void GetCurrentCarsAtWash_WhenTimeIsSpecified_ReturnsActiveCars()
    {
        var currentTime = new DateTime(2026, 6, 1, 10, 15, 0);

        List<string> expectedPlates =
        [
            "А101АА663",
            "В206ВВ663",
            "Е456ЕЕ163",
            "С302СС363"
        ];

        var actualPlates = fixture.Orders
            .Where(o => o.StartTime <= currentTime && currentTime < o.StartTime.AddMinutes(o.Service.DurationMinutes))
            .Select(o => o.Car.LicensePlate)
            .OrderBy(plate => plate)
            .ToList();

        Assert.Equal(expectedPlates, actualPlates);
    }

    /// <summary>
    /// Вывести 5 самых популярных услуг
    /// </summary>
    [Fact]
    public void GetTop5PopularServices_WhenCalled_ReturnsServicesOrderedByPopularity()
    {
        List<string> expectedServices =
        [
            "Экспресс-мойка",
            "Комплексная мойка",
            "Стандартная мойка SUV",
            "Химчистка салона",
            "Мойка грузовика"
        ];

        var actualServices = fixture.Orders
            .GroupBy(o => o.Service)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.Id)
            .Take(5)
            .Select(g => g.Key.Name)
            .ToList();

        Assert.Equal(expectedServices, actualServices);
    }

    /// <summary>
    /// Узнать для выбранного бокса мойки, начиная с какого времени он освобождается
    /// </summary>
    [Fact]
    public void GetBoxFreeTime_WhenBoxIsBusy_ReturnsNextAvailableTime()
    {
        var targetBox = 1;
        var currentTime = new DateTime(2026, 6, 1, 10, 10, 0);
        var expectedFreeTime = new DateTime(2026, 6, 1, 10, 20, 0);

        var busyOrders = fixture.Orders
            .Where(o => o.BoxNumber == targetBox && o.StartTime.AddMinutes(o.Service.DurationMinutes) > currentTime)
            .OrderBy(o => o.StartTime)
            .ToList();

        var freeTime = currentTime;

        foreach (var order in busyOrders)
        {
            var orderEndTime = order.StartTime.AddMinutes(order.Service.DurationMinutes);

            if (order.StartTime <= freeTime)
            {
                if (orderEndTime > freeTime)
                {
                    freeTime = orderEndTime;
                }
            }
            else
            {
                break;
            }
        }

        Assert.Equal(expectedFreeTime, freeTime);
    }

    /// <summary>
    /// Для каждой услуги вывести суммарную выручку
    /// </summary>
    [Fact]
    public void GetRevenueForEachService_WhenCalled_CalculatesTotalRevenueCorrectly()
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

        var actualRevenue = fixture.Orders
            .GroupBy(o => o.Service.Name)
            .ToDictionary(g => g.Key, g => g.Sum(o => o.Service.Price));

        Assert.Equal(expected: expectedRevenue, actual: actualRevenue);
    }
}