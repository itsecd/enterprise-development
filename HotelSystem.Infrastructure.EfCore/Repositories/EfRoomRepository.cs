using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий номеров гостиницы с хранением данных в базе PostgreSQL
/// </summary>
public class EfRoomRepository(HotelDbContext context) : IRepository<Room>
{
    /// <inheritdoc />
    public async Task<List<Room>> GetAllAsync() =>
        await context.Rooms
            .Include(room => room.RoomType)
            .ToListAsync();

    /// <inheritdoc />
    public async Task<Room?> GetByIdAsync(int id) =>
        await context.Rooms
            .Include(room => room.RoomType)
            .FirstOrDefaultAsync(room => room.Id == id);

    /// <inheritdoc />
    public async Task<Room> AddAsync(Room room)
    {
        await context.Rooms.AddAsync(room);
        await context.SaveChangesAsync();

        return room;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, Room room)
    {
        var existingRoom = await GetByIdAsync(id);

        if (existingRoom is null)
        {
            return false;
        }

        existingRoom.RoomNumber = room.RoomNumber;
        existingRoom.Floor = room.Floor;
        existingRoom.HasBalcony = room.HasBalcony;
        existingRoom.RoomType = room.RoomType;

        await context.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var room = await GetByIdAsync(id);

        if (room is null)
        {
            return false;
        }

        context.Rooms.Remove(room);
        await context.SaveChangesAsync();

        return true;
    }
}