namespace AutoServiceLab.Application.Contracts.Interfaces;

/// <summary>
/// Базовый контракт для CRUD-сервисов
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создать запись
    /// </summary>
    public TDto Create(TCreateUpdateDto dto);

    /// <summary>
    /// Получить запись по идентификатору
    /// </summary>
    public TDto? Get(TKey id);

    /// <summary>
    /// Получить все записи
    /// </summary>
    public IList<TDto> GetAll();

    /// <summary>
    /// Обновить запись
    /// </summary>
    public TDto Update(TCreateUpdateDto dto, TKey id);

    /// <summary>
    /// Удалить запись
    /// </summary>
    public bool Delete(TKey id);
}
