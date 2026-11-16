namespace EnterpriseDevelopment.Application.Interfaces;

/// <summary>
/// Общий интерфейс для выполнения CRUD-операций над сущностями
/// </summary>
public interface ICrudService<TDto, TCreateDto, TUpdateDto>
    where TDto : class
{
    /// <summary>
    /// Получает все элементы
    /// </summary>
    /// <returns>Коллекция элементов</returns>
    public IEnumerable<TDto> GetAll();

    /// <summary>
    /// Получает элемент по идентификатору
    /// </summary>
    /// <returns>Найденный элемент или null</returns>
    public TDto? GetById(int id);

    /// <summary>
    /// Создаёт новый элемент
    /// </summary>
    /// <returns>Созданный элемент</returns>
    public TDto Create(TCreateDto dto);

    /// <summary>
    /// Обновляет существующий элемент
    /// </summary>
    /// <returns>True при успешном обновлении, иначе false</returns>
    public bool Update(int id, TUpdateDto dto);

    /// <summary>
    /// Удаляет элемент по идентификатору
    /// </summary>
    /// <returns>True при успешном удалении, иначе false</returns>
    public bool Delete(int id);
}
