using AutoService.Contracts.Dtos;

namespace AutoService.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с механиками.
/// </summary>
public interface IMechanicService
{
    /// <summary>
    /// Получить список всех механиков.
    /// </summary>
    public Task<IEnumerable<MechanicDto>> GetAllAsync();

    /// <summary>
    /// Получить механика по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор механика.</param>
    public Task<MechanicDto?> GetByIdAsync(Guid id);

    /// <summary>
    /// Добавить нового механика.
    /// </summary>
    /// <param name="mechanicDto">Данные нового механика.</param>
    public Task<MechanicDto> CreateAsync(MechanicDto mechanicDto);

    /// <summary>
    /// Обновить данные механика.
    /// </summary>
    /// <param name="id">Идентификатор механика.</param>
    /// <param name="mechanicDto">Новые данные механика.</param>
    public Task UpdateAsync(Guid id, MechanicUpdateDto mechanicDto);

    /// <summary>
    /// Удалить механика по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор механика.</param>
    public Task DeleteAsync(Guid id);
}
