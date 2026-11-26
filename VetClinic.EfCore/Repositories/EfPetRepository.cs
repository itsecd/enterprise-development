using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.EfCore.Repositories;

/// <summary>
/// EF Core реализация репозитория питомцев
/// </summary>
/// <param name="context">Контекст базы данных ветеринарной клиники</param>
public class EfPetRepository(VetClinicDbContext context) : IRepository<Pet>
{
    /// <inheritdoc />
    public Task<List<Pet>> GetAll() =>
        context.Pets
            .Include(x => x.Breed)
            .Include(x => x.Owner)
            .ToListAsync();

    /// <inheritdoc />
    public Task<Pet?> Get(int id) =>
        context.Pets
            .Include(x => x.Breed)
            .Include(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Id == id);

    /// <inheritdoc />
    public async Task Create(Pet entity)
    {
        entity.Breed = null!;
        entity.Owner = null!;
        await context.Pets.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Update(Pet entity)
    {
        var existing = await context.Pets.FindAsync(entity.Id)
            ?? throw new KeyNotFoundException($"Питомец с ID {entity.Id} не найден.");

        existing.Name = entity.Name;
        existing.Type = entity.Type;
        existing.BirthDate = entity.BirthDate;
        existing.Weight = entity.Weight;
        existing.BreedId = entity.BreedId;
        existing.OwnerId = entity.OwnerId;

        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Delete(int id)
    {
        var entity = await context.Pets.FindAsync(id);
        if (entity is not null)
        {
            context.Pets.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
