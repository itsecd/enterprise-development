namespace CarWash.Domain;

/// <summary>
/// Интерфейс репозитория для CRUD-операций
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности</typeparam>
/// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Создание новой сущности
    /// </summary>
    Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Получение сущности по идентификатору
    /// </summary>
    Task<TEntity?> Read(TKey entityId);

    /// <summary>
    /// Получение всего списка сущностей
    /// </summary> 
    Task<IList<TEntity>> ReadAll();

    /// <summary>
    /// Обновление сущности в коллекции
    /// </summary>
    Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Удаление сущности из коллекции
    /// </summary>
    Task<bool> Delete(TKey entityId);
}
