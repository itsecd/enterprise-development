using CarWash.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.EfCore.Repositories;

/// <summary>
/// Базовая реализация репозитория на EF Core
/// </summary>
/// <typeparam name="TEntity">Тип сущности (с ключом Id)</typeparam>
/// <param name="context">Контекст базы данных</param>
public abstract class EfRepository<TEntity>(CarWashDbContext context) : IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    protected CarWashDbContext Context { get; } = context;

    /// <summary>
    /// Набор сущностей
    /// </summary>
    protected DbSet<TEntity> Entities => Context.Set<TEntity>();

    /// <summary>
    /// Базовый запрос; наследники добавляют сюда загрузку связанных сущностей (Include)
    /// </summary>
    /// <returns>Запрос к сущностям</returns>
    protected virtual IQueryable<TEntity> Query() => Entities;

    /// <inheritdoc />
    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await Query().OrderBy(e => EF.Property<int>(e, "Id")).ToListAsync();

    /// <inheritdoc />
    public async Task<TEntity?> GetByIdAsync(int id)
        => await Query().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

    /// <inheritdoc />
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await Entities.AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(TEntity entity)
    {
        // Сущность, полученная из этого же контекста, уже отслеживается: достаточно сохранить изменения
        if (Context.Entry(entity).State == EntityState.Detached)
        {
            Entities.Update(entity);
        }

        await Context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        var entity = await Entities.FindAsync(id);
        if (entity is null)
        {
            return;
        }

        Entities.Remove(entity);
        await Context.SaveChangesAsync();
    }
}
