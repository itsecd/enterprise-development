using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Repositories;

namespace VetClinic.Infrastructure.SqlServer.Repositories;

/// <summary>
/// Базовый репозиторий EF Core. Списки читаются без отслеживания, поиск по ID отслеживает сущность.
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <param name="context">Контекст базы данных</param>
public abstract class EfRepository<TEntity>(VetClinicDbContext context) : IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    protected VetClinicDbContext Context { get; } = context;

    /// <summary>
    /// Таблица сущностей
    /// </summary>
    protected DbSet<TEntity> Entities => Context.Set<TEntity>();

    /// <inheritdoc/>
    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await Entities.AsNoTracking().ToListAsync(cancellationToken);

    /// <inheritdoc/>
    public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await Entities.FindAsync([id], cancellationToken);

    /// <inheritdoc/>
    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Entities.Add(entity);
        await SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <inheritdoc/>
    public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
        Context.SaveChangesAsync(cancellationToken);

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await Entities.FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        Entities.Remove(entity);
        await SaveChangesAsync(cancellationToken);
        return true;
    }
}
