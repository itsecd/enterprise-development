using Hotel.Contracts.Dto;
using Hotel.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Controllers;

/// <summary>Работает с гостиничными номерами и аналитикой бронирований.</summary>
/// <param name="service">Сервис номеров.</param>
[ApiController]
[Route("api/rooms")]
public class RoomsController(IRoomService service) : ControllerBase
{
    private readonly IRoomService _service = service;

    /// <summary>Возвращает все номера гостиницы.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<RoomDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RoomDto>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    /// <summary>Возвращает номер по идентификатору.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoomDto>> GetById(int id)
    {
        var room = await _service.GetByIdAsync(id);
        return room is null ? NotFound() : Ok(room);
    }

    /// <summary>Возвращает номера, в которых проживают клиенты на указанную дату.</summary>
    /// <param name="date">Дата проверки. По умолчанию используется текущая дата сервера.</param>
    [HttpGet("current-bookings")]
    [ProducesResponseType(typeof(IReadOnlyList<RoomDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RoomDto>>> GetCurrentBookings([FromQuery] DateTime? date)
    {
        return Ok(await _service.GetCurrentBookingsAsync((date ?? DateTime.Today).Date));
    }

    /// <summary>Возвращает топ-5 наиболее часто бронируемых номеров.</summary>
    [HttpGet("most-booked")]
    [ProducesResponseType(typeof(IReadOnlyList<RoomBookingCountDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RoomBookingCountDto>>> GetMostBooked()
    {
        return Ok(await _service.GetMostBookedAsync());
    }

    /// <summary>Возвращает число бронирований для каждого номера, включая ноль.</summary>
    [HttpGet("booking-counts")]
    [ProducesResponseType(typeof(IReadOnlyList<RoomBookingCountDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RoomBookingCountDto>>> GetBookingCounts()
    {
        return Ok(await _service.GetBookingCountsAsync());
    }

    /// <summary>Создаёт номер с существующим типом.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(RoomDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<RoomDto>> Create([FromBody] RoomEditDto request)
    {
        var room = await _service.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    /// <summary>Изменяет данные номера.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(int id, [FromBody] RoomEditDto request)
    {
        return await _service.UpdateAsync(id, request) ? NoContent() : NotFound();
    }

    /// <summary>Удаляет номер, если для него нет бронирований.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(int id)
    {
        return await _service.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
