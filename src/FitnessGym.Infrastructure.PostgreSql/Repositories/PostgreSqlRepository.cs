using FitnessGym.Domain.Exceptions;
using FitnessGym.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FitnessGym.Infrastructure.PostgreSql.Repositories;

/// <summary>
/// Операции над одной таблицей; запись затрагивает только скалярные поля и внешние ключи.
/// </summary>
public abstract class PostgreSqlRepository<TEntity>(FitnessGymDbContext context) : IRepository<TEntity>
    where TEntity : class, IIdentified
{
    protected FitnessGymDbContext Context => context;
    protected virtual IQueryable<TEntity> Query => context.Set<TEntity>().AsNoTracking();

    public async Task<IReadOnlyList<TEntity>> GetAllAsync() =>
        await Query.OrderBy(entity => entity.Id).ToListAsync();

    public Task<TEntity?> GetByIdAsync(int id) => Query.FirstOrDefaultAsync(entity => entity.Id == id);

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        // ToObject копирует только отображённые поля: отсоединённые навигации не вставляются повторно.
        var stored = (TEntity)context.Entry(entity).CurrentValues.ToObject();
        stored.Id = 0;
        context.Set<TEntity>().Add(stored);
        await SaveChangesAsync();
        entity.Id = stored.Id;
        return entity;
    }

    public async Task<bool> UpdateAsync(int id, TEntity entity)
    {
        var stored = await context.Set<TEntity>().FindAsync(id);
        if (stored is null)
        {
            return false;
        }

        entity.Id = id;
        context.Entry(stored).CurrentValues.SetValues(entity);
        try
        {
            await SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            context.Entry(stored).State = EntityState.Detached;
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var stored = await context.Set<TEntity>().FindAsync(id);
        if (stored is null)
        {
            return false;
        }

        context.Set<TEntity>().Remove(stored);
        try
        {
            await SaveChangesAsync();
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            context.Entry(stored).State = EntityState.Detached;
            return false;
        }
    }

    private async Task SaveChangesAsync()
    {
        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException exception) when (
            exception.InnerException is PostgresException
            {
                SqlState: PostgresErrorCodes.ForeignKeyViolation or PostgresErrorCodes.RestrictViolation,
            })
        {
            throw new EntityReferenceException(exception);
        }
    }
}
