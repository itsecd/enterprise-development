using Hotel.Application.Services;
using Hotel.Contracts.Dto;
using Hotel.Contracts.Exceptions;
using Hotel.Domain.Data;
using Hotel.Domain.Enums;
using Hotel.Infrastructure.InMemory;
using Hotel.Infrastructure.InMemory.Repositories;
using Xunit;

namespace Hotel.Tests;

/// <summary>
/// Проверяет аналитические запросы через сервисы серверного приложения
/// </summary>
public class ServiceAnalyticsTests
{
    /// <summary>
    /// Проверяет вывод клиентов по категории номера с сортировкой по ФИО
    /// </summary>
    [Fact]
    public async Task ClientsByRoomCategory_AreOrderedByFullName()
    {
        var (service, _) = CreateServices();

        var result = await service.GetByRoomCategoryAsync(RoomCategory.Standard);

        Assert.NotEmpty(result);
        Assert.Equal(result.OrderBy(client => client.FullName, StringComparer.Ordinal)
            .Select(client => client.Id), result.Select(client => client.Id));
        Assert.Contains(result, client => client.FullName == "Борисов Борис Борисович");
        Assert.Contains(result, client => client.FullName == "Волкова Виктория Владимировна");
    }

    /// <summary>
    /// Проверяет топ-5 клиентов по суммарной стоимости проживания
    /// </summary>
    [Fact]
    public async Task TopFiveClientsByTotalSpent_ReturnsExpectedAmounts()
    {
        var (service, _) = CreateServices();

        var result = await service.GetTopSpendersAsync();

        var expected = new decimal[] { 60_000m, 31_500m, 12_400m, 9_800m, 9_000m };
        Assert.Equal(expected, result.Select(item => item.TotalSpent));
        Assert.Equal("Козлов Константин Кириллович", result[0].Client.FullName);
    }

    /// <summary>
    /// Проверяет номера, занятые на указанную дату
    /// </summary>
    [Fact]
    public async Task CurrentBookings_OnMarchTenth_ReturnsExpectedRooms()
    {
        var (_, service) = CreateServices();

        var result = await service.GetCurrentBookingsAsync(new DateTime(2025, 3, 10));

        Assert.Equal(new[] { "101", "302" }, result.Select(room => room.RoomNumber));
    }

    /// <summary>
    /// Проверяет топ-5 наиболее часто бронируемых номеров
    /// </summary>
    [Fact]
    public async Task MostBookedRooms_ReturnsFiveRoomsInCorrectOrder()
    {
        var (_, service) = CreateServices();

        var result = await service.GetMostBookedAsync();

        Assert.Equal(5, result.Count);
        Assert.Equal(new[] { "101", "102", "201", "202", "301" },
            result.Select(room => room.RoomNumber));
        Assert.Equal(new[] { 3, 2, 2, 1, 1 }, result.Select(room => room.BookingCount));
    }

    /// <summary>
    /// Проверяет число бронирований всех номеров, включая номера без бронирований
    /// </summary>
    [Fact]
    public async Task BookingCounts_ReturnsAllRooms()
    {
        var (_, service) = CreateServices();

        var result = await service.GetBookingCountsAsync();

        Assert.Equal(10, result.Count);
        Assert.Equal(3, result.Single(room => room.RoomNumber == "101").BookingCount);
        Assert.Equal(2, result.Single(room => room.RoomNumber == "102").BookingCount);
        Assert.Equal(0, result.Single(room => room.RoomNumber == "502").BookingCount);
    }

    /// <summary>
    /// Проверяет запрет на пересекающиеся бронирования одного номера
    /// </summary>
    [Fact]
    public async Task AddBooking_WhenRoomIsAlreadyBooked_ThrowsConflict()
    {
        var data = new TestData();
        var bookingRepository = new BookingRepository(data);
        using var relationshipLock = new RelationshipLock();
        var service = new BookingService(
            bookingRepository,
            new ClientRepository(data),
            new RoomRepository(data),
            relationshipLock);

        var request = new BookingEditDto
        {
            ClientId = 2,
            RoomId = 1,
            CheckInDate = new DateTime(2025, 3, 10),
            Nights = 2
        };

        await Assert.ThrowsAsync<ResourceConflictException>(() => service.AddAsync(request));
    }

    private static (ClientService ClientService, RoomService RoomService) CreateServices()
    {
        var data = new TestData();
        var bookingRepository = new BookingRepository(data);
        var roomRepository = new RoomRepository(data);
        var roomTypeRepository = new RoomTypeRepository(data);
        var clientRepository = new ClientRepository(data);
        var relationshipLock = new RelationshipLock();

        var clientService = new ClientService(clientRepository, bookingRepository, relationshipLock);
        var roomService = new RoomService(
            roomRepository,
            roomTypeRepository,
            bookingRepository,
            relationshipLock);

        return (clientService, roomService);
    }
}
