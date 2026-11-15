namespace Museum.Contracts.Interfaces;

/// <summary>
/// Описывает основные операции для работы с сущностями через DTO
/// </summary>
public interface IEntityService<TDto, TEditDto>
{
    /// <summary>
    /// Возвращает список всех сущностей
    /// </summary>
    public Task<IReadOnlyList<TDto>> GetAllAsync();

    /// <summary>
    /// Находит сущность по идентификатору
    /// </summary>
    public Task<TDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создаёт новую сущность
    /// </summary>
    public Task<TDto> CreateAsync(TEditDto dto);

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    public Task<bool> UpdateAsync(int id, TEditDto dto);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}
