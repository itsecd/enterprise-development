using Hotel.Domain.Data;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Models;

namespace Hotel.Infrastructure.InMemory.Repositories;

/// <summary>Репозиторий бронирований с хранением данных в памяти.</summary>
/// <param name="data">Общий набор данных гостиницы.</param>
public class BookingRepository(TestData data) : IBookingRepository
{
    private readonly TestData _data = data;

    public Task<IReadOnlyList<Booking>> GetAllAsync()
    {
        lock (_data.Bookings)
            return Task.FromResult<IReadOnlyList<Booking>>(_data.Bookings.ToList());
    }

    public Task<Booking?> GetByIdAsync(int id)
    {
        lock (_data.Bookings)
            return Task.FromResult(_data.Bookings.FirstOrDefault(booking => booking.Id == id));
    }

    public Task<Booking> AddAsync(Booking booking)
    {
        lock (_data.Bookings)
        {
            booking.Id = _data.Bookings.Select(item => item.Id).DefaultIfEmpty(0).Max() + 1;
            _data.Bookings.Add(booking);
        }

        return Task.FromResult(booking);
    }

    public Task<bool> UpdateAsync(Booking booking)
    {
        lock (_data.Bookings)
        {
            var existing = _data.Bookings.FirstOrDefault(item => item.Id == booking.Id);
            if (existing is null)
                return Task.FromResult(false);

            existing.Client = booking.Client;
            existing.Room = booking.Room;
            existing.CheckInDate = booking.CheckInDate;
            existing.Nights = booking.Nights;
            return Task.FromResult(true);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_data.Bookings)
        {
            var booking = _data.Bookings.FirstOrDefault(item => item.Id == id);
            return Task.FromResult(booking is not null && _data.Bookings.Remove(booking));
        }
    }

    public Task<bool> HasBookingsForClientAsync(int clientId)
    {
        lock (_data.Bookings)
            return Task.FromResult(_data.Bookings.Any(booking => booking.Client.Id == clientId));
    }

    public Task<bool> HasBookingsForRoomAsync(int roomId)
    {
        lock (_data.Bookings)
            return Task.FromResult(_data.Bookings.Any(booking => booking.Room.Id == roomId));
    }
}
