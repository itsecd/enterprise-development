using Hotel.Application.Interfaces;
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
    public async Task<IActionResult> GetAll() => Ok(await service.GetAllAsync());

    /// <summary>
    /// Получение клиента по Id
    /// </summary>
    /// <param name="id">Id клиента</param>
    /// <returns>Клиент</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id) => Ok(await service.GetAsync(id));

    /// <summary>
    /// Создание нового клиента
    /// </summary>
    /// <param name="request">Данные клиента</param>
    /// <returns>Созданный клиент</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ClientRequest request)
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
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}