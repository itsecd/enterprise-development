using AutoService.Domain.Models;

namespace AutoService.Tests;

/// <summary>
/// Класс, хранящий юнит-тесты
/// </summary>
public class AutoServiceTests(AutoServiceFixture fixture, ITestOutputHelper output)
    : IClassFixture<AutoServiceFixture>
{
    /// <summary>
    /// Тест 1: механики по виду работ
    /// </summary>
    [Fact]
    public void GetMechanicsByWorkCategory()
    {
        const string diagnosticsWorkName = "Диагностика ходовой";

        var expected = new[]
        {
            "Мошкова Мошка Мошковна",
            "Родионов Михаил Петрович"
        };

        var work = fixture.Works.First(w => w.Name == diagnosticsWorkName);

        var actual  = fixture.Mechanics
            .Where(m => m.Specialization.ToString() == work.Category)
            .OrderBy(m => m.FullName)
            .ToList();

        foreach (var m in actual)
            output.WriteLine($"{m.FullName} — {m.Specialization}");

        Assert.Equal(expected, actual.Select(m => m.FullName).ToList());
    }

    /// <summary>
    /// Тест 2: клиенты указанного механика, упорядоченные по ФИО
    /// </summary>
    [Fact]
    public void GetClientsByMechanic()
    {
        var expected = new[]
        {
            "Андреев Андрей Андреевич",
            "Иванов Иван Иванович"
        };

        var mechanicId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var mechanic = fixture.Mechanics.Single(m => m.Id == mechanicId);

        var actual = fixture.Clients
            .Where(c => fixture.Orders.Any(o => o.ClientId == c.Id && o.MechanicId == mechanic.Id))
            .OrderBy(c => c.FullName)
            .ToList();

        output.WriteLine($"Механик: {mechanic.FullName}");
        foreach (var c in actual)
            output.WriteLine($"{c.FullName} — {c.Phone}");

        Assert.Equal(expected, actual.Select(c => c.FullName).ToList());
    }

    /// <summary>
    /// Тест 3: кол-во клиентов с повторным обращением за последний месяц
    /// </summary>
    [Fact]
    public void GetRepeatClientsLastMonth()
    {
        var monthAgo = new DateTime(2026, 10, 12, 23, 5, 10, DateTimeKind.Utc).AddDays(-30);

        var expected = new[] { "Иванов Иван Иванович" };

        var actual = fixture.Orders
            .Where(o => o.AcceptedAt >= monthAgo)
            .GroupBy(o => o.ClientId)
            .Where(g => g.Count() > 1)
            .Select(g => new { Client = fixture.Clients.First(c => c.Id == g.Key), Count = g.Count() })
            .OrderBy(x => x.Client.FullName)
            .ToList();

        foreach (var g in actual)
            output.WriteLine($"{g.Client.FullName}: {g.Count} обращения");

        Assert.Equal(expected, actual.Select(x => x.Client.FullName).ToList());
    }

    /// <summary>
    /// Тест 4: суммарная стоимость работ для выбранного заказа
    /// </summary>
    [Fact]
    public void GetOrderTotalCost()
    {
        var expectedTotal = 9700m;

        var orderId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var order = fixture.Orders.Single(o => o.Id == orderId);

        var actualTotal = order.Works.Sum(w => w.Price);
        var car = fixture.Cars.First(c => c.Id == order.CarId);

        output.WriteLine($"Авто: {car.Brand} {car.Model} ({car.LicensePlate})");
        foreach (var w in order.Works)
            output.WriteLine($"  {w.Name}: {w.Price} р.");
        output.WriteLine($"Итого: {actualTotal} р.");

        Assert.Equal(expectedTotal, actualTotal);
    }

    /// <summary>
    /// Тест 5: топ 5 наиболее частых видов работ
    /// </summary>
    [Fact]
    public void GetTop5Works()
    {
        var expected = new[]
        {
            "Диагностика ходовой",
            "Замена АКБ",
            "Замена тормозных дисков",
            "Компьютерная диагностика",
            "Правка вмятины крыла"
        };

        var actual = fixture.Orders
            .SelectMany(o => o.Works)
            .GroupBy(w => w.Name)
            .Select(g => new { Name = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Name)
            .Take(5)
            .ToList();

        foreach (var t in actual)
            output.WriteLine($"{t.Name}: {t.Count}");

        Assert.Equal(expected, actual.Select(x => x.Name).ToList());
    }
}