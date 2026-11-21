using TransportCompany.Contracts.Dto;

namespace TransportCompany.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с транспортными средствами
/// </summary>
public interface IVehicleService
{
    /// <summary>
    /// Получить список всех транспортных средств
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список транспортных средств</returns>
    public Task<IEnumerable<VehicleDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить транспортное средство по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор транспортного средства</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Транспортное средство или null, если не найдено</returns>
    public Task<VehicleDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создать новое транспортное средство
    /// </summary>
    /// <param name="dto">Данные транспортного средства</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданное транспортное средство</returns>
    public Task<VehicleDto> CreateAsync(VehicleEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить данные транспортного средства
    /// </summary>
    /// <param name="id">Идентификатор транспортного средства</param>
    /// <param name="dto">Новые данные транспортного средства</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<VehicleDto?> UpdateAsync(int id, VehicleEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить транспортное средство
    /// </summary>
    /// <param name="id">Идентификатор транспортного средства</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}