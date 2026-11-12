using Microsoft.EntityFrameworkCore;
using Carwash.Domain;
using Carwash.Domain.Entities;

namespace Carwash.Infrastructure.EfCore.Repositories;

/// <summary>
/// Имплементация репозитория для работы с автомобилями с использованием Entity Framework Core
/// </summary>
/// <param name="context">Контекст БД</param>
public class CarEfCoreRepository(CarwashDbContext context) : IRepository<Car, int>
{
    /// <inheritdoc/>
    public async Task<Car> Create(Car entity)
    {
        var result = await context.Cars.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int entityId)
    {
        var result = await context.Cars.FirstOrDefaultAsync(c => c.Id == entityId);
        if (result == null)
            return false;

        context.Cars.Remove(result);
        await context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public async Task<Car?> Read(int entityId) => await context.Cars.Include(c => c.Client).FirstOrDefaultAsync(c => c.Id == entityId);

    /// <inheritdoc/>
    public async Task<List<Car>> ReadAll() => await context.Cars.Include(c => c.Client).ToListAsync();

    /// <inheritdoc/>
    public async Task<Car> Update(Car entity)
    {
        context.Cars.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
