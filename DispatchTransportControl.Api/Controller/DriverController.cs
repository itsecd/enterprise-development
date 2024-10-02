using DispatchTransportControl.Api.DTO;
using DispatchTransportControl.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DispatchTransportControl.Api.Controller;

/// <summary>
///     Контроллер для водителей
/// </summary>
[Route("api/driver")]
[ApiController]
public class DriverController(IDriverService service) : ControllerBase
{
    /// <summary>
    ///     Получение всех водителей
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<DriverDto>> GetDrivers()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    ///     Получение водителя по id
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<DriverDto> GetDriver(int id)
    {
        var result = service.GetById(id);
        if (result == null) return NotFound();

        return Ok(result);
    }

    /// <summary>
    ///     Создание водителя
    /// </summary>
    [HttpPost]
    public ActionResult<DriverDto> CreateDriver(DriverCreateDto driver)
    {
        var result = service.Create(driver);
        return CreatedAtAction(nameof(GetDriver), new { id = result.Id }, result);
    }

    /// <summary>
    ///     Изменение существующего водителя
    /// </summary>
    [HttpPut]
    public ActionResult<DriverDto> UpdateDriver(DriverDto driver)
    {
        return Ok(service.Update(driver));
    }

    /// <summary>
    ///     Удаление водителя
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult DeleteDriver(int id)
    {
        service.Delete(id);
        return Ok();
    }

    /// <summary>
    ///     Получение всех водителей, совершивших поездки за заданный период
    /// </summary>
    [HttpPost("/get-all-by-period")]
    public ActionResult<IEnumerable<DriverDto>> GetDriversByPeriod(TimePeriodDto dto)
    {
        return Ok(service.GetAllByPeriod(dto));
    }

    /// <summary>
    ///     Получение топ 5 водителей по совершенному количеству поездок
    /// </summary>
    [HttpGet("/get-top-5-drivers-by-trip-count")]
    public ActionResult<IEnumerable<DriverTripCountDto>> GetTop5DriversByTripCount()
    {
        return Ok(service.GetTop5DriversByTripCount());
    }

    /// <summary>
    ///     Получение информации о количестве поездок, среднем времени и максимальном времени поездки для каждого водителя
    /// </summary>
    [HttpGet("/get-drivers-trip-stats")]
    public ActionResult<IEnumerable<DriverTripStatsDto>> GetDriverTripStats()
    {
        return Ok(service.GetDriverTripStats());
    }
}