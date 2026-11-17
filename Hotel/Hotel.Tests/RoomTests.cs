using Hotel.Domain.Data;

namespace Hotel.Tests;

/// <summary>
/// Тесты для операций с номерами гостиницы
/// </summary>
public class RoomTests
{
    /// <summary>
    /// Проверяет получение номеров, которые заняты на указанную дату
    /// </summary>
    [Fact]
    public void GetBookedRooms()
    {
        var expected = new List<int> { 3, 4, 6 };

        var currentDate = new DateOnly(2026, 8, 6);

        var result = HotelData.Bookings
            .Where(booking =>
                booking.CheckInDate <= currentDate &&
                currentDate < booking.CheckInDate.AddDays(booking.DaysCount))
            .Select(booking => booking.Room.Id)
            .Distinct()
            .OrderBy(roomId => roomId)
            .ToList();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет получение пяти номеров, которые бронировались чаще всего
    /// </summary>
    [Fact]
    public void GetTopFiveMostBookedRooms()
    {
        var expected = new List<int> { 7, 3, 10, 2, 4 };

        var result = HotelData.Bookings
            .GroupBy(booking => booking.Room)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key.Id)
            .Take(5)
            .Select(group => group.Key.Id)
            .ToList();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет количество бронирований для каждого номера
    /// </summary>
    [Theory]
    [InlineData(1, 1)]
    [InlineData(2, 2)]
    [InlineData(3, 4)]
    [InlineData(4, 2)]
    [InlineData(5, 2)]
    [InlineData(6, 2)]
    [InlineData(7, 5)]
    [InlineData(8, 1)]
    [InlineData(9, 0)]
    [InlineData(10, 3)]
    public void GetBookingCountForEachRoom(int roomId, int expected)
    {
        var result = HotelData.Bookings
            .Count(booking => booking.Room.Id == roomId);

        Assert.Equal(expected, result);
    }
}