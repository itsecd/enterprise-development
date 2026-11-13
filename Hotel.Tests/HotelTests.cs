using Xunit;

namespace Hotel.Tests;

/// <summary>
/// Юнит-тесты по заданным сущностям
/// </summary>
public class HotelTests(HotelFixture fixture) : IClassFixture<HotelFixture>
{
    /// <summary>
    /// Информация о клиентах, проживающих в номерах указанного типа
    /// </summary>
    [Fact]
    public void FindClientByTypeRoom()
    {
        // Id указанного типа комнаты
        var selectedTypeRoomId = 5;

        // Ожидаемый результат
        var checkResult = new[]
        {
            new ClientFullName("Yoon", "Suk-Yeol", null),
            new ClientFullName("Киркоров", "Филипп", "Бедросович")
        };

        var result = fixture.Bookings
            .Where(booking => booking.Room.RoomType.Id == selectedTypeRoomId)
            .Select(booking => booking.Client)
            .DistinctBy(client => client.Id)
            .OrderBy(client => client.LastName, StringComparer.InvariantCulture)
            .ThenBy(client => client.FirstName, StringComparer.InvariantCulture)
            .ThenBy(client => client.Patronymic, StringComparer.InvariantCulture)
            .Select(client => new ClientFullName(client.LastName, client.FirstName, client.Patronymic))
            .ToList();

        Assert.Equal(checkResult, result);
    }

    /// <summary>
    /// Информация о забронированных номерах
    /// </summary>
    [Fact]
    public void RoomBooked()
    {
        // Заданное время для теста
        var currentDay = new DateTime(2019, 04, 13);

        // Ожидаемый результат
        int[] checkResult = [4, 6];

        var numberRooms = fixture.Bookings
            .Where(booking => booking.DateArrival <= currentDay && booking.DateArrival.AddDays(booking.DayCount) >= currentDay)
            .Select(booking => booking.Room.Number)
            .Distinct()
            .OrderBy(number => number)
            .ToList();

        Assert.Equal(checkResult, numberRooms);
    }

    /// <summary>
    /// Топ 5 наиболее часто бронируемых номеров
    /// </summary>
    [Fact]
    public void Top5MostFrequentlyBookedRooms()
    {
        // Ожидаемый результат
        int[] checkResultNumber = [18, 3, 6, 7, 1];

        var numberRooms = fixture.Bookings
            .GroupBy(booking => booking.Room.Number)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key)
            .Take(5)
            .Select(group => group.Key)
            .ToList();

        Assert.Equal(checkResultNumber, numberRooms);
    }

    /// <summary>
    /// Число бронирований для каждого номера
    /// </summary>
    [Fact]
    public void CountBookingsForEachRoom()
    {
        // Ожидаемый результат
        int[] checkResult = [1, 2, 1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 3];

        var result = fixture.Bookings
            .GroupBy(booking => booking.Room.Number)
            .OrderBy(group => group.Key)
            .Select(group => group.Count())
            .ToList();

        Assert.Equal(checkResult, result);
    }

    /// <summary>
    /// Топ 5 клиентов по суммарной стоимости проживания
    /// </summary>
    [Fact]
    public void Top5ClientsByTotalStay()
    {
        // Ожидаемый результат
        int[] checkResultId = [10, 2, 9, 4, 1];

        var resultId = fixture.Bookings
            .GroupBy(booking => booking.Client.Id)
            .OrderByDescending(group => group.Sum(booking => booking.DayCount * booking.Room.RoomType.Price))
            .ThenBy(group => group.Key)
            .Take(5)
            .Select(group => group.Key)
            .ToList();

        Assert.Equal(checkResultId, resultId);
    }
}
