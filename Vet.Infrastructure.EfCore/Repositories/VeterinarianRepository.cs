using Microsoft.EntityFrameworkCore;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий ветеринаров на базе Entity Framework Core
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class VeterinarianRepository(VetDbContext context) : IRepository<Veterinarian, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<Veterinarian>> GetAllAsync()
    {
        return await context.Veterinarians
            .Include(v => v.Specialization)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Veterinarian?> GetByIdAsync(int entityId)
    {
        return await context.Veterinarians
            .Include(v => v.Specialization)
            .FirstOrDefaultAsync(v => v.Id == entityId);
    }

    /// <inheritdoc />
    public async Task<Veterinarian> AddAsync(Veterinarian entity)
    {
        await context.Veterinarians.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(Veterinarian entity)
    {
        if (context.Entry(entity).State == EntityState.Detached)
        {
            var existing = await context.Veterinarians.FindAsync(entity.Id);
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
        var veterinarian = await context.Veterinarians.FindAsync(entityId);
        if (veterinarian is null)
        {
            return false;
        }

        context.Veterinarians.Remove(veterinarian);
        await context.SaveChangesAsync();
        return true;
    }
}