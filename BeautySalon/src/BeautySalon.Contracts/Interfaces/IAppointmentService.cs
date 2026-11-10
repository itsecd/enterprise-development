using BeautySalon.Contracts.Dto;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Сервис управления записями на приём
/// </summary>
public interface IAppointmentService
{
    /// <summary>
    /// Получает список всех записей на приём
    /// </summary>
    public Task<IReadOnlyList<AppointmentDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает запись на приём по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    public Task<AppointmentDto> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создаёт запись на приём
    /// </summary>
    /// <param name="dto">Данные для создания</param>
    /// <returns>Созданный объект с назначенным идентификатором</returns>
    public Task<AppointmentDto> CreateAsync(AppointmentEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновляет запись на приём
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="dto">Новые данные</param>
    /// <returns>Обновлённый объект</returns>
    public Task<AppointmentDto> UpdateAsync(int id, AppointmentEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет запись на приём
    /// </summary>
    /// <param name="id">Идентификатор</param>
    public Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}