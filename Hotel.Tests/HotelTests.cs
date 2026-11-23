using Hotel.Domain.Data;
using Xunit;

namespace Hotel.Tests;

/// <summary>
/// Юнит-тесты по доменной области
/// </summary>
public class HotelTests
{
    /// <summary>
    /// 1. Информация о клиентах, проживавших в номерах определённого типа
    /// </summary>
    [Fact]
    public void GetClientsByRoomType()
    {
        List<int> expectedClientIds = [6, 3, 8];
        var roomType = TestData.RoomTypes[5];

        var result = TestData.Bookings
            .Where(booking => booking.Room.Type == roomType)
            .Select(booking => booking.Client)
            .OrderBy(client => client.Surname)
            .Select(client => client.Id)
            .ToList();

        Assert.Equal(result, expectedClientIds);
    }

    /// <summary>
    /// 2. Информация о забронированных номерах
    /// </summary>
    [Fact]
    public void GetBookedRooms()
    {
        List<int> expectedRoomIds = [7, 4];
        var date = new DateOnly(2026, 1, 25);

        var result = TestData.Bookings
            .Where(booking =>
                (booking.CheckInDate <= date) &&
                (booking.CheckInDate.AddDays((int)booking.DaysOfStay) >= date))
            .Select(booking => booking.Room.Id)
            .ToList();

        Assert.Equal(result, expectedRoomIds);
    }

    /// <summary>
    /// 3. Информация о 5 наиболее часто снимаемых комнатах
    /// </summary>
    [Fact]
    public void GetTop5MostPopularRooms()
    {
        List<int> expectedRoomIds = [7, 6, 1, 4, 3];

        var result = TestData.Bookings
            .GroupBy(booking => booking.Room)
            .OrderByDescending(rooms => rooms.Count())
            .Take(5)
            .Select(rooms => rooms.Key.Id)
            .ToList();

        Assert.Equal(result, expectedRoomIds);
    }

    /// <summary>
    /// 4. Информация о колличестве бронирований у каждой комнаты
    /// </summary>
    [Fact]
    public void GetBookingCountOfEveryRoom()
    {
        List<int> expectedBookingCount = [2, 2, 2, 1, 1, 3, 3, 1];

        var result = TestData.Bookings
            .GroupBy(booking => booking.Room.Number)
            .OrderBy(group => group.Key)
            .Select(group => group.Count())
            .ToList();

        Assert.Equal(result, expectedBookingCount);
    }

    /// <summary>
    /// 5. Топ 5 клиентов по суммарной стоимости проживания
    /// </summary>
    [Fact]
    public void GetTop5MostRichClients()
    {
        List<int> expectedClientIds = [5, 1, 8, 6, 3];

        var result = TestData.Bookings
            .GroupBy(booking => booking.Client)
            .OrderByDescending(group => group.Sum(booking => booking.TotalPrice))
            .Select(group => group.Key.Id)
            .Take(5)
            .ToList();

        Assert.Equal(result, expectedClientIds);
    }
}