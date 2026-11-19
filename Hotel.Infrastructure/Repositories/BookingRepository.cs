using Hotel.Domain.Entities;
using Hotel.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class BookingRepository(HotelDbContext context) :
    EfRepository<Booking, int>(context), IBookingRepository
{
    public async Task<bool> HasOverlappingBookingAsync(int roomId, DateOnly checkIn, uint days, CancellationToken ct = default)
    {
        var checkOut = checkIn.AddDays((int)days);
        return await DbSet.AnyAsync(b =>
            b.Room.Id == roomId &&
            b.CheckInDate < checkOut &&
            b.CheckInDate.AddDays((int)b.DaysOfStay) > checkIn,
            ct);
    }
}