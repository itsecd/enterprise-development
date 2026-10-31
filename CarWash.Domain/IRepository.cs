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
    void Create(TEntity entity);

    /// <summary>
    /// Получение сущности по идентификатору
    /// </summary>
    TEntity Read(TKey entityId);

    /// <summary>
    /// Получение всего списка сущностей
    /// </summary> 
    List<TEntity> ReadAll();

    /// <summary>
    /// Обновление сущности в коллекции
    /// </summary>
    void Update(TEntity entity);

    /// <summary>
    /// Удаление сущности из коллекции
    /// </summary>
    void Delete(TKey entityId);
}
