using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;
using VetClinic.Domain.Shared.Enums;

namespace VetClinic.EfCore.Repositories;

/// <summary>
/// EF Core реализация репозитория для работы с ветеринарами в MySQL
/// </summary>
/// <param name="context">Контекст базы данных ветеринарной клиники</param>
public class EfVetRepository(VetClinicDbContext context) : IVetRepository
{
    /// <inheritdoc />
    public Task<List<Vet>> GetAll() =>
        context.Vets.Include(x => x.Specialization).ToListAsync();

    /// <inheritdoc />
    public Task<Vet?> Get(int id) =>
        context.Vets.Include(x => x.Specialization).FirstOrDefaultAsync(x => x.Id == id);

    /// <inheritdoc />
    public async Task Create(Vet entity)
    {
        entity.Specialization = null!;
        await context.Vets.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Update(Vet entity)
    {
        var existing = await context.Vets.FindAsync(entity.Id)
            ?? throw new KeyNotFoundException($"Врач с ID {entity.Id} не найден.");

        existing.FullName = entity.FullName;
        existing.PassportNumber = entity.PassportNumber;
        existing.BirthDate = entity.BirthDate;
        existing.ExpirienceYears = entity.ExpirienceYears;
        existing.SpecializationId = entity.SpecializationId;

        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Delete(int id)
    {
        var entity = await context.Vets.FindAsync(id);
        if (entity is not null)
        {
            context.Vets.Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    /// <inheritdoc />
    public Task<List<Vet>> FindVetsBySpecies(Species species) =>
        context.Vets
            .Include(x => x.Specialization)
            .Where(v => v.Specialization.TargetSpecies == species)
            .ToListAsync();
}
