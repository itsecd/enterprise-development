using Microsoft.AspNetCore.Mvc;
using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;

namespace Museum.Api.Controllers;

/// <summary>Обрабатывает HTTP-запросы для работы с экскурсиями</summary>
[ApiController]
[Route("api/[controller]")]
public sealed class ToursController(ITourService tourService) : ControllerBase
{
    /// <summary>Возвращает список всех экскурсий</summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TourDto>>> GetAll()
    {
        var tours = await tourService.GetAllAsync();
        return Ok(tours);
    }

    /// <summary>Возвращает экскурсию по её идентификатору</summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TourDto>> GetById(int id)
    {
        var tour = await tourService.GetByIdAsync(id);

        if (tour is null)
            return NotFound();

        return Ok(tour);
    }

    /// <summary>Создаёт новую экскурсию</summary>
    [HttpPost]
    public async Task<ActionResult<TourDto>> Create(TourEditDto dto)
    {
        try
        {
            var tour = await tourService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = tour.Id },
                tour);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    /// <summary>Обновляет сведения об экскурсии</summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TourEditDto dto)
    {
        try
        {
            var updated = await tourService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    /// <summary>Удаляет экскурсию, если на неё не оформлены билеты</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await tourService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(exception.Message);
        }
    }
}
