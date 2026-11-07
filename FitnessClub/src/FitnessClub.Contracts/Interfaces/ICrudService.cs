namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// Общий контракт CRUD-операций для ресурсов API
/// </summary>
/// <typeparam name="TDto">
/// DTO, возвращаемый клиенту
/// </typeparam>
/// <typeparam name="TEditDto">
/// DTO входных данных для создания и изменения
/// </typeparam>
public interface ICrudService<TDto, TEditDto>
{
    /// <summary>
    /// Получает все ресурсы
    /// </summary>
    /// <returns>Список DTO ресурсов</returns>
    public Task<IReadOnlyList<TDto>> GetAllAsync();

    /// <summary>
    /// Получает ресурс по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <returns>DTO ресурса или <see langword="null"/>, если он не найден</returns>
    public Task<TDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создаёт ресурс
    /// </summary>
    /// <param name="editDto">Данные для создания</param>
    /// <returns>DTO созданного ресурса</returns>
    public Task<TDto> AddAsync(TEditDto editDto);

    /// <summary>
    /// Обновляет ресурс
    /// </summary>
    /// <param name="id">Идентификатор обновляемого ресурса</param>
    /// <param name="editDto">Данные для обновления</param>
    /// <returns>DTO обновлённого ресурса или <see langword="null"/>, если он не найден</returns>
    public Task<TDto?> UpdateAsync(int id, TEditDto editDto);

    /// <summary>
    /// Удаляет ресурс
    /// </summary>
    /// <param name="id">Идентификатор удаляемого ресурса</param>
    /// <returns><see langword="true"/>, если ресурс найден и удалён; иначе <see langword="false"/></returns>
    public Task<bool> DeleteAsync(int id);
}