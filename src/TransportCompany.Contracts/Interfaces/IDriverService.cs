using TransportCompany.Contracts.Dto;

namespace TransportCompany.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с водителями транспортной компании
/// </summary>
public interface IDriverService
{
    /// <summary>
    /// Получить список всех водителей
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список водителей</returns>
    public Task<IEnumerable<DriverDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить водителя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор водителя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Водитель или null, если не найден</returns>
    public Task<DriverDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создать нового водителя
    /// </summary>
    /// <param name="dto">Данные водителя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданный водитель</returns>
    public Task<DriverDto> CreateAsync(DriverEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить данные водителя
    /// </summary>
    /// <param name="id">Идентификатор водителя</param>
    /// <param name="dto">Новые данные водителя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<DriverDto?> UpdateAsync(int id, DriverEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить водителя
    /// </summary>
    /// <param name="id">Идентификатор водителя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}