using Hotel.Application.Contracts.RoomType;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Hotel.WebAPI.Controllers;

/// <summary>
/// Контроллер для CRUD операций над типами комнат
/// </summary>
/// <param name="roomTypeService">Интерфейс службы</param>
[ApiController]
[Route("api/[controller]")]
public class RoomTypesController(IRoomTypeService roomTypeService) : ControllerBase
{
    /// <summary>
    /// Получение всех типов комнат
    /// </summary>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Коллекция типов комнат</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RoomTypeResponseDto>), 200)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await roomTypeService.GetAllAsync(ct));

    /// <summary>
    /// Получение информации  о типе комнат
    /// </summary>
    /// <param name="id">Id типа</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Тип комнат</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RoomTypeResponseDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await roomTypeService.GetByIdAsync(id, ct);
        return result is null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Создание типа комнат
    /// </summary>
    /// <param name="ct">Токен для отмены операции</param>
    /// <param name="dto">DTO для создания типа</param>
    /// <returns>Созданный тип кимнат</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RoomTypeResponseDto), 201)]
    public async Task<IActionResult> Create(RoomTypeCreateUpdateDto dto, CancellationToken ct)
    {
        var created = await roomTypeService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Получение информации о типе комнат
    /// </summary>
    /// <param name="id">Id типа</param>
    /// <param name="dto">DTO для обновления типа</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>обновлённый тип комнат</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(RoomTypeResponseDto), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, RoomTypeCreateUpdateDto dto, CancellationToken ct)
    {
        var updated = await roomTypeService.UpdateAsync(id, dto, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// Удаления типа комнат
    /// </summary>
    /// <param name="id">Id типа</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>статус-код</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await roomTypeService.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}