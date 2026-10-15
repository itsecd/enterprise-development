using FitnesClub.Domain.Data;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Storage;

public class InMemoryBookingRepo : IRepository<Booking, Guid>
{
    private readonly List<Booking> _bookings;

    public InMemoryBookingRepo()
    {
        _bookings = [.. DataSeeder.Bookings];
    }

    public void Create(Booking booking)
    {
        if (booking.Id == Guid.Empty)
        {
            booking.Id = Guid.NewGuid();
        }

        _bookings.Add(booking);
    }

    public Booking? Read(Guid id)
    {
        return _bookings.FirstOrDefault(b => b.Id == id);
    }

    public List<Booking> ReadAll()
    {
        return [.. _bookings];
    }

    public void Update(Booking updatedBooking)
    {
        var index = _bookings.FindIndex(b => b.Id == updatedBooking.Id);
        if (index != -1)
        {
            _bookings[index] = updatedBooking;
        }
    }

    public void Delete(Guid id)
    {
        var item = Read(id);
        if (item is not null)
        {
            _bookings.Remove(item);
        }
    }
}