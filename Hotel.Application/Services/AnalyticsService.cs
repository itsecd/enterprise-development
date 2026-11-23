using Hotel.Application.Contracts;
using Hotel.Application.Contracts.Booking;
using Hotel.Application.Mappers;
using Hotel.Domain.Shared.Enums;

namespace Hotel.Application.Services;

/// <summary>
/// Реализация службы CRUD операций для бронирований
/// </summary>
/// <param name="_bookings">Репозиторий брони</param>
/// <param name="_rooms">Репозиторий номеров</param>
public class AnalyticsService(IBookingRepository _bookings, IRoomRepository _rooms) : IAnalyticsService
{
    /// <inheritdoc/>
    public async Task<ClientsByRoomTypeDto>
        GetClientsByRoomTypeAsync(RoomCategory category, CancellationToken ct = default)
    {
        var bookings = await _bookings.ReadAll(ct);

        var clients = bookings
            .Where(b => b.Room.Type.Category == category)
            .Select(b => b.Client)
            .GroupBy(c => c.Id)
            .Select(g => g.First())
            .OrderBy(c => c.Surname)
            .ThenBy(c => c.Name)
            .ThenBy(c => c.Patronymic)
            .Select(c => ClientMapper.ToDto(c))
            .ToList();

        return new ClientsByRoomTypeDto(category, clients);
    }
    
    /// <inheritdoc/>
    public async Task<IEnumerable<BookingResponseDto>>
        GetCurrentBookingsAsync(CancellationToken ct = default)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var bookings = await _bookings.ReadAll(ct);

        return bookings
            .Where(b => b.CheckInDate <= today && b.CheckInDate.AddDays((int)b.DaysOfStay) > today)
            .Select(b => BookingMapper.ToDto(b))
            .ToList();
    }
    
    /// <inheritdoc/>
    public async Task<IEnumerable<RoomBookingCountDto>>
        GetTop5FrequentlyBookedRoomsAsync(CancellationToken ct = default)
    {
        var bookings = await _bookings.ReadAll(ct);

        return bookings
            .GroupBy(b => b.Room)
            .Select(g => new RoomBookingCountDto(g.Key.Id, g.Key.Number, g.Count()))
            .OrderByDescending(r => r.BookingCount)
            .Take(5)
            .ToList();
    }
    
    /// <inheritdoc/>
    public async Task<IEnumerable<RoomBookingCountDto>>
        GetBookingCountForEachRoomAsync(CancellationToken ct = default)
    {
        var rooms = await _rooms.ReadAll(ct);
        var bookings = await _bookings.ReadAll(ct);

        return rooms.Select(r => new RoomBookingCountDto(
                r.Id,
                r.Number,
                bookings.Count(b => b.Room.Id == r.Id)))
            .ToList();
    }
    
    /// <inheritdoc/>
    public async Task<IEnumerable<ClientTotalSpentDto>>
        GetTop5ClientsByTotalSpentAsync(CancellationToken ct = default)
    {
        var bookings = await _bookings.ReadAll(ct);

        return bookings
            .GroupBy(b => b.Client)
            .Select(g => new ClientTotalSpentDto(ClientMapper.ToDto(g.Key), g.Sum(b => b.TotalPrice)))
            .OrderByDescending(c => c.TotalSpent)
            .Take(5)
            .ToList();
    }
}