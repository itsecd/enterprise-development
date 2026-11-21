using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EfCore.Repositories;

/// <summary>
/// Выполняет общие операции EF Core для сущностей с целочисленным ключом
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <param name="context">Контекст базы данных</param>
public abstract class EfCoreRepository<TEntity>(FoodDeliveryDbContext context)
    : IRepository<TEntity> where TEntity : class, IHasId
{
    /// <summary>Контекст хранилища</summary>
    protected FoodDeliveryDbContext Context { get; } = context;

    /// <summary>Запрос к сущностям с необходимыми связями</summary>
    protected virtual IQueryable<TEntity> Query => Context.Set<TEntity>();

    /// <inheritdoc/>
    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Query
            .OrderBy(entity => entity.Id)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return Query.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Context.Set<TEntity>().Add(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var existing = await Context.Set<TEntity>().FindAsync([entity.Id], cancellationToken);
        if (existing is null)
        {
            return false;
        }

        Context.Entry(existing).CurrentValues.SetValues(entity);
        await Context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await Context.Set<TEntity>().FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return;
        }

        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync(cancellationToken);
    }
}
