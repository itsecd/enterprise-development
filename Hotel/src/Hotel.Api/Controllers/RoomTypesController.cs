using Hotel.Contracts.Dto;
using Hotel.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers;

/// <summary>Работает со справочником типов номеров.</summary>
/// <param name="service">Сервис типов номеров.</param>
[ApiController]
[Route("api/room-types")]
public class RoomTypesController(IRoomTypeService service) : ControllerBase
{
    private readonly IRoomTypeService _service = service;

    /// <summary>Возвращает все типы номеров.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<RoomTypeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RoomTypeDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    /// <summary>Возвращает тип номера по идентификатору.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RoomTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoomTypeDto>> GetById(int id)
    {
        var roomType = await _service.GetByIdAsync(id);
        return roomType is null ? NotFound() : Ok(roomType);
    }

    /// <summary>Создаёт тип номера.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(RoomTypeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RoomTypeDto>> Create([FromBody] RoomTypeEditDto request)
    {
        var roomType = await _service.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = roomType.Id }, roomType);
    }

    /// <summary>Изменяет характеристики и стоимость типа номера.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] RoomTypeEditDto request)
    {
        return await _service.UpdateAsync(id, request) ? NoContent() : NotFound();
    }

    /// <summary>Удаляет тип номера, если он не используется номерами гостиницы.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(int id)
    {
        return await _service.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
