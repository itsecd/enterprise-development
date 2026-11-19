using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Реализация репозитория механиков на Entity Framework Core.
/// </summary>
public class EfMechanicRepository(AutoServiceDbContext context) : IMechanicRepository
{
    /// <summary>
    /// Получить список всех механиков.
    /// </summary>
    public async Task<IEnumerable<Mechanic>> GetAllAsync() => await context.Mechanics.ToListAsync();

    /// <summary>
    /// Получить механика по его идентификатору.
    /// </summary>
    public async Task<Mechanic?> GetByIdAsync(Guid id) => await context.Mechanics.FirstOrDefaultAsync(m => m.Id == id);

    /// <summary>
    /// Добавить нового механика.
    /// </summary>
    public async Task AddAsync(Mechanic mechanic)
    {
        context.Mechanics.Add(mechanic);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновить данные существующего механика.
    /// </summary>
    public async Task UpdateAsync(Mechanic mechanic)
    {
        var existing = await context.Mechanics.FindAsync(mechanic.Id);
        if (existing is null) return;

        existing.Passport = mechanic.Passport;
        existing.FullName = mechanic.FullName;
        existing.Specialization = mechanic.Specialization;
        existing.Experience = mechanic.Experience;

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить механика.
    /// </summary>
    public async Task DeleteAsync(Mechanic mechanic)
    {
        context.Mechanics.Remove(mechanic);
        await context.SaveChangesAsync();
    }
}
