using Hotel.Application.Interfaces;
using Hotel.Contracts.Request;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

/// <summary>
/// Контроллер для управления бронированиями
/// </summary>
/// <param name="service">Сервис для работы с бронированиями</param>
[ApiController]
[Route("api/bookings")]
public class BookingsController(IBookingService service) : ControllerBase
{
    /// <summary>
    /// Получение всех бронирований
    /// </summary>
    /// <returns>Список бронирований</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await service.GetAllAsync());

    /// <summary>
    /// Получение бронирования по Id
    /// </summary>
    /// <param name="id">Id бронирования</param>
    /// <returns>Бронирование</returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id) => Ok(await service.GetAsync(id));

    /// <summary>
    /// Создание нового бронирования
    /// </summary>
    /// <param name="request">Данные бронирования</param>
    /// <returns>Созданное бронирование</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BookingRequest request)
    {
        var result = await service.CreateAsync(request);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    /// <summary>
    /// Обновление бронирования
    /// </summary>
    /// <param name="id">Id бронирования</param>
    /// <param name="request">Новые данные бронирования</param>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] BookingRequest request)
    {
        await service.UpdateAsync(id, request);
        return NoContent();
    }

    /// <summary>
    /// Удаление бронирования
    /// </summary>
    /// <param name="id">Id бронирования</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}