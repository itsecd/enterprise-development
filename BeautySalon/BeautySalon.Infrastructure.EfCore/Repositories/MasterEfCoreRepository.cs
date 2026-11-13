using BeautySalon.Domain;
using BeautySalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для мастеров с Entity Framework Core
/// </summary>
/// <param name="context">Контекст БД</param>
public class MasterEfCoreRepository(BeautySalonDbContext context) : IRepository<Master, int>
{
    /// <inheritdoc/>
    public async Task<Master> Create(Master entity)
    {
        var result = await context.Masters.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int entityId)
    {
        var result = await context.Masters.FirstOrDefaultAsync(m => m.Id == entityId);
        if (result == null)
            return false;

        context.Masters.Remove(result);
        await context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public async Task<Master?> Read(int entityId) => await context.Masters.FirstOrDefaultAsync(m => m.Id == entityId);

    /// <inheritdoc/>
    public async Task<List<Master>> ReadAll() => await context.Masters.ToListAsync();

    /// <inheritdoc/>
    public async Task<Master> Update(Master entity)
    {
        context.Masters.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
