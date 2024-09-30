namespace DispatchTransportControl.Api.Repository;

/// <summary>
/// Обобщенный интерфейс репозитория
/// </summary>
public interface IRepository<TEntity, in TKey>
{
    /// <summary>
    /// Получение всех сущностей
    /// </summary>
    public IEnumerable<TEntity> GetAll();

    /// <summary>
    /// Получение сущности по id
    /// </summary>
    public TEntity? GetById(TKey id);

    /// <summary>
    /// Создание сущности
    /// </summary>
    public TEntity Create(TEntity entity);

    /// <summary>
    /// Изменение сущности
    /// </summary>
    public TEntity Update(TEntity entity);

    /// <summary>
    /// Удаление сущности
    /// </summary>
    public void Delete(TEntity entity);
}