using Hotel.Application.Contracts.Room;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebAPI.Controllers;

/// <summary>
/// Контроллер для CRUD операций над номерами
/// </summary>
/// <param name="roomService">Интерфейс службы</param>
[ApiController]
[Route("api/[controller]")]
public class RoomsController(IRoomService roomService) : ControllerBase
{
    /// <summary>
    /// Получение всех номеров
    /// </summary>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Коллекция номеров</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await roomService.GetAllAsync(ct));
    
    /// <summary>
    /// Получение информации о номере
    /// </summary>
    /// <param name="id">Id номера</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Номер</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await roomService.GetByIdAsync(id, ct);
        return result is null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Создание номера
    /// </summary>
    /// <param name="dto">DTO для создания номеров</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Созданный номер</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> Create(RoomCreateUpdateDto dto, CancellationToken ct)
    {
        var created = await roomService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    /// <summary>
    /// Обновление информации о номере
    /// </summary>
    /// <param name="id">Id комнаты</param>
    /// <param name="dto">DTO для обновления номеров</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Обновлённый номер</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, RoomCreateUpdateDto dto, CancellationToken ct)
    {
        var updated = await roomService.UpdateAsync(id, dto, ct);
        return updated is null ? NotFound() : Ok(updated);
    }
    
    /// <summary>
    /// Удаление номера
    /// </summary>
    /// <param name="id">Id номера</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Статус-код</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await roomService.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}