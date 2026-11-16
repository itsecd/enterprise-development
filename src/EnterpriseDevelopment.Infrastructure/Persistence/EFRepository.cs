using EnterpriseDevelopment.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseDevelopment.Infrastructure.Persistence;

/// <summary>
/// Универсальный репозиторий для работы с базой данных
/// </summary>
public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entities;

    /// <summary>
    /// Создаёт репозиторий для указанной сущности
    /// </summary>
    public EfRepository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    /// <summary>
    /// Получает все сущности
    /// </summary>
    public IEnumerable<T> GetAll()
    {
        return _entities.ToList();
    }

    /// <summary>
    /// Получает сущность по идентификатору
    /// </summary>
    public T? GetById(int id)
    {
        return _entities.Find(id);
    }

    /// <summary>
    /// Добавляет новую сущность
    /// </summary>
    public T Add(T entity)
    {
        _entities.Add(entity);
        _context.SaveChanges();

        return entity;
    }

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    public void Update(T entity)
    {
        _entities.Update(entity);
        _context.SaveChanges();
    }

    /// <summary>
    /// Удаляет существующую сущность
    /// </summary>
    public void Delete(T entity)
    {
        _entities.Remove(entity);
        _context.SaveChanges();
    }
}
