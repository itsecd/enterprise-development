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
    public List<Booking> GetAll() => storage.Bookings;

    /// <inheritdoc />
    public Booking? GetById(int id) =>
        storage.Bookings.FirstOrDefault(booking => booking.Id == id);

    /// <inheritdoc />
    public Booking Add(Booking booking)
    {
        booking.Id = storage.Bookings.Count == 0
            ? 1
            : storage.Bookings.Max(existing => existing.Id) + 1;

        storage.Bookings.Add(booking);

        return booking;
    }

    /// <inheritdoc />
    public bool Update(int id, Booking booking)
    {
        var index = storage.Bookings.FindIndex(existing => existing.Id == id);

        if (index == -1)
        {
            return false;
        }

        booking.Id = id;
        storage.Bookings[index] = booking;

        return true;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var booking = GetById(id);

        return booking is not null && storage.Bookings.Remove(booking);
    }
}