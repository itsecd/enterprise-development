using Microsoft.AspNetCore.Mvc;
using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;

namespace Museum.Api.Controllers;

/// <summary>Обрабатывает HTTP-запросы для работы с экскурсиями</summary>
[ApiController]
[Route("api/[controller]")]
public sealed class ToursController(
    ITourService tourService,
    ILogger<ToursController> logger) : ControllerBase
{
    /// <summary>Возвращает список всех экскурсий</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TourDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TourDto>>> GetAll()
    {
        var tours = await tourService.GetAllAsync();
        return Ok(tours);
    }

    /// <summary>Возвращает экскурсию по её идентификатору</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TourDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TourDto>> GetById(int id)
    {
        var tour = await tourService.GetByIdAsync(id);

        if (tour is null)
        {
            logger.LogWarning("Экскурсия {TourId} не найдена", id);
            return NotFound();
        }

        return Ok(tour);
    }

    /// <summary>Создаёт новую экскурсию</summary>
    [HttpPost]
    [ProducesResponseType(typeof(TourDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TourDto>> Create(TourEditDto dto)
    {
        logger.LogInformation("Создание экскурсии на {Date}", dto.Date);

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
            logger.LogWarning(exception, "Не удалось создать экскурсию");
            return BadRequest(exception.Message);
        }
    }

    /// <summary>Обновляет сведения об экскурсии</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, TourEditDto dto)
    {
        logger.LogInformation("Обновление экскурсии {TourId}", id);

        try
        {
            var updated = await tourService.UpdateAsync(id, dto);

            if (!updated)
            {
                logger.LogWarning("Экскурсия {TourId} для обновления не найдена", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Не удалось обновить экскурсию {TourId}", id);
            return BadRequest(exception.Message);
        }
    }

    /// <summary>Удаляет экскурсию, если на неё не оформлены билеты</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation("Удаление экскурсии {TourId}", id);

        try
        {
            var deleted = await tourService.DeleteAsync(id);

            if (!deleted)
            {
                logger.LogWarning("Экскурсия {TourId} для удаления не найдена", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (InvalidOperationException exception)
        {
            logger.LogWarning(exception, "Не удалось удалить экскурсию {TourId}", id);
            return Conflict(exception.Message);
        }
    }
}
