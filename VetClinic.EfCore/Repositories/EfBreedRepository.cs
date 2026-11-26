using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.EfCore.Repositories;

/// <summary>
/// EF Core реализация репозитория пород животных
/// </summary>
/// <param name="context">Контекст базы данных ветеринарной клиники</param>
public class EfBreedRepository(VetClinicDbContext context) : IRepository<Breed>
{
    /// <inheritdoc />
    public Task<List<Breed>> GetAll() => context.Breeds.ToListAsync();

    /// <inheritdoc />
    public Task<Breed?> Get(int id) => context.Breeds.FirstOrDefaultAsync(x => x.Id == id);

    /// <inheritdoc />
    public async Task Create(Breed entity)
    {
        await context.Breeds.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Update(Breed entity)
    {
        var existing = await context.Breeds.FindAsync(entity.Id)
            ?? throw new KeyNotFoundException($"Порода с ID {entity.Id} не найдена.");

        existing.Title = entity.Title;
        existing.Type = entity.Type;

        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Delete(int id)
    {
        var entity = await context.Breeds.FindAsync(id);
        if (entity is not null)
        {
            context.Breeds.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
