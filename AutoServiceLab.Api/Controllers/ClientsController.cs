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
    public override ActionResult<IList<ClientDto>> GetAll() => base.GetAll();

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public override ActionResult<ClientDto> Get(int id) => base.Get(id);

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    [HttpPost]
    public override ActionResult<ClientDto> Post(ClientCreateUpdateDto dto) => base.Post(dto);

    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    [HttpPut("{id}")]
    public override ActionResult<ClientDto> Update(ClientCreateUpdateDto dto, int id) => base.Update(dto, id);

    /// <summary>
    /// Удалить клиента
    /// </summary>
    [HttpDelete("{id}")]
    public override IActionResult Delete(int id) => base.Delete(id);

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

    /// <summary>
    /// Получить заказы клиента
    /// </summary>
    [HttpGet("{id}/orders")]
    [ProducesResponseType(typeof(IList<ServiceOrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<ServiceOrderDto>> GetClientOrders(int id)
    {
        logger.LogInformation("Запрос на получение заказов клиента {Id}", id);
        return Ok(service.GetOrdersByClient(id));
    }
}
