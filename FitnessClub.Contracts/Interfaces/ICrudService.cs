namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Универсальный контракт CRUD-сервиса
/// </summary>
/// <typeparam name="TDto">Read-DTO сущности</typeparam>
/// <typeparam name="TEditDto">Edit-DTO сущности</typeparam>
public interface ICrudService<TDto, TEditDto> where TDto : class where TEditDto : class
{
    /// <summary>
    /// Создаёт запись и возвращает её
    /// </summary>
    public Task<TDto> Create(TEditDto editDto);

    /// <summary>
    /// Возвращает все записи
    /// </summary>
    public Task<IReadOnlyList<TDto>> ReadAll();

    /// <summary>
    /// Возвращает запись по идентификатору
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task<TDto> Read(int id);

    /// <summary>
    /// Обновляет запись и возвращает её
    /// </summary>
    public Task<TDto> Update(int id, TEditDto editDto);

    /// <summary>
    /// Удаляет запись
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns></returns>
    public Task Delete(int id);
}