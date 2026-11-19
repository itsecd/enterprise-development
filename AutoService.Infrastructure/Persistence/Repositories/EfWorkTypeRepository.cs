using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Реализация репозитория видов работ на Entity Framework Core.
/// </summary>
public class EfWorkTypeRepository(AutoServiceDbContext context) : IWorkTypeRepository
{
    /// <summary>
    /// Получить список всех видов работ.
    /// </summary>
    public async Task<IEnumerable<WorkType>> GetAllAsync() => await context.WorkTypes.ToListAsync();

    /// <summary>
    /// Получить вид работ по его идентификатору.
    /// </summary>
    public async Task<WorkType?> GetByIdAsync(Guid id) => await context.WorkTypes.FirstOrDefaultAsync(w => w.Id == id);

    /// <summary>
    /// Добавить новый вид работ.
    /// </summary>
    public async Task AddAsync(WorkType workType)
    {
        context.WorkTypes.Add(workType);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновить данные существующего вида работ.
    /// </summary>
    public async Task UpdateAsync(WorkType workType)
    {
        var existing = await context.WorkTypes.FindAsync(workType.Id);
        if (existing is null) return;

        existing.Name = workType.Name;
        existing.Category = workType.Category;
        existing.Price = workType.Price;
        existing.Duration = workType.Duration;

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить вид работ.
    /// </summary>
    public async Task DeleteAsync(WorkType workType)
    {
        context.WorkTypes.Remove(workType);
        await context.SaveChangesAsync();
    }
}
