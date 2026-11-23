using Microsoft.EntityFrameworkCore;
using MuseumSystem.Domain.Interfaces;

namespace MuseumSystem.Infrastructure.EfCore;

public class EfRepository<TEntity>(DbContext context) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();

        // Подгружаем связанные сущности, при их передаче
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        var existing = await _dbSet.FindAsync(entity.Id);
        if (existing is null)
        {
            return null; // Если записи нет
        }

        // Копируем новые значения в существующую сущность
        _dbSet.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();

        return existing; // Возвращаем обновлённую сущность
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is null) return false;

        _dbSet.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}