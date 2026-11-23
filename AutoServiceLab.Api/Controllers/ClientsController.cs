using AutoServiceLab.Application.Contracts.Dtos.Cars;
using AutoServiceLab.Application.Contracts.Dtos.Clients;
using AutoServiceLab.Application.Contracts.Dtos.ServiceOrders;
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
    /// Получить список всех клиентов
    /// </summary>
    [HttpGet]
    public override async Task<ActionResult<IList<ClientDto>>> GetAll() => await base.GetAll();

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public override async Task<ActionResult<ClientDto>> Get(int id) => await base.Get(id);

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    [HttpPost]
    public override async Task<ActionResult<ClientDto>> Post(ClientCreateUpdateDto dto) => await base.Post(dto);

    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    [HttpPut("{id}")]
    public override async Task<ActionResult<ClientDto>> Update(ClientCreateUpdateDto dto, int id) => await base.Update(dto, id);

    /// <summary>
    /// Удалить клиента
    /// </summary>
    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete(int id) => await base.Delete(id);

    /// <summary>
    /// Получить автомобили клиента
    /// </summary>
    [HttpGet("{id}/cars")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetClientCars(int id)
    {
        logger.LogInformation("Запрос на получение машин клиента {Id}", id);
        return Ok(await carService.GetCarsByClientId(id));
    }

    /// <summary>
    /// Получить заказы клиента
    /// </summary>
    [HttpGet("{id}/orders")]
    [ProducesResponseType(typeof(IList<ServiceOrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<ServiceOrderDto>>> GetClientOrders(int id)
    {
        logger.LogInformation("Запрос на получение заказов клиента {Id}", id);
        return Ok(await service.GetOrdersByClient(id));
    }
}
