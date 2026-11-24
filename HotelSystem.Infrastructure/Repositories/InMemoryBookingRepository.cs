using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.Storage;

namespace HotelSystem.Infrastructure.Repositories;

/// <summary>
/// Репозиторий бронирований гостиницы с хранением данных в памяти
/// </summary>
public class InMemoryBookingRepository(HotelInMemoryStorage storage)
    : IRepository<Booking>
{
    /// <inheritdoc />
    public Task<List<Booking>> GetAllAsync() =>
        Task.FromResult(storage.Bookings);

    /// <inheritdoc />
    public Task<Booking?> GetByIdAsync(int id) =>
        Task.FromResult(
            storage.Bookings.FirstOrDefault(booking => booking.Id == id));

    /// <inheritdoc />
    public Task<Booking> AddAsync(Booking booking)
    {
        booking.Id = storage.Bookings.Count == 0
            ? 1
            : storage.Bookings.Max(existing => existing.Id) + 1;

        storage.Bookings.Add(booking);

        return Task.FromResult(booking);
    }

    /// <inheritdoc />
    public Task<bool> UpdateAsync(int id, Booking booking)
    {
        var index = storage.Bookings.FindIndex(
            existing => existing.Id == id);

        if (index == -1)
        {
            return Task.FromResult(false);
        }

        booking.Id = id;
        storage.Bookings[index] = booking;

        return Task.FromResult(true);
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int id)
    {
        var booking = storage.Bookings.FirstOrDefault(
            existing => existing.Id == id);

        return Task.FromResult(
            booking is not null && storage.Bookings.Remove(booking));
    }
}