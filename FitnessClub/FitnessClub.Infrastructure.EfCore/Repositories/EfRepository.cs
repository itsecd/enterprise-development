using FitnessClub.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.EfCore.Repositories;

/// <summary>CRUD-операции со скалярными полями и внешними ключами сущностей.</summary>
public class EfRepository<TEntity>(FitnessClubDbContext context) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync() =>
        await context.Set<TEntity>().AsNoTracking().OrderBy(entity => entity.Id).ToListAsync();

    /// <inheritdoc />
    public Task<TEntity?> GetByIdAsync(int id) =>
        context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(entity => entity.Id == id);

    /// <inheritdoc />
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Added;
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, TEntity entity)
    {
        var existing = await context.Set<TEntity>().FindAsync(id);
        if (existing is null)
        {
            return false;
        }

        entity.Id = id;
        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Set<TEntity>().FindAsync(id);
        if (entity is null)
        {
            return false;
        }

        context.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}
