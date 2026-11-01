using AutoServiceLab.Domain.Enums;
using AutoServiceLab.Tests.Fixtures;

namespace AutoServiceLab.Tests;

/// <summary>
/// Набор тестов для проверки LINQ-запросов автосервиса
/// </summary>
public class QueriesTest(AutoServiceFixture fixture) : IClassFixture<AutoServiceFixture>
{
    /// <summary>
    /// Тест 1: Вывести информацию о всех механиках, специализирующихся на выбранном виде работ
    /// </summary>
    [Fact]
    public void GetMechanicsByWorkType_ReturnsOnlyMatching()
    {
        var expectedMechanicIds = new[] { 1, 5 };

        var result = fixture.Mechanics
            .Where(m => m.Specialization == MechanicSpecialization.Engine)
            .Select(m => m.Id)
            .OrderBy(id => id)
            .ToList();

        Assert.Equal(expectedMechanicIds, result);
    }

    /// <summary>
    /// Тест 2: Вывести информацию обо всех клиентах, чьи автомобили обслуживались у указанного механика, упорядочить по ФИО
    /// </summary>
    [Fact]
    public void GetClientsByMechanic_ReturnsSortedByName()
    {
        var targetMechanicId = 1;
        var expectedNames = new[]
        {
            "Волкова Анна Владимировна",
            "Иванов Иван Иванович",
            "Соколова Наталья Игоревна"
        };

        var result = fixture.OrderMechanics
            .Where(om => om.MechanicId == targetMechanicId)
            .Select(om => om.ServiceOrder.Client.FullName)
            .Distinct()
            .OrderBy(n => n)
            .ToList();

        Assert.Equal(expectedNames, result);
    }

    /// <summary>
    /// Тест 3: Вывести информацию о количестве повторных обращений клиентов за последний месяц
    /// </summary>
    [Fact]
    public void GetRepeatClientsCount_ReturnsCorrectCount()
    {
        var monthAgo = new DateTime(2026, 9, 1);
        var expectedCount = 2;

        var repeatClientsCount = fixture.ServiceOrders
            .Where(o => o.ReceptionDate >= monthAgo)
            .GroupBy(o => o.ClientId)
            .Count(g => g.Count() > 1);

        Assert.Equal(expectedCount, repeatClientsCount);
    }

    /// <summary>
    /// Тест 4: Для выбранного заказа подсчитать суммарную стоимость работ
    /// </summary>
    [Fact]
    public void GetOrderTotalCost_ReturnsCorrectSum()
    {
        var targetOrderId = 1;
        var expectedTotalCost = 6250m;

        var totalCost = fixture.OrderWorks
            .Where(ow => ow.ServiceOrderId == targetOrderId)
            .Sum(ow => ow.WorkType.LaborCost);

        Assert.Equal(expectedTotalCost, totalCost);
    }

    /// <summary>
    /// Тест 5: Вывести топ 5 наиболее частых видов работ
    /// </summary>
    [Fact]
    public void GetTopFiveWorkTypes_ReturnsTopFive()
    {
        var expected = new[]
        {
            "Замена масла",
            "Ремонт двигателя",
            "Диагностика двигателя",
            "Замена амортизаторов",
            "Замена ремня ГРМ"
        };

        var result = fixture.OrderWorks
            .GroupBy(ow => ow.WorkType.Name)
            .Select(g => new { Name = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Name)
            .Take(5)
            .Select(x => x.Name)
            .ToList();

        Assert.Equal(expected, result);
    }
}
