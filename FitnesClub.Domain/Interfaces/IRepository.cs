namespace FitnesClub.Domain.Interfaces;

/// <summary>
/// Обобщенный интерфейс репозитория для выполнения базовых CRUD-операций над сущностями.
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности, управляемой репозиторием.</typeparam>
/// <typeparam name="TKey">Тип уникального идентификатора сущности (структура, например <see cref="Guid"/>).</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Добавляет новую сущность в хранилище.
    /// </summary>
    /// <param name="entity">Добавляемый экземпляр сущности.</param>
    public void Create(TEntity entity);

    /// <summary>
    /// Возвращает список всех сущностей, находящихся в хранилище.
    /// </summary>
    /// <returns>Список всех сохраненных сущностей.</returns>
    public List<TEntity> ReadAll();

    /// <summary>
    /// Выполняет поиск сущности по её уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор сущности.</param>
    /// <returns>Найденная сущность либо <see langword="null"/>, если сущность с указанным идентификатором отсутствует.</returns>
    public TEntity? Read(TKey id);

    /// <summary>
    /// Обновляет состояние существующей сущности в хранилище.
    /// </summary>
    /// <param name="entity">Сущность с обновленными данными.</param>
    public void Update(TEntity entity);

    /// <summary>
    /// Удаляет сущность из хранилища по её уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор удаляемой сущности.</param>
    public void Delete(TKey id);
}