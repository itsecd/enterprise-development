namespace Museum.Contracts.Interfaces;

/// <summary>
/// Описывает основные операции для работы с сущностями через DTO
/// </summary>
public interface IEntityService<TDto, TEditDto>
{
    /// <summary>
    /// Возвращает список всех сущностей
    /// </summary>
    Task<IReadOnlyList<TDto>> GetAllAsync();

    /// <summary>
    /// Находит сущность по идентификатору
    /// </summary>
    Task<TDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создаёт новую сущность
    /// </summary>
    Task<TDto> CreateAsync(TEditDto dto);

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    Task<bool> UpdateAsync(int id, TEditDto dto);

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    Task<bool> DeleteAsync(int id);
}
