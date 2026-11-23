using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoServiceLab.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий видов работ (EF Core)
/// </summary>
public class WorkTypeEfCoreRepository(AutoServiceDbContext context) : IWorkTypeRepository
{
    /// <inheritdoc />
    public async Task<WorkType?> GetAsync(int id) =>
        await context.WorkTypes.FirstOrDefaultAsync(w => w.Id == id);

    /// <inheritdoc />
    public async Task<IReadOnlyList<WorkType>> GetAllAsync() =>
        await context.WorkTypes.ToListAsync();

    /// <inheritdoc />
    public async Task AddAsync(WorkType entity)
    {
        await context.WorkTypes.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(WorkType entity)
    {
        context.WorkTypes.Update(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var workType = await GetAsync(id);
        if (workType == null) return false;

        context.WorkTypes.Remove(workType);
        await context.SaveChangesAsync();
        return true;
    }
}
