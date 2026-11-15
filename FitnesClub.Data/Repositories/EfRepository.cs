using FitnesClub.Data.Context;
using FitnesClub.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnesClub.Data.Repositories;

/// <summary>
/// Обобщенный репозиторий Entity Framework Core для работы с сущностями в базе данных.
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности.</typeparam>
/// <param name="context">Контекст базы данных фитнес-клуба.</param>
public class EfRepository<TEntity>(FitnesClubDbContext context) : IRepository<TEntity, Guid>
    where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    /// <inheritdoc />
    public void Create(TEntity entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        _dbSet.Add(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public TEntity? Read(Guid id)
    {
        return _dbSet.AsNoTracking().FirstOrDefault(e => e.Id == id);
    }

    /// <inheritdoc />
    public List<TEntity> ReadAll()
    {
        return _dbSet.AsNoTracking().ToList();
    }

    /// <inheritdoc />
    public void Update(TEntity entity)
    {
        var existing = _dbSet.FirstOrDefault(e => e.Id == entity.Id);
        if (existing is not null)
        {
            context.Entry(existing).CurrentValues.SetValues(entity);
            context.SaveChanges();
        }
    }

    /// <inheritdoc />
    public void Delete(Guid id)
    {
        var existing = _dbSet.FirstOrDefault(e => e.Id == id);
        if (existing is not null)
        {
            _dbSet.Remove(existing);
            context.SaveChanges();
        }
    }
}