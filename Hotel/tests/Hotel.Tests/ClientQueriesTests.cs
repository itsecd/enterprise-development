using Hotel.Domain.Data;
using Hotel.Domain.Enums;

namespace Hotel.Tests;

/// <summary>
/// Тесты аналитических запросов по клиентам
/// </summary>
public class ClientQueriesTests
{
    /// <summary>
    /// Проверяет вывод клиентов, проживавших в номерах указанного типа,
    /// с сортировкой по ФИО
    /// </summary>
    [Fact]
    public void ClientsByRoomType_OrderedByFullName()
    {
        var data = new TestData();
        var targetCategory = RoomCategory.Standard;

        var result = (from b in data.Bookings
                      where b.Room.Type.Category == targetCategory
                      select b.Client)
                     .GroupBy(c => c.PassportNumber)
                     .Select(g => g.First())
                     .OrderBy(c => c.FullName)
                     .ToList();

        Assert.NotEmpty(result);

        var expectedOrder = result.OrderBy(c => c.FullName).Select(c => c.PassportNumber).ToList();
        Assert.Equal(expectedOrder, result.Select(c => c.PassportNumber).ToList());

        Assert.Contains(result, c => c.FullName == "Борисов Борис Борисович");
        Assert.Contains(result, c => c.FullName == "Волкова Виктория Владимировна");
    }

    /// <summary>
    /// Проверяет топ 5 клиентов по суммарной стоимости проживания
    /// </summary>
    [Fact]
    public void Top5ClientsByTotalSpent()
    {
        var data = new TestData();
        var expected = new (string PassportNumber, decimal TotalSpent)[]
        {
            ("4519 012345", 60_000m), // Козлов: 10 суток * 6000
            ("4515 678901", 31_500m), // Егоров: 7 суток * 4500
            ("4512 345678", 12_400m), // Волкова: 4800 + 7600
            ("4513 456789", 9_800m),  // Григорьев: 6600 + 3200
            ("4516 789012", 9_000m)   // Жукова: 2 суток * 4500
        };

        var result = data.Bookings
            .GroupBy(b => b.Client.PassportNumber)
            .Select(g => new
            {
                PassportNumber = g.Key,
                ClientName = g.First().Client.FullName,
                TotalSpent = g.Sum(b => b.Nights * b.Room.Type.PricePerNight)
            })
            .OrderByDescending(x => x.TotalSpent)
            .ThenBy(x => x.ClientName, StringComparer.Ordinal)
            .Take(5)
            .ToList();

        Assert.Equal(expected, result.Select(x => (x.PassportNumber, x.TotalSpent)));
    }
}