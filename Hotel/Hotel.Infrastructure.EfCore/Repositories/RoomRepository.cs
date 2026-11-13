using Hotel.Domain;
using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий номеров гостиницы
/// </summary>
public class RoomRepository(HotelDbContext context) : IRepository<Room, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<Room>> GetAll()
    {
        return await context.Rooms
            .AsNoTracking()
            .Include(room => room.RoomType)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Room?> GetById(int id)
    {
        return await context.Rooms
            .Include(room => room.RoomType)
            .FirstOrDefaultAsync(room => room.Id == id);
    }

    /// <inheritdoc />
    public async Task<Room> Add(Room room)
    {
        var roomType = await GetRoomType(room.RoomType.Id);

        room.RoomTypeId = roomType.Id;
        room.RoomType = roomType;

        context.Rooms.Add(room);
        await context.SaveChangesAsync();

        return room;
    }

    /// <inheritdoc />
    public async Task<Room?> Update(Room room)
    {
        var existingRoom = await context.Rooms.FindAsync(room.Id);

        if (existingRoom is null) return null;

        var roomType = await GetRoomType(room.RoomType.Id);

        existingRoom.RoomNumber = room.RoomNumber;
        existingRoom.Floor = room.Floor;
        existingRoom.HasBalcony = room.HasBalcony;
        existingRoom.RoomTypeId = roomType.Id;
        existingRoom.RoomType = roomType;

        await context.SaveChangesAsync();

        return existingRoom;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id)
    {
        var room = await context.Rooms.FindAsync(id);

        if (room is null) return false;

        context.Rooms.Remove(room);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Получить тип номера по идентификатору
    /// </summary>
    private async Task<RoomType> GetRoomType(int roomTypeId)
    {
        var roomType = await context.RoomTypes.FindAsync(roomTypeId);

        return roomType ?? throw new ArgumentException($"Не удалось найти тип номера с идентификатором {roomTypeId}");
    }
}