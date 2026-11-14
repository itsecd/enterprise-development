using BeautySalon.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.PostgreSQL.Repositories;

/// <summary>
/// Общие операции с таблицей сущностей с ключом <see cref="IEntity.Id"/>.
/// </summary>
/// <typeparam name="TEntity">Доменная сущность.</typeparam>
/// <param name="db">Контекст текущего запроса.</param>
internal abstract class PostgreSqlRepository<TEntity>(SalonDbContext db) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Контекст текущего запроса.
    /// </summary>
    protected SalonDbContext Database { get; } = db;

    /// <summary>
    /// Таблица сущностей текущего типа.
    /// </summary>
    protected DbSet<TEntity> Entities => Database.Set<TEntity>();

    /// <inheritdoc />
    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Entities.AsNoTracking().OrderBy(entity => entity.Id).ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return Entities.SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Entities.Add(entity);
        await Database.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var current = await Entities.FindAsync([entity.Id], cancellationToken);
        if (current is null)
        {
            return false;
        }

        ApplyChanges(current, entity);
        await Database.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var deleted = await Entities.Where(entity => entity.Id == id).ExecuteDeleteAsync(cancellationToken);
        return deleted != 0;
    }

    /// <summary>
    /// Переносит новые поля в найденную запись, не изменяя её ключ.
    /// </summary>
    /// <param name="current">Запись, отслеживаемая контекстом.</param>
    /// <param name="updated">Новые данные.</param>
    protected abstract void ApplyChanges(TEntity current, TEntity updated);
}
