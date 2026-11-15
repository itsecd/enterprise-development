using Microsoft.EntityFrameworkCore;
using Vet.Domain.Entities;
using Vet.Domain.Enums;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий ветеринаров на базе Entity Framework Core
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class VeterinarianRepository(VetDbContext context) : IVeterinarianRepository
{
    /// <inheritdoc />
    /// <inheritdoc />
    public async Task<IReadOnlyList<Veterinarian>> GetAllAsync() =>
        await context.Veterinarians
            .Include(v => v.Specialization)
                .ThenInclude(s => s.Species)
            .ToListAsync();

    /// <inheritdoc />
    public async Task<Veterinarian?> GetByIdAsync(int entityId) =>
        await context.Veterinarians
            .Include(v => v.Specialization)
                .ThenInclude(s => s.Species)
            .FirstOrDefaultAsync(v => v.Id == entityId);

    /// <inheritdoc />
    public async Task<IReadOnlyList<Veterinarian>> GetBySpeciesAsync(Species species) =>
        await context.Veterinarians
            .AsNoTracking()
            .Include(v => v.Specialization)
                .ThenInclude(s => s.Species)
            .Where(v => v.Specialization.Species.Any(ss => ss.Species == species))
            .OrderBy(v => v.FullName)
            .ToListAsync();

    /// <inheritdoc />
    public async Task<bool> AnyBySpecializationIdAsync(int specializationId) =>
        await context.Veterinarians.AnyAsync(v => v.Specialization.Id == specializationId);

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