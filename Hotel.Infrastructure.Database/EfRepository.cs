using System.Linq.Expressions;
using Hotel.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Database;

/// <summary>
/// Репозиторий для работы с базой данных
/// </summary>
/// <typeparam name="T">Тип сущности</typeparam>
public class EfRepository<T>(HotelDbContext context) : IRepository<T> where T : class, IEntity
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    /// <summary>
    /// Получить все сущности из базы данных
    /// </summary>
    /// <returns>Коллекция сущностей</returns>
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Получить одну сущность
    /// </summary>
    /// <param name="id">Id сущности</param>
    /// <returns>Сущность</returns>
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    /// <summary>
    /// Добавить сущность в базу даных
    /// </summary>
    /// <returns>Добавленная сущность</returns>
    public async Task<T> AddAsync(T entity)
    {
        entity.Id = 0;
        AttachExistingNavigations(entity);

        await _dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Обновить значение сущности
    /// </summary>
    /// <param name="id">Id обновляемой сущности</param>
    /// <param name="update">Обновляющее действие</param>
    /// <returns>True, если сущность найдена и обновлена, иначе False</returns>
    public async Task<bool> UpdateAsync(int id, Action<T> update)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return false;

        update(entity);
        AttachExistingNavigations(entity);

        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удалить сущность
    /// </summary>
    /// <param name="id">Id удаляемой сущности</param>
    /// <exception cref="KeyNotFoundException">Сущность не найдена</exception>
    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            throw new KeyNotFoundException($"Сущность(Id={id}) не найдена.");

        _dbSet.Remove(entity);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Получение сущностей по условию
    /// </summary>
    /// <param name="predicate">Условие поиска</param>
    /// <returns>Список сущностей</returns>
    public async Task<IReadOnlyList<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    /// <summary>
    /// Привязка связанных сущностей к контексту
    /// </summary>
    private void AttachExistingNavigations(T entity)
    {
        var entry = context.Entry(entity);
        foreach (var navigation in entry.Navigations)
        {
            if (navigation.CurrentValue is IEntity navEntity && navEntity.Id > 0)
            {
                var navEntry = context.Entry(navEntity);
                if (navEntry.State == EntityState.Detached)
                {
                    navEntry.State = EntityState.Unchanged;
                }
            }
        }
    }
}
