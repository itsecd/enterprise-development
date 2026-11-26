namespace VetClinic.Application.Contracts;

/// <summary>
/// Базовый контракт для CRUD операций
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создаёт новый элемент в репозитории
    /// </summary>
    /// <param name="dto">Информация о новом объекте</param>
    /// <returns>DTO нового объекта с присвоенным id</returns>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Получает элемент по идентификатору
    /// </summary>
    /// <param name="id">Id интересуемого объекта</param>
    /// <returns>DTO интересуемого объекта</returns>
    public Task<TDto?> Get(TKey id);

    /// <summary>
    /// Получает все элементы
    /// </summary>
    /// <returns>DTO всех объектов</returns>
    public Task<List<TDto>> GetAll();

    /// <summary>
    /// Обновляет элемент по идентификатору
    /// </summary>
    /// <param name="dto">Актуальная информация об объекте</param>
    /// <param name="id">Id интересуемого объекта</param>
    /// <returns>DTO интересуемого объекта</returns>
    public Task<TDto> Update(TCreateUpdateDto dto, TKey id);
    /// <summary>
    /// Удаляет элемент по идентификатору
    /// </summary>
    /// <param name="id">Id интересуемого объекта</param>
    /// <returns>true, если объект успешно удален; false, если запись не найдена</returns>
    public Task<bool> Delete(TKey id);
}