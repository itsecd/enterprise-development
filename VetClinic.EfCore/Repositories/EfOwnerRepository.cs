using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.EfCore.Repositories;

/// <summary>
/// EF Core реализация репозитория владельцев животных 
/// </summary>
/// <param name="context">Контекст базы данных ветеринарной клиники.</param>
public class EfOwnerRepository(VetClinicDbContext context) : IRepository<Owner>
{
    /// <inheritdoc />
    public Task<List<Owner>> GetAll() => context.Owners.ToListAsync();

    /// <inheritdoc />
    public Task<Owner?> Get(int id) => context.Owners.FirstOrDefaultAsync(x => x.Id == id);

    /// <inheritdoc />
    public async Task Create(Owner entity)
    {
        await context.Owners.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Update(Owner entity)
    {
        var existing = await context.Owners.FindAsync(entity.Id)
            ?? throw new KeyNotFoundException($"Владелец с ID {entity.Id} не найден.");

        existing.FullName = entity.FullName;
        existing.Phone = entity.Phone;
        existing.Adress = entity.Adress;

        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Delete(int id)
    {
        var entity = await context.Owners.FindAsync(id);
        if (entity is not null)
        {
            context.Owners.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
