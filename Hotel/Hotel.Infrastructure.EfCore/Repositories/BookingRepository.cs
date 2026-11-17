using Hotel.Domain;
using Hotel.Domain.Entities;
using Hotel.Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий бронирований
/// </summary>
public class BookingRepository(HotelDbContext context) : IBookingRepository
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Booking>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.Bookings
            .AsNoTracking()
            .Include(booking => booking.Client)
            .Include(booking => booking.Room)
            .ThenInclude(room => room.RoomType)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public Task<Booking?> GetById(int id, CancellationToken cancellationToken = default)
    {
        return context.Bookings
            .Include(booking => booking.Client)
            .Include(booking => booking.Room)
            .ThenInclude(room => room.RoomType)
            .FirstOrDefaultAsync(booking => booking.Id == id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Booking> Add(Booking booking, CancellationToken cancellationToken = default)
    {
        var clientId = booking.Client.Id;
        var roomId = booking.Room.Id;

        var client = await context.HotelClients.FindAsync([clientId], cancellationToken)
            ?? throw new ArgumentException($"Не удалось найти клиента с идентификатором {clientId}");

        var room = await context.Rooms
            .Include(room => room.RoomType)
            .FirstOrDefaultAsync(room => room.Id == roomId, cancellationToken)
            ?? throw new ArgumentException($"Не удалось найти номер с идентификатором {roomId}");

        booking.Client = client;
        booking.Room = room;

        context.Bookings.Add(booking);
        await context.SaveChangesAsync(cancellationToken);

        return booking;

    }

    /// <inheritdoc/>
    public async Task<Booking?> Update(Booking booking, CancellationToken cancellationToken = default)
    {
        var existingBooking = await GetById(booking.Id, cancellationToken);

        if (existingBooking is null) return null;

        var clientId = booking.Client.Id;
        var roomId = booking.Room.Id;

        var client = await context.HotelClients.FindAsync([clientId], cancellationToken)
            ?? throw new ArgumentException($"Не удалось найти клиента с идентификатором {clientId}");

        var room = await context.Rooms
            .Include(room => room.RoomType)
            .FirstOrDefaultAsync(room => room.Id == roomId, cancellationToken)
            ?? throw new ArgumentException($"Не удалось найти номер с идентификатором {roomId}");

        existingBooking.Client = client;
        existingBooking.Room = room;
        existingBooking.CheckInDate = booking.CheckInDate;
        existingBooking.DaysCount = booking.DaysCount;

        await context.SaveChangesAsync(cancellationToken);

        return existingBooking;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        var booking = await context.Bookings.FindAsync([id], cancellationToken);

        if (booking is null) return false;

        context.Bookings.Remove(booking);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }

    /// <inheritdoc/>
    public Task<bool> HasOverlappingBookingAsync(int roomId, DateOnly checkInDate, DateOnly checkOutDate, int? excludedBookingId, CancellationToken cancellationToken = default)
    {
        return context.Bookings.AnyAsync(booking =>
            booking.RoomId == roomId &&
            booking.Id != excludedBookingId &&
            booking.CheckInDate < checkOutDate &&
            booking.CheckInDate.AddDays(booking.DaysCount) > checkInDate, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<HotelClient>> GetClientsByRoomCategoryAsync(RoomCategory category, CancellationToken cancellationToken = default)
    {
        return await context.Bookings
            .AsNoTracking()
            .Where(booking => booking.Room.RoomType.Category == category)
            .Select(booking => booking.Client)
            .Distinct()
            .OrderBy(client => client.LastName)
            .ThenBy(client => client.FirstName)
            .ThenBy(client => client.MiddleName)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<(HotelClient Client, decimal TotalStayCost)>> GetTopFiveClientsByTotalStayCostAsync(CancellationToken cancellationToken = default)
    {
        var topClients = await context.Bookings
            .AsNoTracking()
            .GroupBy(booking => booking.Client.Id)
            .Select(group => new
            {
                ClientId = group.Key,
                group.First().Client,
                TotalStayCost = group.Sum(booking =>
                    booking.Room.RoomType.PricePerDay * booking.DaysCount)
            })
            .OrderByDescending(item => item.TotalStayCost)
            .ThenBy(item => item.ClientId)
            .Take(5)
            .ToListAsync(cancellationToken);

        return topClients
            .Select(item => (item.Client, item.TotalStayCost))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyDictionary<int, int>> GetBookingCountsByRoomAsync(CancellationToken cancellationToken = default)
    {
        return await context.Bookings
            .GroupBy(booking => booking.RoomId)
            .Select(group => new { RoomId = group.Key, BookingCount = group.Count() })
            .ToDictionaryAsync(item => item.RoomId, item => item.BookingCount, cancellationToken);
    }
}
