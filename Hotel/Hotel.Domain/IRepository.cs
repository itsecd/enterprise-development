namespace Hotel.Domain;

/// <summary>
/// Обобщённый интерфейс репозитория
/// </summary>
/// <typeparam name="TEntity">Тип сущности</typeparam>
/// <typeparam name="TKey">Тип идентификатора</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Получить все сущности
    /// </summary>
    public IReadOnlyList<TEntity> GetAll();

    /// <summary>
    /// Получить сущность по идентификатору
    /// </summary>
    public TEntity? GetById(TKey id);

    /// <summary>
    /// Добавить новую сущность
    /// </summary>
    public TEntity Add(TEntity entity);

    /// <summary>
    /// Обновить существующую сущность
    /// </summary>
    public TEntity? Update(TEntity entity);

    /// <summary>
    /// Удалить сущность по идентификатору
    /// </summary>
    public bool Delete(TKey id);
}