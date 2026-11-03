using HotelSystem.Domain.Shared.Enums;
using HotelSystem.Domain.Data;

namespace HotelSystem.Tests;

public class HotelTests
{
    /// <summary>
    /// 1. Вывести информацию о всех клиентах,проживавших в номерах указанного типа, упорядочить по ФИО
    /// </summary>

    [Fact]
    public void GetClientsByRoomType()
    {
        var roomCategory = RoomCategory.Standard;

        var result = HotelTestData.Bookings
            .Where(booking => booking.Room.RoomType.Category == roomCategory)
            .Select(booking => booking.Client)
            .DistinctBy(client => client.Id)
            .OrderBy(client => client.LastName)
            .ThenBy(client => client.FirstName)
            .ThenBy(client => client.Patronymic)
            .ToList();

        Assert.Equal(4, result.Count);

        Assert.Contains(
            result,
            client =>
                client.LastName == "Белов" &&
                client.FirstName == "Борис" &&
                client.Patronymic == "Сергеевич");
    }

    /// <summary>
    /// 2. Вывести информацию о номерах, находящихся в текущем бронировании
    /// </summary>
    [Fact]
    public void GetCurrentlyBookedRooms()
    {
        var currentDate = new DateTime(2026, 9, 10);

        var expectedRooms = new[]
        {
            103, 203, 401
        };

        var result = HotelTestData.Bookings
            .Where(booking =>
                booking.CheckInDate <= currentDate &&
                booking.CheckInDate.AddDays(booking.NumberOfDays) > currentDate)
            .Select(booking => booking.Room)
            .DistinctBy(room => room.Id)
            .OrderBy(room => room.RoomNumber)
            .ToList();

        Assert.Equal(
            expectedRooms,
            result.Select(room => room.RoomNumber).ToArray());
    }

    /// <summary>
    /// 3. Вывести топ 5 наиболее часто бронируемых номеров
    /// </summary>
    [Fact]
    public void GetTop5MostFrequentlyBookedRooms()
    {
        var expected = new[]
        {
            (RoomNumber: 203, BookingCount: 3),
            (RoomNumber: 102, BookingCount: 2),
            (RoomNumber: 202, BookingCount: 2),
            (RoomNumber: 101, BookingCount: 1),
            (RoomNumber: 103, BookingCount: 1)
        };

        var result = HotelTestData.Bookings
            .GroupBy(booking => booking.Room)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key.RoomNumber)
            .Take(5)
            .Select(group => (
                RoomNumber: group.Key.RoomNumber,
                BookingCount: group.Count()))
            .ToArray();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// 4. Для каждого номера вывести число бронирований
    /// <summary>
    [Theory]
    [InlineData(101, 1)]
    [InlineData(102, 2)]
    [InlineData(103, 1)]
    [InlineData(201, 1)]
    [InlineData(202, 2)]
    [InlineData(203, 3)]
    [InlineData(301, 1)]
    [InlineData(302, 1)]
    [InlineData(303, 1)]
    [InlineData(401, 1)]
    public void GetBookingCountForEachRoom(
        int roomNumber,
        int expectedBookingCount)
    {
        var bookingCounts = HotelTestData.Bookings
            .GroupBy(booking => booking.Room.RoomNumber)
            .ToDictionary(
                group => group.Key,
                group => group.Count());

        var actualBookingCount = bookingCounts.GetValueOrDefault(roomNumber);

        Assert.Equal(expectedBookingCount, actualBookingCount);
    }

    /// <summary>
    /// 5.Вывести топ 5 клиентов по суммарной стоимости проживания
    /// </summary>
    [Fact]
    public void GetTop5ClientsByTotalCost()
    {
        var expected = new (string LastName, string FirstName, string? Patronymic, decimal TotalCost)[]
        {
            ("Васильева", "Виктория", "Андреевна", 94000m),
            ("Захаров", "Захар", "Денисович", 80000m),
            ("Громов", "Георгий", "Иванович", 75200m),
            ("Ван Дам", "Ирина", "Павловна", 40000m),
            ("Белов", "Борис", "Сергеевич", 33500m)
        };

        var result = HotelTestData.Bookings
            .GroupBy(booking => booking.Client)
            .Select(group => new
            {
                Client = group.Key,
                TotalCost = group.Sum(booking => booking.TotalCost)
            })
            .OrderByDescending(x => x.TotalCost)
            .Take(5)
            .Select(x => (
                LastName: x.Client.LastName,
                FirstName: x.Client.FirstName,
                Patronymic: x.Client.Patronymic,
                TotalCost: x.TotalCost))
            .ToArray();

        Assert.Equal(expected, result);
    }
}