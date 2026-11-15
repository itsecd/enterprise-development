namespace FitnessClub.Application.Contracts;

/// <summary>
/// Базовый контракт для CRUD операций
/// </summary>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создаёт новый элемент
    /// </summary>
    Task<TDto> Create(TCreateUpdateDto dto);
    /// <summary>
    /// Получает элемент по идентификатору
    /// </summary>
    Task<TDto?> Get(TKey id);
    /// <summary>
    /// Получает все элементы
    /// </summary>
    Task<IList<TDto>> GetAll();
    /// <summary>
    /// Обновляет элемент по идентификатору
    /// </summary>
    Task<TDto> Update(TCreateUpdateDto dto, TKey id);
    /// <summary>
    /// Удаляет элемент по идентификатору
    /// </summary>
    Task<bool> Delete(TKey id);
}