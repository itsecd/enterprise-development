using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.PostgreSql.Data;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.PostgreSql.Repositories;

/// <summary>
/// Универсальный репозиторий
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности</typeparam>
/// <param name="context">Контекст базы данных</param>
public class EfRepository<TEntity>(FitnessClubDbContext context) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    protected FitnessClubDbContext Context { get; } = context;

    /// <summary>
    /// Базовый запрос для чтения
    /// </summary>
    protected virtual IQueryable<TEntity> Query => Context.Set<TEntity>();

    /// <summary>
    /// Добавляет сущность
    /// </summary>
    /// <param name="entity">Сущность</param>
    /// <returns>Сохранённая сущность</returns>
    public virtual async Task<TEntity> Create(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Возвращает все сущности вместе с загруженными навигационными свойствами
    /// </summary>
    /// <returns>Коллекция сущностей</returns>
    public virtual async Task<IReadOnlyList<TEntity>> ReadAll() =>
        await Query.ToListAsync();

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор</param>
    /// <returns>Сущность или null</returns>
    public virtual async Task<TEntity?> Read(int entityId) =>
        await Query.FirstOrDefaultAsync(e => e.Id == entityId);

    /// <summary>
    /// Обновляет скалярные свойства и внешние ключи существующей сущности
    /// </summary>
    /// <param name="entity">Сущность с новыми значениями</param>
    /// <returns>true, если сущность найдена и обновлена</returns>
    public virtual async Task<bool> Update(TEntity entity)
    {
        var existing = await Context.Set<TEntity>().FindAsync(entity.Id);
        if (existing is null)
            return false;

        Context.Entry(existing).CurrentValues.SetValues(entity);
        await Context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор</param>
    /// <returns>true, если сущность найдена и удалена</returns>
    public virtual async Task<bool> Delete(int entityId)
    {
        var existing = await Context.Set<TEntity>().FindAsync(entityId);
        if (existing is null)
            return false;

        Context.Set<TEntity>().Remove(existing);
        await Context.SaveChangesAsync();

        return true;
    }
}
