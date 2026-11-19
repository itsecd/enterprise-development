using Hotel.Domain.Entities;
using Hotel.Infrastructure;

public class RoomTypeRepository(HotelDbContext context) :
    EfRepository<RoomType, int>(context), IRoomTypeRepository
{ }