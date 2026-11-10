using BeautySalon.Contracts.Dto;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Сервис управления мастерами
/// </summary>
public interface IMasterService
{
    /// <summary>
    /// Получает список всех мастеров
    /// </summary>
    public Task<IReadOnlyList<MasterDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает мастера по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    public Task<MasterDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт мастера
    /// </summary>
    /// <param name="dto">Данные для создания</param>
    /// <returns>Созданный объект с назначенным идентификатором</returns>
    public Task<MasterDto> CreateAsync(MasterEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет мастера
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="dto">Новые данные</param>
    /// <returns>Обновлённый объект</returns>
    public Task<MasterDto> UpdateAsync(int id, MasterEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет мастера
    /// </summary>
    /// <param name="id">Идентификатор</param>
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}