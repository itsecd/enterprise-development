namespace VetClinic.Domain.Interfaces;

/// <summary>
/// Generic интерфейс репозитория
/// </summary>
/// <typeparam name="T">Класс доменной модели</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary> 
    /// Получить все записи из источника данных 
    /// </summary>
    public Task<List<T>> GetAll();

    /// <summary> 
    /// Найти одну запись по ее уникальному идентификатору ID
    /// </summary>
    public Task<T?> Get(int id);

    /// <summary> 
    /// Добавить новый объект в память
    /// </summary>
    public Task Create(T entity);

    /// <summary> 
    /// Обновить данные существующего объекта
    /// </summary>
    public Task Update(T entity);

    /// <summary> 
    /// Удалить объект из памяти
    /// <param>Id</param>
    /// </summary>
    public Task Delete(int id);
}
