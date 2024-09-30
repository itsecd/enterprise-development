using DispatchTransportControl.Api.DTO;
using DispatchTransportControl.Api.Service;
using Microsoft.AspNetCore.Mvc;

namespace DispatchTransportControl.Api.Controller;

/// <summary>
/// Контроллер для моделей транспортных средств
/// </summary>
[Route("api/vehicle-model")]
[ApiController]
public class VehicleModelController(IVehicleModelService service) : ControllerBase
{
    /// <summary>
    /// Получение всех моделей транспортных средств
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<VehicleModelDto>> GetVehicleModels()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получение модели транспортного средства по id
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<VehicleModelDto> GetVehicleModel(int id)
    {
        var result = service.GetById(id);
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Создание модели транспортного средства
    /// </summary>
    [HttpPost]
    public ActionResult<VehicleModelDto> CreateVehicleModel(VehicleModelCreateDto vehicleModel)
    {
        var result = service.Create(vehicleModel);
        return CreatedAtAction(nameof(GetVehicleModel), new { id = result.Id }, result);
    }

    /// <summary>
    /// Изменение существующей модели транспортного средства
    /// </summary>
    [HttpPut]
    public ActionResult<VehicleModelDto> UpdateVehicleModel(VehicleModelDto vehicleModel)
    {
        return Ok(service.Update(vehicleModel));
    }

    /// <summary>
    /// Удаление модели транспортного средства
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult DeleteVehicleModel(int id)
    {
        service.Delete(id);
        return Ok();
    }
}