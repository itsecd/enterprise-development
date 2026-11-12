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
    public async Task<IReadOnlyList<RoomType>> GetAll()
    {
        return await context.RoomTypes
            .AsNoTracking()
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<RoomType?> GetById(int id)
    {
        return await context.RoomTypes.FindAsync(id);
    }

    /// <inheritdoc/>
    public async Task<RoomType> Add(RoomType roomType)
    {
        context.RoomTypes.Add(roomType);
        await context.SaveChangesAsync();

        return roomType;
    }

    /// <inheritdoc/>
    public async Task<RoomType?> Update(RoomType roomType)
    {
        var existingRoomType = await GetById(roomType.Id);

        if (existingRoomType is null) return null;

        existingRoomType.Category = roomType.Category;
        existingRoomType.Area = roomType.Area;
        existingRoomType.BedCount = roomType.BedCount;
        existingRoomType.HasBathroom = roomType.HasBathroom;
        existingRoomType.PricePerDay = roomType.PricePerDay;

        await context.SaveChangesAsync();

        return existingRoomType;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        var roomType = await GetById(id);

        if (roomType is null) return false;

        context.RoomTypes.Remove(roomType);
        await context.SaveChangesAsync();

        return true;
    }
}