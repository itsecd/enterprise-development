using Microsoft.EntityFrameworkCore;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий владельцев на базе Entity Framework Core
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class OwnerRepository(VetDbContext context) : IRepository<Owner, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<Owner>> GetAllAsync()
    {
        return await context.Owners
            .Include(o => o.Pets)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Owner?> GetByIdAsync(int entityId)
    {
        return await context.Owners
            .Include(o => o.Pets)
            .FirstOrDefaultAsync(o => o.Id == entityId);
    }

    /// <inheritdoc />
    public async Task<Owner> AddAsync(Owner entity)
    {
        await context.Owners.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(Owner entity)
    {
        if (context.Entry(entity).State == EntityState.Detached)
        {
            var existing = await context.Owners.FindAsync(entity.Id);
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
        var owner = await context.Owners.FindAsync(entityId);
        if (owner is null)
        {
            return false;
        }

        context.Owners.Remove(owner);
        await context.SaveChangesAsync();
        return true;
    }
}