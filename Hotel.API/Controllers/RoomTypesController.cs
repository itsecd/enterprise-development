using Hotel.Application.Interfaces;
using Hotel.Contracts.DTO;
using Hotel.Contracts.Request;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

/// <summary>
/// Контроллер для управления типами комнат
/// </summary>
/// <param name="service">Сервис для работы с типами комнат</param>
[ApiController]
[Route("api/roomtypes")]
public class RoomTypesController(IRoomTypeService service) : ControllerBase
{
    /// <summary>
    /// Получение всех типов комнат
    /// </summary>
    /// <returns>Список типов комнат</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<RoomTypeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RoomTypeDto>>> GetAll() => Ok(await service.GetAllAsync());

    /// <summary>
    /// Получение типа комнаты по Id
    /// </summary>
    /// <param name="id">Id типа комнаты</param>
    /// <returns>Тип комнаты</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RoomTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoomTypeDto>> Get(int id) => Ok(await service.GetAsync(id));

    /// <summary>
    /// Создание нового типа комнаты
    /// </summary>
    /// <param name="request">Данные типа комнаты</param>
    /// <returns>Созданный тип комнаты</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RoomTypeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RoomTypeDto>> Create([FromBody] RoomTypeRequest request)
    {
        var result = await service.CreateAsync(request);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// Обновление типа комнаты
    /// </summary>
    /// <param name="id">Id типа комнаты</param>
    /// <param name="request">Новые данные типа комнаты</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] RoomTypeRequest request)
    {
        await service.UpdateAsync(id, request);
        return NoContent();
    }

    /// <summary>
    /// Удаление типа комнаты
    /// </summary>
    /// <param name="id">Id типа комнаты</param>
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
