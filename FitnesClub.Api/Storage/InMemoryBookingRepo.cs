using FitnesClub.Domain.Data;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Storage;

/// <summary>
/// Репозиторий для хранения и управления записями на тренировки в оперативной памяти.
/// </summary>
public class InMemoryBookingRepo : IRepository<Booking, Guid>
{
    private readonly List<Booking> _bookings;

    /// <summary>
    /// Инициализирует репозиторий начальным набором данных из <see cref="DataSeeder"/>.
    /// </summary>
    public InMemoryBookingRepo()
    {
        _bookings = [.. DataSeeder.Bookings];
    }

    /// <inheritdoc />
    public void Create(Booking booking)
    {
        if (booking.Id == Guid.Empty)
        {
            booking.Id = Guid.NewGuid();
        }

        _bookings.Add(booking);
    }

    /// <inheritdoc />
    public Booking? Read(Guid id)
    {
        return _bookings.FirstOrDefault(b => b.Id == id);
    }

    /// <inheritdoc />
    public List<Booking> ReadAll()
    {
        return [.. _bookings];
    }

    /// <inheritdoc />
    public void Update(Booking updatedBooking)
    {
        var index = _bookings.FindIndex(b => b.Id == updatedBooking.Id);
        if (index != -1)
        {
            _bookings[index] = updatedBooking;
        }
    }

    /// <inheritdoc />
    public void Delete(Guid id)
    {
        var item = Read(id);
        if (item is not null)
        {
            _bookings.Remove(item);
        }
    }
}