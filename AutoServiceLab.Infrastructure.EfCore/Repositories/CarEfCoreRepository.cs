using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoServiceLab.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий автомобилей (EF Core)
/// </summary>
public class CarEfCoreRepository(AutoServiceDbContext context) : ICarRepository
{
    /// <inheritdoc />
    public async Task<Car?> GetAsync(int id) =>
        await context.Cars.FirstOrDefaultAsync(c => c.Id == id);

    /// <inheritdoc />
    public async Task<IReadOnlyList<Car>> GetAllAsync() =>
        await context.Cars.ToListAsync();

    /// <inheritdoc />
    public async Task AddAsync(Car entity)
    {
        await context.Cars.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Car entity)
    {
        context.Cars.Update(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var car = await GetAsync(id);
        if (car == null) return false;

        context.Cars.Remove(car);
        await context.SaveChangesAsync();
        return true;
    }
}
