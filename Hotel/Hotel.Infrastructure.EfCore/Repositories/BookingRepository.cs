using Hotel.Domain;
using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий бронирований
/// </summary>
public class BookingRepository(HotelDbContext context) : IRepository<Booking, int>
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Booking>> GetAll()
    {
        return await context.Bookings
            .AsNoTracking()
            .Include(booking => booking.Client)
            .Include(booking => booking.Room)
            .ThenInclude(room => room.RoomType)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public Task<Booking?> GetById(int id)
    {
        return context.Bookings
            .Include(booking => booking.Client)
            .Include(booking => booking.Room)
            .ThenInclude(room => room.RoomType)
            .FirstOrDefaultAsync(booking => booking.Id == id);
    }

    /// <inheritdoc/>
    public async Task<Booking> Add(Booking booking)
    {
        var clientId = booking.Client.Id;
        var roomId = booking.Room.Id;

        var client = await context.HotelClients.FindAsync(clientId)
            ?? throw new ArgumentException($"Не удалось найти клиента с идентификатором {clientId}");

        var room = await context.Rooms
            .Include(room => room.RoomType)
            .FirstOrDefaultAsync(room => room.Id == roomId)
            ?? throw new ArgumentException($"Не удалось найти номер с идентификатором {roomId}");

        booking.ClientId = client.Id;
        booking.Client = client;
        booking.RoomId = room.Id;
        booking.Room = room;

        context.Bookings.Add(booking);
        await context.SaveChangesAsync();

        return booking;

    }

    /// <inheritdoc/>
    public async Task<Booking?> Update(Booking booking)
    {
        var existingBooking = await GetById(booking.Id);

        if (existingBooking is null) return null;

        var clientId = booking.Client.Id;
        var roomId = booking.Room.Id;

        var client = await context.HotelClients.FindAsync(clientId)
            ?? throw new ArgumentException($"Не удалось найти клиента с идентификатором {clientId}");

        var room = await context.Rooms
            .Include(room => room.RoomType)
            .FirstOrDefaultAsync(room => room.Id == roomId)
            ?? throw new ArgumentException($"Не удалось найти номер с идентификатором {roomId}");

        existingBooking.ClientId = client.Id;
        existingBooking.Client = client;
        existingBooking.RoomId = room.Id;
        existingBooking.Room = room;
        existingBooking.CheckInDate = booking.CheckInDate;
        existingBooking.DaysCount = booking.DaysCount;

        await context.SaveChangesAsync();

        return existingBooking;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        var booking = await context.Bookings.FindAsync(id);

        if (booking is null) return false;

        context.Bookings.Remove(booking);
        await context.SaveChangesAsync();

        return true;
    }
}
