using Hotel.Domain;
using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий типов номеров
/// </summary>
public class RoomTypeRepository(HotelDbContext context) : IRepository<RoomType, int>
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<RoomType>> GetAll(CancellationToken cancellationToken = default)
    {
        return await context.RoomTypes
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<RoomType?> GetById(int id, CancellationToken cancellationToken = default)
    {
        return await context.RoomTypes.FindAsync([id], cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<RoomType> Add(RoomType roomType, CancellationToken cancellationToken = default)
    {
        context.RoomTypes.Add(roomType);
        await context.SaveChangesAsync(cancellationToken);

        return roomType;
    }

    /// <inheritdoc/>
    public async Task<RoomType?> Update(RoomType roomType, CancellationToken cancellationToken = default)
    {
        var existingRoomType = await GetById(roomType.Id, cancellationToken);

        if (existingRoomType is null) return null;

        existingRoomType.Category = roomType.Category;
        existingRoomType.Area = roomType.Area;
        existingRoomType.BedCount = roomType.BedCount;
        existingRoomType.HasBathroom = roomType.HasBathroom;
        existingRoomType.PricePerDay = roomType.PricePerDay;

        await context.SaveChangesAsync(cancellationToken);

        return existingRoomType;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        var roomType = await GetById(id, cancellationToken);

        if (roomType is null) return false;

        context.RoomTypes.Remove(roomType);
        await context.SaveDeletionAsync("Нельзя удалить тип номера, к которому привязаны номера", cancellationToken);

        return true;
    }
}