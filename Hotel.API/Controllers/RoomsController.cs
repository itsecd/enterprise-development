using Hotel.Application.Interfaces;
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
    public async Task<IActionResult> GetAll() => Ok(await service.GetAllAsync());

    /// <summary>
    /// Получение комнаты по Id
    /// </summary>
    /// <param name="id">Id комнаты</param>
    /// <returns>Комната</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id) => Ok(await service.GetAsync(id));

    /// <summary>
    /// Создание новой комнаты
    /// </summary>
    /// <param name="request">Данные комнаты</param>
    /// <returns>Созданная комната</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomRequest request)
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
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}