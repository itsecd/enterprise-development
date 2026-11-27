using Microsoft.EntityFrameworkCore;
using Npgsql;
using VetClinic.Domain.Exceptions;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Infrastructure.PostgreSql.Repositories;

public sealed class EfRepository<T>(VetClinicDbContext context, IQueryable<T> query) : IRepository<T>
    where T : class, IEntity
{
    public async Task<IEnumerable<T>> GetAllAsync() =>
        await query.OrderBy(entity => entity.Id).ToListAsync();

    public Task<T?> GetByIdAsync(int id) =>
        query.FirstOrDefaultAsync(entity => entity.Id == id);

    public async Task AddAsync(T entity)
    {
        context.Set<T>().Add(entity);
        await context.SaveChangesAsync();
    }

    public Task UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        return context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            return await context.Set<T>().Where(entity => entity.Id == id).ExecuteDeleteAsync() > 0;
        }
        catch (PostgresException ex) when (ex.SqlState == PostgresErrorCodes.ForeignKeyViolation)
        {
            throw new EntityInUseException(ex);
        }
    }
}
