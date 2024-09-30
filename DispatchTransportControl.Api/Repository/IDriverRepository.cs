using DispatchTransportControl.Domain;

namespace DispatchTransportControl.Api.Repository;

/// <summary>
/// Интерфейс репозитория для водителей
/// </summary>
public interface IDriverRepository : IRepository<Driver, int>
{
    /// <summary>
    /// Получение всех водителей, совершивших поездки за заданный период
    /// </summary>
    public IEnumerable<Driver> GetAllByPeriod(DateTime start, DateTime end);

    /// <summary>
    /// Получение топ 5 водителей по совершенному количеству поездок
    /// </summary>
    public IEnumerable<(Driver Driver, int TripCount)> GetTop5DriversByTripCount();

    /// <summary>
    /// Получение информации о количестве поездок, среднем времени и максимальном времени поездки для каждого водителя
    /// </summary>
    public Dictionary<Driver, (int TripCount, double AvgTime, double MaxTime)> GetDriverTripStats();
}