using DispatchTransportControl.Api.DTO;
using DispatchTransportControl.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DispatchTransportControl.Api.Controller;

/// <summary>
/// Контроллер для транспортных средств
/// </summary>
[Route("api/vehicle")]
[ApiController]
public class VehicleController(IVehicleService service) : ControllerBase
{
    /// <summary>
    /// Получение всех транспортных средств
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<VehicleDto>> GetVehicles()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получение транспортного средства по id
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<VehicleDto> GetVehicle(int id)
    {
        var result = service.GetById(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Создание транспортного средства
    /// </summary>
    [HttpPost]
    public ActionResult<VehicleDto> CreateVehicle(VehicleCreateDto vehicle)
    {
        var result = service.Create(vehicle);
        return CreatedAtAction(nameof(GetVehicle), new { id = result.Id }, result);
    }

    /// <summary>
    /// Изменение существующего транспортного средства
    /// </summary>
    [HttpPut]
    public ActionResult<VehicleDto> UpdateVehicle(VehicleUpdateDto vehicle)
    {
        return Ok(service.Update(vehicle));
    }

    /// <summary>
    /// Удаление транспортного средства
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult DeleteVehicle(int id)
    {
        service.Delete(id);
        return Ok();
    }

    /// <summary>
    /// Получение суммарного времени поездок транспортного средства каждого типа и модели
    /// </summary>
    [HttpGet("get-total-trip-time-for-every-vehicle-type-and-model")]
    public ActionResult<IEnumerable<TripTimeForVehicleTypeAndModelDto>> GetTotalTripTimeForEveryVehicleTypeAndModel()
    {
        return Ok(service.GetTotalTripTimeForEveryVehicleTypeAndModel());
    }

    /// <summary>
    /// Получение информации о транспортных средствах, совершивших максимальное число поездок за указанный период
    /// </summary>
    [HttpPost("get-vehicles-with-max-trips-for-period")]
    public ActionResult<IEnumerable<TripTimeForVehicleTypeAndModelDto>>
        GetVehiclesWithMaxTripsForPeriod(TimePeriodDto dto)
    {
        return Ok(service.GetVehiclesWithMaxTripsForPeriod(dto.Start, dto.End));
    }
}