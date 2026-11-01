using AutoServiceLab.Application.Contracts.DTOs.Cars;
using AutoServiceLab.Application.Contracts.DTOs.Clients;
using AutoServiceLab.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoServiceLab.Api.Controllers;

/// <summary>
/// Контроллер для работы с клиентами
/// </summary>
public class ClientsController(
    IClientService service,
    ICarService carService,
    ILogger<ClientsController> logger)
    : CrudControllerBase<ClientDto, ClientCreateUpdateDto, int>(service, logger)
{
    /// <summary>
    /// Получить автомобили клиента
    /// </summary>
    [HttpGet("{id}/cars")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<CarDto>> GetClientCars(int id)
    {
        logger.LogInformation("Запрос на получение машин клиента {Id}", id);
        return Ok(carService.GetCarsByClientId(id));
    }
}
