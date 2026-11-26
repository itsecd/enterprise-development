using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.EfCore.Repositories;

/// <summary>
/// EF Core реализация репозитория для управления медицинскими услугами 
/// </summary>
/// <param name="context">Контекст базы данных ветеринарной клиники</param>
public class EfServiceRepository(VetClinicDbContext context) : IRepository<Service>
{
    /// <inheritdoc />
    public Task<List<Service>> GetAll() =>
        context.Services.Include(x => x.Specialization).ToListAsync();

    /// <inheritdoc />
    public Task<Service?> Get(int id) =>
        context.Services.Include(x => x.Specialization).FirstOrDefaultAsync(x => x.Id == id);

    /// <inheritdoc />
    public async Task Create(Service entity)
    {
        entity.Specialization = null!;
        await context.Services.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Update(Service entity)
    {
        var existing = await context.Services.FindAsync(entity.Id)
            ?? throw new KeyNotFoundException($"Услуга с ID {entity.Id} не найдена.");

        existing.Title = entity.Title;
        existing.Price = entity.Price;
        existing.SpecializationId = entity.SpecializationId;

        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Delete(int id)
    {
        var entity = await context.Services.FindAsync(id);
        if (entity is not null)
        {
            context.Services.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
