using Hotel.Domain.Entities;
using Hotel.Infrastructure;


public class RoomRepository(HotelDbContext context) :
    EfRepository<Room, int>(context), IRoomRepository
{ }