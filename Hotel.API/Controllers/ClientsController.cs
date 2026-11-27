using Hotel.Application.Interfaces;
using Hotel.Contracts.DTO;
using Hotel.Contracts.Request;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

/// <summary>
/// Контроллер для управления клиентами
/// </summary>
/// <param name="service">Сервис для работы с клиентами</param>
[ApiController]
[Route("api/clients")]
public class ClientsController(IClientService service) : ControllerBase
{
    /// <summary>
    /// Получение всех клиентов
    /// </summary>
    /// <returns>Список клиентов</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetAll() => Ok(await service.GetAllAsync());

    /// <summary>
    /// Получение клиента по Id
    /// </summary>
    /// <param name="id">Id клиента</param>
    /// <returns>Клиент</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientDto>> Get(int id) => Ok(await service.GetAsync(id));

    /// <summary>
    /// Создание нового клиента
    /// </summary>
    /// <param name="request">Данные клиента</param>
    /// <returns>Созданный клиент</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientDto>> Create([FromBody] ClientRequest request)
    {
        var result = await service.CreateAsync(request);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// Обновление клиента
    /// </summary>
    /// <param name="id">Id клиента</param>
    /// <param name="request">Новые данные клиента</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] ClientRequest request)
    {
        await service.UpdateAsync(id, request);
        return NoContent();
    }

    /// <summary>
    /// Удаление клиента
    /// </summary>
    /// <param name="id">Id клиента</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}
