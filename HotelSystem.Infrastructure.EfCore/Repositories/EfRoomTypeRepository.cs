using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий типов номеров гостиницы с хранением данных в базе PostgreSQL
/// </summary>
public class EfRoomTypeRepository(HotelDbContext context) : IRepository<RoomType>
{
    /// <inheritdoc />
    public async Task<List<RoomType>> GetAllAsync() =>
        await context.RoomTypes.ToListAsync();

    /// <inheritdoc />
    public async Task<RoomType?> GetByIdAsync(int id) =>
        await context.RoomTypes.FirstOrDefaultAsync(roomType => roomType.Id == id);

    /// <inheritdoc />
    public async Task<RoomType> AddAsync(RoomType roomType)
    {
        await context.RoomTypes.AddAsync(roomType);
        await context.SaveChangesAsync();

        return roomType;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, RoomType roomType)
    {
        var existingRoomType = await GetByIdAsync(id);

        if (existingRoomType is null)
        {
            return false;
        }

        context.Entry(existingRoomType).CurrentValues.SetValues(roomType);
        await context.SaveChangesAsync();

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var roomType = await GetByIdAsync(id);

        if (roomType is null)
        {
            return false;
        }

        context.RoomTypes.Remove(roomType);
        await context.SaveChangesAsync();

        return true;
    }
}