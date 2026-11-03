namespace HotelSystem.Domain.Repositories;

/// <summary>
/// Порт для доступа к хранилищу сущностей
/// </summary>
public interface IRepository<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    public List<TEntity> GetAll();

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    public TEntity? GetById(int id);

    /// <summary>
    /// Добавляет новую сущность и присваивает ей идентификатор
    /// </summary>
    public TEntity Add(TEntity entity);

    /// <summary>
    /// Заменяет существующую сущность
    /// </summary>
    public bool Update(int id, TEntity entity);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    public bool Delete(int id);
}