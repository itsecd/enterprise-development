using Hotel.Application.Interfaces;
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
    public async Task<IActionResult> GetAll() => Ok(await service.GetAllAsync());

    /// <summary>
    /// Получение типа комнаты по Id
    /// </summary>
    /// <param name="id">Id типа комнаты</param>
    /// <returns>Тип комнаты</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id) => Ok(await service.GetAsync(id));

    /// <summary>
    /// Создание нового типа комнаты
    /// </summary>
    /// <param name="request">данные о типе комнаты</param>
    /// <returns>Созданный тип комнаты</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomTypeRequest request)
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
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}