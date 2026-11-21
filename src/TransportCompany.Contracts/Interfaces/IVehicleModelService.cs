using TransportCompany.Contracts.Dto;

namespace TransportCompany.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с моделями транспортных средств
/// </summary>
public interface IVehicleModelService
{
    /// <summary>
    /// Получить список всех моделей транспортных средств
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список моделей ТС</returns>
    public Task<IEnumerable<VehicleModelDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить модель транспортного средства по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор модели ТС</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель ТС или null, если не найдена</returns>
    public Task<VehicleModelDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создать новую модель транспортного средства
    /// </summary>
    /// <param name="dto">Данные модели ТС</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданная модель ТС</returns>
    public Task<VehicleModelDto> CreateAsync(VehicleModelEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить данные модели транспортного средства
    /// </summary>
    /// <param name="id">Идентификатор модели ТС</param>
    /// <param name="dto">Новые данные модели ТС</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<VehicleModelDto?> UpdateAsync(int id, VehicleModelEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить модель транспортного средства
    /// </summary>
    /// <param name="id">Идентификатор модели ТС</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}