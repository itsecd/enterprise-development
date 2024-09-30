using DispatchTransportControl.Domain;

namespace DispatchTransportControl.Api.Repository;

/// <summary>
/// Интерфейс репозитория для модели транспортного средства
/// </summary>
public interface IVehicleModelRepository : IRepository<VehicleModel, int>;