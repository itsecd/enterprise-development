using Microsoft.EntityFrameworkCore;
using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.MySql.Repositories;

/// <summary>
/// Выполняет основные операции с записями в базе данных
/// </summary>
public abstract class EfRepository<TEntity>(MuseumDbContext context) : IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    protected MuseumDbContext Context => context;

    /// <summary>
    /// Запрос к таблице; наследники подключают связанные данные
    /// </summary>
    protected virtual IQueryable<TEntity> Query() => context.Set<TEntity>();

    /// <summary>
    /// Возвращает все записи
    /// </summary>
    public async Task<IReadOnlyList<TEntity>> GetAllAsync()
    {
        return await Query()
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(entity => EF.Property<int>(entity, "Id"))
            .ToListAsync();
    }

    /// <summary>
    /// Находит запись по идентификатору
    /// </summary>
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await Query()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(entity => EF.Property<int>(entity, "Id") == id);
    }

    /// <summary>
    /// Добавляет новую запись; идентификатор присваивает база данных
    /// </summary>
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Обновляет существующую запись
    /// </summary>
    public virtual async Task<bool> UpdateAsync(TEntity entity)
    {
        var id = (int)context.Entry(entity).Property("Id").CurrentValue!;
        var existing = await context.Set<TEntity>().FindAsync(id);

        if (existing is null)
        {
            return false;
        }

        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Удаляет запись по идентификатору
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await context.Set<TEntity>().FindAsync(id);

        if (existing is null)
        {
            return false;
        }

        context.Set<TEntity>().Remove(existing);
        await context.SaveChangesAsync();

        return true;
    }
}
