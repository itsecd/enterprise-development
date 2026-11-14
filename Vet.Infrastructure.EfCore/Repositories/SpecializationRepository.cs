using Microsoft.EntityFrameworkCore;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий специализаций на базе Entity Framework Core
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class SpecializationRepository(VetDbContext context) : IRepository<Specialization, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<Specialization>> GetAllAsync()
    {
        return await context.Specializations.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Specialization?> GetByIdAsync(int entityId)
    {
        return await context.Specializations.FirstOrDefaultAsync(s => s.Id == entityId);
    }

    /// <inheritdoc />
    public async Task<Specialization> AddAsync(Specialization entity)
    {
        await context.Specializations.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(Specialization entity)
    {
        if (context.Entry(entity).State == EntityState.Detached)
        {
            var existing = await context.Specializations.FindAsync(entity.Id);
            if (existing is null)
            {
                return false;
            }

            context.Entry(existing).CurrentValues.SetValues(entity);
        }

        await context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int entityId)
    {
        var specialization = await context.Specializations.FindAsync(entityId);
        if (specialization is null)
        {
            return false;
        }

        context.Specializations.Remove(specialization);
        await context.SaveChangesAsync();
        return true;
    }
}