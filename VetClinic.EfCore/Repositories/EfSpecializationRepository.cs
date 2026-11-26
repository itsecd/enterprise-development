using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.EfCore.Repositories;

/// <summary>
/// EF Core реализация репозитория специализаций врачей и услуг
/// </summary>
/// <param name="context">Контекст базы данных ветеринарной клиники</param>
public class EfSpecializationRepository(VetClinicDbContext context) : IRepository<Specialization>
{
    /// <inheritdoc />
    public Task<List<Specialization>> GetAll() => context.Specializations.ToListAsync();

    /// <inheritdoc />
    public Task<Specialization?> Get(int id) => context.Specializations.FirstOrDefaultAsync(x => x.Id == id);

    /// <inheritdoc />
    public async Task Create(Specialization entity)
    {
        await context.Specializations.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Update(Specialization entity)
    {
        var existing = await context.Specializations.FindAsync(entity.Id)
            ?? throw new KeyNotFoundException($"Специализация с ID {entity.Id} не найдена.");

        existing.Title = entity.Title;
        existing.TargetSpecies = entity.TargetSpecies;

        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Delete(int id)
    {
        var entity = await context.Specializations.FindAsync(id);
        if (entity is not null)
        {
            context.Specializations.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}

