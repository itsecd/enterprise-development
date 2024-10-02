using DispatchTransportControl.Api.DTO;

namespace DispatchTransportControl.Api.Service;

/// <summary>
///     Интерфейс сервиса для работы с водителями
/// </summary>
public interface IDriverService
{
    /// <summary>
    ///     Получение всех водителей
    /// </summary>
    public IEnumerable<DriverDto> GetAll();

    /// <summary>
    ///     Получение водителя по id
    /// </summary>
    public DriverDto? GetById(int id);

    /// <summary>
    ///     Создание водителя
    /// </summary>
    public DriverDto Create(DriverCreateDto dto);

    /// <summary>
    ///     Изменение существующего водителя
    /// </summary>
    public DriverDto Update(DriverDto dto);

    /// <summary>
    ///     Удаление водителя
    /// </summary>
    public void Delete(int id);

    /// <summary>
    ///     Получение всех водителей, совершивших поездки за заданный период
    /// </summary>
    public IEnumerable<DriverDto> GetAllByPeriod(TimePeriodDto dto);

    /// <summary>
    ///     Получение топ 5 водителей по совершенному количеству поездок
    /// </summary>
    public IEnumerable<DriverTripCountDto> GetTop5DriversByTripCount();

    /// <summary>
    ///     Получение информации о количестве поездок, среднем времени и максимальном времени поездки для каждого водителя
    /// </summary>
    public IEnumerable<DriverTripStatsDto> GetDriverTripStats();
}