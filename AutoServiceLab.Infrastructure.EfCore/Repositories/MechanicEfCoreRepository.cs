using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoServiceLab.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий механиков (EF Core)
/// </summary>
public class MechanicEfCoreRepository(AutoServiceDbContext context) : IMechanicRepository
{
    /// <inheritdoc />
    public async Task<Mechanic?> GetAsync(int id) =>
        await context.Mechanics.FirstOrDefaultAsync(m => m.Id == id);

    /// <inheritdoc />
    public async Task<IReadOnlyList<Mechanic>> GetAllAsync() =>
        await context.Mechanics.ToListAsync();

    /// <inheritdoc />
    public async Task AddAsync(Mechanic entity)
    {
        await context.Mechanics.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Mechanic entity)
    {
        context.Mechanics.Update(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var mechanic = await GetAsync(id);
        if (mechanic == null) return false;

        context.Mechanics.Remove(mechanic);
        await context.SaveChangesAsync();
        return true;
    }
}
