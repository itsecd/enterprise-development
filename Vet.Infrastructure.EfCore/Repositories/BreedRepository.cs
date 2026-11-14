using Microsoft.EntityFrameworkCore;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий пород
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class BreedRepository(VetDbContext context) : IRepository<Breed, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<Breed>> GetAllAsync()
    {
        return await context.Breeds.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Breed?> GetByIdAsync(int entityId)
    {
        return await context.Breeds.FirstOrDefaultAsync(b => b.Id == entityId);
    }

    /// <inheritdoc />
    public async Task<Breed> AddAsync(Breed entity)
    {
        await context.Breeds.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(Breed entity)
    {
        if (context.Entry(entity).State == EntityState.Detached)
        {
            var existing = await context.Breeds.FindAsync(entity.Id);
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
        var breed = await context.Breeds.FindAsync(entityId);
        if (breed is null)
        {
            return false;
        }

        context.Breeds.Remove(breed);
        await context.SaveChangesAsync();
        return true;
    }
}