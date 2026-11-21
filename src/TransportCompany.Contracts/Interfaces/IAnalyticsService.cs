using TransportCompany.Contracts.Dto;

namespace TransportCompany.Contracts.Interfaces;

/// <summary>
/// Сервис аналитики транспортной компании
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получить клиентов, перевозивших грузы на ТС указанной модели
    /// </summary>
    /// <param name="modelId">Идентификатор модели ТС</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Клиенты, отсортированные по имени</returns>
    public Task<IEnumerable<ClientDto>> GetClientsByVehicleModelAsync(int modelId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить транспортные средства, находящиеся в рейсе
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Транспортные средства в пути</returns>
    public Task<IEnumerable<VehicleDto>> GetVehiclesInTransitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить топ-5 водителей по количеству рейсов
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Топ-5 водителей</returns>
    public Task<IEnumerable<TopDriverDto>> GetTop5DriversAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить количество рейсов для каждого транспортного средства
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество рейсов по транспортным средствам</returns>
    public Task<IEnumerable<VehicleTripCountDto>> GetTripCountForVehiclesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить топ-5 клиентов по суммарной стоимости перевозок
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Топ-5 клиентов</returns>
    public Task<IEnumerable<TopClientDto>> GetTop5ClientsByCostAsync(CancellationToken cancellationToken = default);
}