using TransportCompany.Contracts.Dto;

namespace TransportCompany.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с рейсами транспортной компании
/// </summary>
public interface ITripService
{
    /// <summary>
    /// Получить список всех рейсов
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список рейсов</returns>
    public Task<IEnumerable<TripDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить рейс по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор рейса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Рейс или null, если не найден</returns>
    public Task<TripDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создать новый рейс
    /// </summary>
    /// <param name="dto">Данные рейса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданный рейс</returns>
    public Task<TripDto> CreateAsync(TripEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновить данные рейса
    /// </summary>
    /// <param name="id">Идентификатор рейса</param>
    /// <param name="dto">Новые данные рейса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<TripDto?> UpdateAsync(int id, TripEditDto dto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить рейс
    /// </summary>
    /// <param name="id">Идентификатор рейса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}