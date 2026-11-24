using Hotel.Contracts.Dto;
using Hotel.Contracts.Interfaces;
using Hotel.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers;

/// <summary>Работает с клиентами гостиницы и аналитикой их проживания.</summary>
/// <param name="service">Сервис клиентов.</param>
[ApiController]
[Route("api/clients")]
public class ClientsController(IClientService service) : ControllerBase
{
    private readonly IClientService _service = service;

    /// <summary>Возвращает всех клиентов.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    /// <summary>Возвращает клиента по идентификатору.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        var client = await _service.GetByIdAsync(id);
        return client is null ? NotFound() : Ok(client);
    }

    /// <summary>Выводит клиентов, проживавших в номерах выбранной категории, по ФИО.</summary>
    /// <param name="category">Категория номера.</param>
    [HttpGet("by-room-type/{category}")]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetByRoomCategory(RoomCategory category)
    {
        return Ok(await _service.GetByRoomCategoryAsync(category));
    }

    /// <summary>Возвращает топ-5 клиентов по суммарной стоимости проживания.</summary>
    [HttpGet("top-spenders")]
    [ProducesResponseType(typeof(IReadOnlyList<TopClientSpendingDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TopClientSpendingDto>>> GetTopSpenders()
    {
        return Ok(await _service.GetTopSpendersAsync());
    }

    /// <summary>Создаёт клиента.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ClientDto>> Create([FromBody] ClientEditDto request)
    {
        var client = await _service.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
    }

    /// <summary>Изменяет данные клиента.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(int id, [FromBody] ClientEditDto request)
    {
        return await _service.UpdateAsync(id, request) ? NoContent() : NotFound();
    }

    /// <summary>Удаляет клиента, если у него нет бронирований.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(int id)
    {
        return await _service.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
