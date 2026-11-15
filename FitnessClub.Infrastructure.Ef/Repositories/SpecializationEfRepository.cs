using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.Ef.Repositories;

/// <summary>
/// Репозиторий специализаций с хранением в БД
/// </summary>
public class SpecializationEfRepository(FitnessClubDbContext context): IRepository<Specialization, int>
{
    /// <inheritdoc/>
    public async Task<Specialization?> Get(int id)
    {
        return await context.Specializations.FindAsync(id);
    }
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Specialization>> GetAll()
    {
        return await context.Specializations.ToListAsync();
    }
    /// <inheritdoc/>
    public async Task Add(Specialization entity)
    {
        context.Specializations.Add(entity);
        await context.SaveChangesAsync();
    }
    /// <inheritdoc/>
    public async Task Update(Specialization entity)
    {
        context.Specializations.Update(entity);
        await context.SaveChangesAsync();
    }
    /// <inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        var specialization = await context.Specializations.FindAsync(id);
        if (specialization is null) return false;
        context.Specializations.Remove(specialization);
        await context.SaveChangesAsync();
        return true;
    }
}
