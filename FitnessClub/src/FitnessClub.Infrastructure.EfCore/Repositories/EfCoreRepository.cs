using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.EfCore.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.EfCore.Repositories;

/// <summary>
/// Реализует базовые операции репозитория средствами Entity Framework Core
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
public class EfCoreRepository<TEntity>(
    FitnessClubDbContext dbContext) : IRepository<TEntity>
    where TEntity : Entity
{
    /// <summary>
    /// Контекст текущего scope
    /// </summary>
    protected FitnessClubDbContext DbContext => dbContext;

    /// <summary>
    /// Набор сущностей текущего типа
    /// </summary>
    protected DbSet<TEntity> Entities => dbContext.Set<TEntity>();

    /// <inheritdoc />
    public async Task<IReadOnlyList<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await Entities
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await Entities.SingleOrDefaultAsync(
            entity => entity.Id == id,
            cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TEntity> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        Entities.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<TEntity?> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        Entities.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return false;

        Entities.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
