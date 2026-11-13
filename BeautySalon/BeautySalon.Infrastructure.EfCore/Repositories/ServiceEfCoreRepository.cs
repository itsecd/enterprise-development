using BeautySalon.Domain;
using BeautySalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для услуг с Entity Framework Core
/// </summary>
/// <param name="context">Контекст БД</param>
public class ServiceEfCoreRepository(BeautySalonDbContext context) : IRepository<Service, int>
{
    /// <inheritdoc/>
    public async Task<Service> Create(Service entity)
    {
        var result = await context.Services.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int entityId)
    {
        var result = await context.Services.FirstOrDefaultAsync(s => s.Id == entityId);
        if (result == null)
            return false;

        context.Services.Remove(result);
        await context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public async Task<Service?> Read(int entityId) => await context.Services.FirstOrDefaultAsync(s => s.Id == entityId);

    /// <inheritdoc/>
    public async Task<List<Service>> ReadAll() => await context.Services.ToListAsync();

    /// <inheritdoc/>
    public async Task<Service> Update(Service entity)
    {
        context.Services.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
