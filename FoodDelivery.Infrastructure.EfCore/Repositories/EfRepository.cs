using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EfCore.Repositories;

/// <summary>
/// Предоставляет базовую реализацию репозитория с использованием efcore
/// </summary>
public abstract class EfRepository<T>(FoodDeliveryDbContext dbContext) : IRepository<T> where T : class, IEntity
{
    /// <summary>
    /// Контекст бд
    /// </summary>
    protected readonly FoodDeliveryDbContext DbContext = dbContext;

    /// <inheritdoc />
    public virtual async Task<IList<T>> GetAllAsync()
    {
        return await DbContext
            .Set<T>()
            .ToListAsync();
    }

    /// <inheritdoc />
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await DbContext
            .Set<T>()
            .FindAsync(id);
    }

    /// <inheritdoc />
    public virtual async Task<T> AddAsync(T entity)
    {
        await DbContext
            .Set<T>()
            .AddAsync(entity);

        await DbContext.SaveChangesAsync();

        return entity;
    }

    /// <inheritdoc />
    public virtual async Task<T?> UpdateAsync(T entity)
    {
        var existingEntity = await GetByIdAsync(entity.Id);

        if (existingEntity is null)
        {
            return null;
        }

        DbContext.Entry(existingEntity)
            .CurrentValues
            .SetValues(entity);

        await DbContext.SaveChangesAsync();

        return existingEntity;
    }

    /// <inheritdoc />
    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        if (entity is null)
        {
            return false;
        }

        DbContext.Set<T>().Remove(entity);

        await DbContext.SaveChangesAsync();

        return true;
    }
}