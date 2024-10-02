using DispatchTransportControl.Domain.Entity;

namespace DispatchTransportControl.Domain.Repository;

/// <summary>
///     Интерфейс репозитория для транспортных средств
/// </summary>
public interface IVehicleRepository : IRepository<Vehicle, int>
{
    /// <summary>
    ///     Получение суммарного времени поездок транспортного средства каждого типа и модели
    /// </summary>
    public Dictionary<(VehicleType VehicleType, VehicleModel VehicleModel), double>
        GetTotalTripTimeForEveryVehicleTypeAndModel();

    /// <summary>
    ///     Получение информации о транспортных средствах, совершивших максимальное число поездок за указанный период
    /// </summary>
    public IEnumerable<(Vehicle Vehicle, int TripCount)> GetVehiclesWithMaxTripsForPeriod(DateTime start, DateTime end);
}