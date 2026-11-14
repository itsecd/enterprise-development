using Microsoft.EntityFrameworkCore;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий питомцев на базе Entity Framework Core
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class PetRepository(VetDbContext context) : IRepository<Pet, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<Pet>> GetAllAsync()
    {
        return await context.Pets
            .Include(p => p.Breed)
            .Include(p => p.Owner)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Pet?> GetByIdAsync(int entityId)
    {
        return await context.Pets
            .Include(p => p.Breed)
            .Include(p => p.Owner)
            .FirstOrDefaultAsync(p => p.Id == entityId);
    }

    /// <inheritdoc />
    public async Task<Pet> AddAsync(Pet entity)
    {
        await context.Pets.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(Pet entity)
    {
        if (context.Entry(entity).State == EntityState.Detached)
        {
            var existing = await context.Pets.FindAsync(entity.Id);
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
        var pet = await context.Pets.FindAsync(entityId);
        if (pet is null)
        {
            return false;
        }

        context.Pets.Remove(pet);
        await context.SaveChangesAsync();
        return true;
    }
}