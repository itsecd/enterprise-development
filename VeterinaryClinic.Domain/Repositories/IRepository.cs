using VeterinaryClinic.Domain.Models;
using System.Linq.Expressions;
namespace VeterinaryClinic.Domain.Repositories;

/// <summary>
/// Определяет единый набор операций чтения и изменения сущностей в хранилище
/// </summary>
/// <typeparam name="T">Тип сущности, хранящейся в репозитории</typeparam>
public interface IRepository<T> where T : class, IEntity
{
    /// <summary>Возвращает все сущности из хранилища.</summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список сущностей.</returns>
    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>Находит сущности, соответствующие условию.</summary>
    /// <param name="predicate">Условие отбора сущностей.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список найденных сущностей.</returns>
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>Проверяет наличие хотя бы одной сущности, соответствующей условию.</summary>
    /// <param name="predicate">Условие проверки.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если подходящая сущность существует; иначе false.</returns>
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>Подсчитывает сущности, соответствующие условию.</summary>
    /// <param name="predicate">Условие отбора.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Количество подходящих сущностей.</returns>
    Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    /// <summary>Получает сущность по идентификатору.</summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Найденная сущность или null, если она отсутствует.</returns>
    Task<T?> GetAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>Добавляет сущность в хранилище.</summary>
    /// <param name="entity">Создаваемая сущность.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сохранённая сущность.</returns>
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>Изменяет существующую сущность.</summary>
    /// <param name="id">Идентификатор изменяемой сущности.</param>
    /// <param name="apply">Действие, применяющее изменения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Обновлённая сущность.</returns>
    /// <exception cref="KeyNotFoundException">Сущность не найдена.</exception>
    Task<T> UpdateAsync(int id, Action<T> apply, CancellationToken cancellationToken = default);

    /// <summary>Удаляет сущность по идентификатору.</summary>
    /// <param name="id">Идентификатор удаляемой сущности.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если сущность удалена; иначе false.</returns>
    Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default);
}
