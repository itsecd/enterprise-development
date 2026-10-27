namespace Hotel.Application.Contracts;

/// <summary>
/// Обобщенный CRUD интерфейс
/// </summary>
/// <typeparam name="TDto">DTO для получения данных</typeparam>
/// <typeparam name="TInputDto">DTO для создания и обновления</typeparam>
/// <typeparam name="TKey">Тип идентификатора</typeparam>
public interface ICrudService<TDto, TInputDto, TKey>
    where TKey : notnull
{
    /// <summary>
    /// Получить список всех объектов
    /// </summary>
    public IReadOnlyList<TDto> GetAll();

    /// <summary>
    /// Получить объект по идентификатору
    /// </summary>
    public TDto? GetById(TKey id);

    /// <summary>
    /// Создать объект
    /// </summary>
    public TDto Create(TInputDto dto);

    /// <summary>
    /// Обновить существующий объект
    /// </summary>
    public TDto? Update(TKey id, TInputDto dto);

    /// <summary>
    /// Удалить объект по идентификатору
    /// </summary>
    public bool Delete(TKey id);
}
