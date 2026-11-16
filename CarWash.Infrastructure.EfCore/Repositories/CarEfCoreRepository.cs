using CarWash.Domain;
using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core репозиторий для автомобилей
/// </summary>
public class CarEfCoreRepository(CarWashDbContext context)
    : IRepository<Car, int>
{
    public async Task<Car> Create(Car entity)
    {
        entity.Id = 0;
        context.Cars.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(int entityId)
        => await context.Cars
            .Where(car => car.Id == entityId)
            .ExecuteDeleteAsync() > 0;

    public async Task<Car?> Read(int entityId)
        => await context.Cars
            .Include(car => car.Customer)
            .Include(car => car.Orders)
            .FirstOrDefaultAsync(car => car.Id == entityId);

    public async Task<IList<Car>> ReadAll()
        => await context.Cars.ToListAsync();

    public async Task<Car> Update(Car entity)
    {
        var existing = await context.Cars
            .FirstOrDefaultAsync(car => car.Id == entity.Id)
            ?? throw new KeyNotFoundException(
                $"Автомобиль с Id {entity.Id} не найден");

        context.Entry(existing).CurrentValues.SetValues(entity);

        await context.SaveChangesAsync();
        return existing;
    }
}