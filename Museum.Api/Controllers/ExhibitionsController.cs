using Microsoft.AspNetCore.Mvc;
using Museum.Application.Repositories;
using Museum.Contracts;

namespace Museum.Api.Controllers;

/// <summary>
/// REST API для выставок.
/// </summary>
[ApiController]
[Route("api/exhibitions")]
public class ExhibitionsController(ExhibitionsRepository repository, ILogger<ExhibitionsController> logger) : ControllerBase
{
    /// <summary>Возвращает выставки.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ExhibitionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<ExhibitionResponse>> GetAll()
    {
        try
        {
            return Ok(repository.GetAll());
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: получение списка выставок");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Возвращает выставку по идентификатору.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ExhibitionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ExhibitionResponse> GetById(int id)
    {
        try
        {
            var result = repository.GetById(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: получение записи Exhibition");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Создаёт выставку.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ExhibitionResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ExhibitionResponse> Create(ExhibitionRequest request)
    {
        try
        {
            if (!IsValid(request))
                return BadRequest("Invalid exhibition data.");
            var result = repository.Create(request);
            logger.LogInformation("Создана запись Exhibition с Id={Id}", result.Id);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: создание записи Exhibition");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Изменяет выставку.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(int id, ExhibitionRequest request)
    {
        try
        {
            if (!IsValid(request))
                return BadRequest("Invalid exhibition data.");
            if (!repository.Update(id, request)) return NotFound();
            logger.LogInformation("Изменена запись Exhibition с Id={Id}", id);
            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: изменение записи Exhibition");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Удаляет выставку.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            if (!repository.Delete(id)) return NotFound();
            logger.LogInformation("Удалена запись Exhibition: Id={Id}", id);
            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: удаление записи Exhibition");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Проверяет данные запроса.</summary>
    private static bool IsValid(ExhibitionRequest request)
    {
        return !string.IsNullOrWhiteSpace(request.Name)
            && Enum.IsDefined(request.Theme)
            && request.HallNumber > 0
            && request.StartDate != default
            && request.EndDate != default
            && request.StartDate <= request.EndDate;
    }
}
