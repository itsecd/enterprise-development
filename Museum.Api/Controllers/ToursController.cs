using Microsoft.AspNetCore.Mvc;
using Museum.Application.Repositories;
using Museum.Contracts;

namespace Museum.Api.Controllers;

/// <summary>
/// REST API для экскурсий.
/// </summary>
[ApiController]
[Route("api/tours")]
public class ToursController(ToursRepository repository, ILogger<ToursController> logger) : ControllerBase
{
    /// <summary>Возвращает экскурсии.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TourResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<TourResponse>> GetAll()
    {
        try
        {
            return Ok(repository.GetAll());
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: получение списка экскурсий");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Возвращает экскурсию по идентификатору.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TourResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TourResponse> GetById(int id)
    {
        try
        {
            var result = repository.GetById(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: получение записи Tour");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Создаёт экскурсию.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(TourResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TourResponse> Create(TourRequest request)
    {
        try
        {
            if (!IsValid(request))
                return BadRequest("Invalid tour data.");
            var result = repository.Create(request);
            logger.LogInformation("Создана запись Tour с Id={Id}", result.Id);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: создание записи Tour");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Изменяет экскурсию.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(int id, TourRequest request)
    {
        try
        {
            if (!IsValid(request))
                return BadRequest("Invalid tour data.");
            if (!repository.Update(id, request)) return NotFound();
            logger.LogInformation("Изменена запись Tour с Id={Id}", id);
            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: изменение записи Tour");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Удаляет экскурсию.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            if (!repository.Delete(id)) return NotFound();
            logger.LogInformation("Удалена запись Tour: Id={Id}", id);
            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: удаление записи Tour");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Проверяет данные запроса.</summary>
    private static bool IsValid(TourRequest request)
    {
        return request.StartDateTime != default && request.Duration > TimeSpan.Zero;
    }
}
