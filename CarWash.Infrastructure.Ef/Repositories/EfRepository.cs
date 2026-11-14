using System.Linq.Expressions;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.Ef.Repositories;

/// <summary>
/// Базовый репозиторий Entity Framework Core
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public abstract class EfRepository<TEntity>(CarWashDbContext context) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Контекст текущего запроса
    /// </summary>
    protected CarWashDbContext Context { get; } = context;

    /// <summary>
    /// Запрос сущностей с необходимыми связями
    /// </summary>
    protected virtual IQueryable<TEntity> Query => Context.Set<TEntity>();

    /// <inheritdoc />
    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await Query.AsNoTracking().OrderBy(entity => entity.Id).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        Query.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

    /// <inheritdoc />
    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) =>
        Context.Set<TEntity>().AnyAsync(predicate, cancellationToken);

    /// <inheritdoc />
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Context.Set<TEntity>().Add(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var current = await Context.Set<TEntity>().FindAsync([entity.Id], cancellationToken);
        if (current is null)
            return;
        Context.Entry(current).CurrentValues.SetValues(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await Context.Set<TEntity>().FindAsync([id], cancellationToken);
        if (entity is null)
            return;
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }
}
