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
    public async Task<IReadOnlyList<Room>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.Rooms
            .AsNoTracking()
            .Include(room => room.RoomType)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Room?> GetById(int id, CancellationToken cancellationToken = default)
    {
        return await context.Rooms
            .Include(room => room.RoomType)
            .FirstOrDefaultAsync(room => room.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Room> Add(Room room, CancellationToken cancellationToken = default)
    {
        var roomType = await GetRoomType(room.RoomType.Id, cancellationToken);

        room.RoomTypeId = roomType.Id;
        room.RoomType = roomType;

        context.Rooms.Add(room);
        await context.SaveChangesAsync(cancellationToken);

        return room;
    }

    /// <inheritdoc />
    public async Task<Room?> Update(Room room, CancellationToken cancellationToken = default)
    {
        var existingRoom = await context.Rooms.FindAsync([room.Id], cancellationToken);

        if (existingRoom is null) return null;

        var roomType = await GetRoomType(room.RoomType.Id, cancellationToken);

        existingRoom.RoomNumber = room.RoomNumber;
        existingRoom.Floor = room.Floor;
        existingRoom.HasBalcony = room.HasBalcony;
        existingRoom.RoomTypeId = roomType.Id;
        existingRoom.RoomType = roomType;

        await context.SaveChangesAsync(cancellationToken);

        return existingRoom;
    }

    /// <inheritdoc />
    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        var room = await context.Rooms.FindAsync([id], cancellationToken);

        if (room is null) return false;

        context.Rooms.Remove(room);
        await context.SaveDeletionAsync("Нельзя удалить номер, для которого есть бронирования", cancellationToken);

        return true;
    }

    /// <summary>
    /// Получить тип номера по идентификатору
    /// </summary>
    /// <param name="roomTypeId">Идентификатор типа номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    private async Task<RoomType> GetRoomType(int roomTypeId, CancellationToken cancellationToken = default)
    {
        var roomType = await context.RoomTypes.FindAsync([roomTypeId], cancellationToken);

        return roomType ?? throw new ArgumentException($"Не удалось найти тип номера с идентификатором {roomTypeId}");
    }
}
