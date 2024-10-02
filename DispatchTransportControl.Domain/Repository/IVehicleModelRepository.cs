using DispatchTransportControl.Domain.Entity;

namespace DispatchTransportControl.Domain.Repository;

/// <summary>
///     Интерфейс репозитория для модели транспортного средства
/// </summary>
public interface IVehicleModelRepository : IRepository<VehicleModel, int>;