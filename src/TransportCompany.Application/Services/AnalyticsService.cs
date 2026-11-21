using TransportCompany.Application.Mappers;
using TransportCompany.Contracts.Dto;
using TransportCompany.Contracts.Interfaces;
using TransportCompany.Domain.Interfaces;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис аналитики транспортной компании
/// </summary>
public class AnalyticsService(IAnalyticsRepository analyticsRepository) : IAnalyticsService
{
    /// <summary>
    /// Получить клиентов, перевозивших грузы на ТС указанной модели
    /// </summary>
    public async Task<IEnumerable<ClientDto>> GetClientsByVehicleModelAsync(int modelId, CancellationToken cancellationToken = default)
    {
        var clients = await analyticsRepository.GetClientsByVehicleModelAsync(modelId, cancellationToken);
        return clients.Select(c => c.ToDto());
    }

    /// <summary>
    /// Получить транспортные средства, находящиеся в рейсе
    /// </summary>
    public async Task<IEnumerable<VehicleDto>> GetVehiclesInTransitAsync(CancellationToken cancellationToken = default)
    {
        var vehicles = await analyticsRepository.GetVehiclesInTransitAsync(cancellationToken);
        return vehicles.Select(v => v.ToDto());
    }

    /// <summary>
    /// Получить топ-5 водителей по количеству рейсов
    /// </summary>
    public async Task<IEnumerable<TopDriverDto>> GetTop5DriversAsync(CancellationToken cancellationToken = default)
    {
        var topDrivers = await analyticsRepository.GetTopDriversAsync(5, cancellationToken);
        return topDrivers.Select(d => new TopDriverDto
        {
            DriverId = d.DriverId,
            DriverName = d.DriverName,
            TripCount = d.TripCount
        });
    }

    /// <summary>
    /// Получить количество рейсов для каждого транспортного средства
    /// </summary>
    public async Task<IEnumerable<VehicleTripCountDto>> GetTripCountForVehiclesAsync(CancellationToken cancellationToken = default)
    {
        var tripCounts = await analyticsRepository.GetTripCountsAsync(cancellationToken);
        return tripCounts.Select(v => new VehicleTripCountDto
        {
            LicensePlate = v.LicensePlate,
            TripCount = v.TripCount
        });
    }

    /// <summary>
    /// Получить топ-5 клиентов по суммарной стоимости перевозок
    /// </summary>
    public async Task<IEnumerable<TopClientDto>> GetTop5ClientsByCostAsync(CancellationToken cancellationToken = default)
    {
        var topClients = await analyticsRepository.GetTopClientsByCostAsync(5, cancellationToken);
        return topClients.Select(c => new TopClientDto
        {
            ClientName = c.ClientName,
            TotalCost = c.TotalCost
        });
    }
}