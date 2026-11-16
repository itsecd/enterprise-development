using Hotel.Domain;
using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


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
    public Task<bool> AnyAsync(Expression<Func<Booking, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return context.Bookings.AnyAsync(predicate, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<TResult>> QueryAsync<TResult>(
        Func<IQueryable<Booking>, IQueryable<TResult>> query,
        CancellationToken cancellationToken = default)
    {
        return await query(context.Bookings.AsNoTracking())
            .ToListAsync(cancellationToken);
    }
}
