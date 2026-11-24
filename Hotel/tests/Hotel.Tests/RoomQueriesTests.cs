using Hotel.Domain.Data;

namespace Hotel.Tests;

/// <summary>
/// Тесты аналитических запросов по номерам гостиницы
/// </summary>
public class RoomQueriesTests
{
    /// <summary>
    /// Проверяет вывод номеров, находящихся в текущем бронировании на заданную дату
    /// </summary>
    [Fact]
    public void RoomsInCurrentBookings_OnSpecificDate()
    {
        var data = new TestData();
        var currentDate = new DateTime(2025, 3, 10);
        string[] expectedRoomNumbers = ["101", "302"];

        var result = data.Bookings
            .Where(b => b.CheckInDate <= currentDate && b.CheckInDate.AddDays(b.Nights) > currentDate)
            .Select(b => b.Room.RoomNumber)
            .Distinct()
            .OrderBy(rn => rn)
            .ToList();

        Assert.Equal(expectedRoomNumbers, result);
    }

    /// <summary>
    /// Проверяет топ 5 наиболее часто бронируемых номеров
    /// </summary>
    [Fact]
    public void Top5MostBookedRooms()
    {
        var data = new TestData();

        var result = data.Bookings
            .GroupBy(b => b.Room.RoomNumber)
            .Select(g => new { RoomNumber = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.RoomNumber)
            .Take(5)
            .ToList();

        Assert.Equal(5, result.Count);
        Assert.Contains(result, x => x.RoomNumber == "101");
        Assert.Contains(result, x => x.RoomNumber == "102");
        Assert.Contains(result, x => x.RoomNumber == "201");

        for (var i = 1; i < result.Count; i++)
        {
            Assert.True(result[i - 1].Count >= result[i].Count);
        }
    }

    /// <summary>
    /// Проверяет число бронирований для каждого номера
    /// </summary>
    [Fact]
    public void BookingCountPerRoom()
    {
        var data = new TestData();

        var result = from room in data.Rooms
                     join booking in data.Bookings on room.RoomNumber equals booking.Room.RoomNumber into roomBookings
                     select new
                     {
                         room.RoomNumber,
                         Count = roomBookings.Count()
                     };

        var listResult = result.ToList();

        Assert.Equal(10, listResult.Count);
        Assert.Equal(3, listResult.First(x => x.RoomNumber == "101").Count);
        Assert.Equal(2, listResult.First(x => x.RoomNumber == "102").Count);
        Assert.Equal(0, listResult.First(x => x.RoomNumber == "502").Count);
    }
}