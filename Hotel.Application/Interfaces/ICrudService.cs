namespace Hotel.Application.Interfaces;

/// <summary>
/// Интерфейс для работы с запросами
/// </summary>
/// <typeparam name="TRequest">Запрос</typeparam>
/// <typeparam name="TDto">Dto</typeparam>
public interface ICrudService<TRequest, TDto>
{
    /// <summary>
    /// Получение всех записей
    /// </summary>
    /// <returns>Список Dto сущностей</returns>
    public Task<IReadOnlyList<TDto>> GetAllAsync();

    /// <summary>
    /// Вернуть запись по Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Найденный Dto сущности</returns>
    public Task<TDto> GetAsync(int id);

    /// <summary>
    /// Добавление записи
    /// </summary>
    /// <param name="request">Данные для создания</param>
    /// <returns>Созданный Dto сущности</returns>
    public Task<TDto> CreateAsync(TRequest request);

    /// <summary>
    /// Обновление существующей записи
    /// </summary>
    /// <param name="id">Id обновляемой записи</param>
    /// <param name="request">Новые данные</param>
    /// <returns>Выполняемая task в текущий момент</returns>
    public Task UpdateAsync(int id, TRequest request);

    /// <summary>
    /// Удалить запись
    /// </summary>
    /// <param name="id">Id удаляемой записи</param>
    /// <returns>Выполняемая task в текущий момент</returns>
    public Task DeleteAsync(int id);
}