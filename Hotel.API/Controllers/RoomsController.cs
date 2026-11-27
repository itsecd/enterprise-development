using Hotel.Application.Interfaces;
using Hotel.Contracts.DTO;
using Hotel.Contracts.Request;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

/// <summary>
/// Контроллер для управления комнатами
/// </summary>
/// <param name="service">Сервис для работы с комнатами</param>
[ApiController]
[Route("api/rooms")]
public class RoomsController(IRoomService service) : ControllerBase
{
    /// <summary>
    /// Получение всех комнат
    /// </summary>
    /// <returns>Список комнат</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<RoomDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RoomDto>>> GetAll() => Ok(await service.GetAllAsync());

    /// <summary>
    /// Получение комнаты по Id
    /// </summary>
    /// <param name="id">Id комнаты</param>
    /// <returns>Комната</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoomDto>> Get(int id) => Ok(await service.GetAsync(id));

    /// <summary>
    /// Создание новой комнаты
    /// </summary>
    /// <param name="request">Данные комнаты</param>
    /// <returns>Созданная комната</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RoomDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoomDto>> Create([FromBody] RoomRequest request)
    {
        var result = await service.CreateAsync(request);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// Обновление комнаты
    /// </summary>
    /// <param name="id">Id комнаты</param>
    /// <param name="request">Новые данные комнаты</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] RoomRequest request)
    {
        await service.UpdateAsync(id, request);
        return NoContent();
    }

    /// <summary>
    /// Удаление комнаты
    /// </summary>
    /// <param name="id">Id комнаты</param>
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
