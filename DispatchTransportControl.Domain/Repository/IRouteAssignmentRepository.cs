using DispatchTransportControl.Domain.Entity;

namespace DispatchTransportControl.Domain.Repository;

/// <summary>
///     Интерфейс репозитория для назначений водителей и транспортных средств на маршруты
/// </summary>
public interface IRouteAssignmentRepository : IRepository<RouteAssignment, int>;