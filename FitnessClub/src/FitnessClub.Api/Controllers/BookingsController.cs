using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Управление бронями
/// </summary>
[ApiController]
[Route("api/bookings")]
public class BookingsController(ICrudService<BookingDto, BookingEditDto> service) : ControllerBase
{
    /// <summary>
    /// Получить все брони
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BookingDto>>> GetAll() =>
        Ok(await service.GetAllAsync());
    /// <summary>
    /// Получить бронь по Id
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookingDto>> GetById(int id)
    {
        var booking = await service.GetByIdAsync(id);
        return booking is null ? NotFound() : Ok(booking);
    }
    /// <summary>
    /// Создать бронь
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<BookingDto>> Create(BookingEditDto dto)
    {
        var created = await service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    /// <summary>
    /// Изменить бронь
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<BookingDto>> Update(int id, BookingEditDto dto)
    {
        var updated = await service.UpdateAsync(id, dto);
        return updated is null ? NotFound() : Ok(updated);
    }
    /// <summary>
    /// Удалить бронь
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await service.DeleteAsync(id) ? NoContent() : NotFound();
}