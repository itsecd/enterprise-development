using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Models;

namespace TransportCompany.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для выполнения аналитических запросов на уровне БД
/// </summary>
public interface IAnalyticsRepository
{
    public Task<IReadOnlyList<Client>> GetClientsByVehicleModelAsync(int modelId, CancellationToken cancellationToken = default);
    public Task<IReadOnlyList<Vehicle>> GetVehiclesInTransitAsync(CancellationToken cancellationToken = default);
    public Task<IReadOnlyList<DriverTripCount>> GetTopDriversAsync(int take, CancellationToken cancellationToken = default);
    public Task<IReadOnlyList<VehicleTripCount>> GetTripCountsAsync(CancellationToken cancellationToken = default);
    public Task<IReadOnlyList<ClientTripCost>> GetTopClientsByCostAsync(int take, CancellationToken cancellationToken = default);
}