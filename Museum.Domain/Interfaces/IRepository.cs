using System.Collections.Generic;
using System.Threading.Tasks;

namespace Museum.Domain.Interfaces;

/// <summary>
/// Описывает основные операции для работы с данными
/// </summary>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Возвращает все записи
    /// </summary>
    Task<IReadOnlyList<TEntity>> GetAllAsync();

    /// <summary>
    /// Находит запись по идентификатору
    /// </summary>
    Task<TEntity?> GetByIdAsync(int id);

    /// <summary>
    /// Добавляет новую запись
    /// </summary>
    Task<TEntity> AddAsync(TEntity entity);

    /// <summary>
    /// Обновляет существующую запись
    /// </summary>
    Task<bool> UpdateAsync(TEntity entity);

    /// <summary>
    /// Удаляет запись по идентификатору
    /// </summary>
    Task<bool> DeleteAsync(int id);
}
