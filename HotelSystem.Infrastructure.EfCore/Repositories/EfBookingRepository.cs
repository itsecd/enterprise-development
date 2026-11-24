using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий бронирований гостиницы с хранением данных в базе PostgreSQL
/// </summary>
public class EfBookingRepository(HotelDbContext context) : IRepository<Booking>
{
    /// <inheritdoc />
    public async Task<List<Booking>> GetAllAsync() =>
        await context.Bookings
            .Include(booking => booking.Client)
            .Include(booking => booking.Room)
                .ThenInclude(room => room.RoomType)
            .ToListAsync();

    /// <inheritdoc />
    public async Task<Booking?> GetByIdAsync(int id) =>
        await context.Bookings
            .Include(booking => booking.Client)
            .Include(booking => booking.Room)
                .ThenInclude(room => room.RoomType)
            .FirstOrDefaultAsync(booking => booking.Id == id);

    /// <inheritdoc />
    public async Task<Booking> AddAsync(Booking booking)
    {
        await context.Bookings.AddAsync(booking);
        await context.SaveChangesAsync();

        return booking;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, Booking booking)
    {
        var existingBooking = await GetByIdAsync(id);

        if (existingBooking is null)
        {
            return false;
        }

        existingBooking.Client = booking.Client;
        existingBooking.Room = booking.Room;
        existingBooking.CheckInDate = booking.CheckInDate;
        existingBooking.NumberOfDays = booking.NumberOfDays;

        await context.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var booking = await GetByIdAsync(id);

        if (booking is null)
        {
            return false;
        }

        context.Bookings.Remove(booking);
        await context.SaveChangesAsync();

        return true;
    }
}