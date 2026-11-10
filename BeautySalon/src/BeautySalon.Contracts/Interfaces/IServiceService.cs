using BeautySalon.Contracts.Dto;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Сервис управления услугами салона
/// </summary>
public interface IServiceService
{
    /// <summary>
    /// Получает список всех услуг
    /// </summary>
    public Task<IReadOnlyList<ServiceDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает услугу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    public Task<ServiceDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт услугу
    /// </summary>
    /// <param name="dto">Данные для создания</param>
    /// <returns>Созданный объект с назначенным идентификатором</returns>
    public Task<ServiceDto> CreateAsync(ServiceEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет услугу
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="dto">Новые данные</param>
    /// <returns>Обновлённый объект</returns>
    public Task<ServiceDto> UpdateAsync(int id, ServiceEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет услугу
    /// </summary>
    /// <param name="id">Идентификатор</param>
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}