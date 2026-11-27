using Hotel.Application.Interfaces;
using Hotel.Contracts.DTO;
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
    [ProducesResponseType(typeof(IReadOnlyList<BookingDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<BookingDto>>> GetAll() => Ok(await service.GetAllAsync());

    /// <summary>
    /// Получение бронирования по Id
    /// </summary>
    /// <param name="id">Id бронирования</param>
    /// <returns>Бронирование</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookingDto>> Get(int id) => Ok(await service.GetAsync(id));

    /// <summary>
    /// Создание нового бронирования
    /// </summary>
    /// <param name="request">Данные бронирования</param>
    /// <returns>Созданное бронирование</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BookingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookingDto>> Create([FromBody] BookingRequest request)
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}
