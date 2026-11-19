using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Реализация репозитория автомобилей на Entity Framework Core.
/// </summary>
public class EfCarRepository(AutoServiceDbContext context) : ICarRepository
{
    /// <summary>
    /// Получить список всех автомобилей.
    /// </summary>
    public async Task<IEnumerable<Car>> GetAllAsync() => await context.Cars.ToListAsync();

    /// <summary>
    /// Получить автомобиль по его идентификатору.
    /// </summary>
    public async Task<Car?> GetByIdAsync(Guid id) => await context.Cars.FirstOrDefaultAsync(c => c.Id == id);

    /// <summary>
    /// Добавить новый автомобиль.
    /// </summary>
    public async Task AddAsync(Car car)
    {
        context.Cars.Add(car);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновить данные существующего автомобиля.
    /// </summary>
    public async Task UpdateAsync(Car car)
    {
        var existing = await context.Cars.FindAsync(car.Id);
        if (existing is null) return;

        existing.LicensePlate = car.LicensePlate;
        existing.Brand = car.Brand;
        existing.Model = car.Model;
        existing.Year = car.Year;
        existing.ClientId = car.ClientId;

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить автомобиль.
    /// </summary>
    public async Task DeleteAsync(Car car)
    {
        context.Cars.Remove(car);
        await context.SaveChangesAsync();
    }
}
