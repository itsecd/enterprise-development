using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.SqlServer.Repositories;

public abstract class SqlRepository<TEntity>(SalonDbContext context) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    protected SalonDbContext Context { get; } = context;
    protected DbSet<TEntity> Entities => Context.Set<TEntity>();
    protected virtual IQueryable<TEntity> ReadQuery => Entities.AsNoTracking();

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await ReadQuery.OrderBy(item => item.Id).ToListAsync(cancellationToken);
    }

    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return ReadQuery.SingleOrDefaultAsync(item => item.Id == id, cancellationToken);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Entities.Add(entity);
        await SaveAsync(cancellationToken);
        return entity;
    }

    public virtual async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id <= 0)
        {
            return false;
        }

        Entities.Update(entity);
        return await SaveAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await Entities.FindAsync([id], cancellationToken);
        if (entity is null)
        {
            return false;
        }

        Entities.Remove(entity);
        return await SaveAsync(cancellationToken);
    }

    protected async Task<bool> SaveAsync(CancellationToken cancellationToken)
    {
        try
        {
            await Context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
        catch (DbUpdateException exception) when (exception.InnerException is SqlException { Number: 2601 or 2627 })
        {
            throw new InvalidOperationException("Специалист с таким паспортом уже существует", exception);
        }
        catch (DbUpdateException exception) when (exception.InnerException is SqlException { Number: 547 })
        {
            throw new InvalidOperationException("Операция нарушает связь с другими объектами", exception);
        }
    }
}
