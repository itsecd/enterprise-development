namespace VeterinaryClinic.Domain.Repositories;

/// <summary>
/// Определяет единый набор операций чтения и изменения сущностей в хранилище
/// </summary>
/// <typeparam name="T">Тип сущности, хранящейся в репозитории</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Возвращает все записи
    /// </summary>
    public IReadOnlyList<T> GetAll();

    /// <summary>
    /// Возвращает записи по условию
    /// </summary>
    /// <param name="predicate">Условие отбора записей</param>
    public IReadOnlyList<T> Find(Func<T, bool> predicate);

    /// <summary>
    /// Проверяет наличие записи по условию
    /// </summary>
    /// <param name="predicate">Условие отбора записей</param>
    public bool Any(Func<T, bool> predicate);

    /// <summary>
    /// Считает записи по условию
    /// </summary>
    /// <param name="predicate">Условие отбора записей</param>
    public int Count(Func<T, bool> predicate);

    /// <summary>
    /// Возвращает запись по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public T? Get(int id);

    /// <summary>
    /// Возвращает следующий свободный идентификатор
    /// </summary>
    public int NextId();

    /// <summary>
    /// Создаёт запись с новым идентификатором
    /// </summary>
    /// <param name="factory">Функция создания сущности по идентификатору</param>
    public T Create(Func<int, T> factory);

    /// <summary>
    /// Обновляет запись по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="apply">Действие для изменения сущности</param>
    public T Update(int id, Action<T> apply);

    /// <summary>
    /// Удаляет запись по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public bool Remove(int id);
}
