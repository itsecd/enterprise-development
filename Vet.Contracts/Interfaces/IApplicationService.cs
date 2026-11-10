namespace Vet.Contracts.Interfaces;

/// <summary>
/// Базовый интерфейс сервиса приложения для выполнения CRUD-операций
/// </summary>
/// <typeparam name="TGetDto">Тип DTO для GET запросов</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип DTO для POST/PUT запросов</typeparam>
/// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
public interface IApplicationService<TGetDto, TCreateUpdateDto, TKey>
{
    /// <summary>
    /// Получить все элементы
    /// </summary>
    /// <returns>Список DTO всех элементов</returns>
    public Task<IReadOnlyList<TGetDto>> GetAllAsync();

    /// <summary>
    /// Получить элемент по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор элемента</param>
    /// <returns>DTO найденного элемента или null, если не найден</returns>
    public Task<TGetDto?> GetByIdAsync(TKey id);

    /// <summary>
    /// Создать новый элемент
    /// </summary>
    /// <param name="dto">DTO с данными для создания</param>
    /// <returns>DTO созданного элемента</returns>
    public Task<TGetDto> CreateAsync(TCreateUpdateDto dto);

    /// <summary>
    /// Обновить существующий элемент
    /// </summary>
    /// <param name="id">Идентификатор элемента</param>
    /// <param name="dto">DTO с обновленными данными</param>
    /// <returns>True, если элемент успешно обновлен; иначе false</returns>
    public Task<bool> UpdateAsync(TKey id, TCreateUpdateDto dto);

    /// <summary>
    /// Удалить элемент по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор элемента</param>
    /// <returns>True, если элемент успешно удален; иначе false</returns>
    public Task<bool> DeleteAsync(TKey id);
}