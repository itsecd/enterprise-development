using DispatchTransportControl.Api.DTO;
using DispatchTransportControl.Domain;

namespace DispatchTransportControl.Api.Service;

/// <summary>
/// Интерфейс сервиса для работы с транспортными средствами
/// </summary>
public interface IVehicleService
{
    /// <summary>
    /// Получение всех транспортных средств
    /// </summary>
    public IEnumerable<VehicleDto> GetAll();

    /// <summary>
    /// Получение транспортного средства по id
    /// </summary>
    public VehicleDto? GetById(int id);

    /// <summary>
    /// Создание транспортного средства
    /// </summary>
    public VehicleDto Create(VehicleCreateDto dto);

    /// <summary>
    /// Изменение существующего транспортного средства
    /// </summary>
    public VehicleDto Update(VehicleUpdateDto dto);

    /// <summary>
    /// Удаление транспортного средства
    /// </summary>
    public void Delete(int id);

    /// <summary>
    /// Получение суммарного времени поездок транспортного средства каждого типа и модели
    /// </summary>
    public IEnumerable<TripTimeForVehicleTypeAndModelDto> GetTotalTripTimeForEveryVehicleTypeAndModel();

    /// <summary>
    /// Получение информации о транспортных средствах, совершивших максимальное число поездок за указанный период
    /// </summary>
    public IEnumerable<VehicleWithTripCountDto> GetVehiclesWithMaxTripsForPeriod(DateTime start, DateTime end);
}