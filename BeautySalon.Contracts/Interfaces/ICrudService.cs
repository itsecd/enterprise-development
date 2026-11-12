namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Общие операции с ресурсом
/// </summary>
public interface ICrudService<TRequest, TResponse>
{
    /// <summary>
    /// Получить список объектов
    /// </summary>
    IReadOnlyList<TResponse> GetAll();

    /// <summary>
    /// Получить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    TResponse GetById(int id);

    /// <summary>
    /// Создать объект
    /// </summary>
    /// <param name="request">Данные объекта</param>
    TResponse Create(TRequest request);

    /// <summary>
    /// Обновить объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <param name="request">Новые данные объекта</param>
    void Update(int id, TRequest request);

    /// <summary>
    /// Удалить объект
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    void Delete(int id);
}
