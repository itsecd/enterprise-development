using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Infrastructure.PostgreSQL;

public sealed class PostgreSqlRepository<T>(ClinicDbContext db) : IRepository<T> where T : class, IEntity
{
    private IQueryable<T> Query()
    {
        if (typeof(T) == typeof(Pet))
            return (IQueryable<T>)db.Pets.Include(p => p.Breed).Include(p => p.Owner);
        if (typeof(T) == typeof(Veterinarian))
            return (IQueryable<T>)db.Veterinarians.Include(v => v.Specialization);
        if (typeof(T) == typeof(Appointment))
            return (IQueryable<T>)db.Appointments
                .Include(a => a.Pet).ThenInclude(p => p.Breed)
                .Include(a => a.Pet).ThenInclude(p => p.Owner)
                .Include(a => a.Veterinarian).ThenInclude(v => v.Specialization);
        if (typeof(T) == typeof(Owner))
            return (IQueryable<T>)db.Owners.Include(o => o.Pets);
        return db.Set<T>();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await Query().AsSplitQuery().ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
        => await Query().Where(predicate).AsSplitQuery().ToListAsync(cancellationToken);

    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
        => db.Set<T>().AnyAsync(predicate, cancellationToken);

    public Task<int> CountAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
        => db.Set<T>().CountAsync(predicate, cancellationToken);

    public Task<T?> GetAsync(int id, CancellationToken cancellationToken = default)
        => Query().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await db.Set<T>().AddAsync(entity, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> UpdateAsync(int id, Action<T> apply, CancellationToken cancellationToken = default)
    {
        var entity = await GetAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Сущность с идентификатором {id} не найдена");
        apply(entity);
        await db.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetAsync(id, cancellationToken);
        if (entity is null) return false;
        db.Set<T>().Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
