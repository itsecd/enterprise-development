using Microsoft.AspNetCore.Mvc;
using Museum.Application.Repositories;
using Museum.Contracts;

namespace Museum.Api.Controllers;

/// <summary>
/// REST API для посетителей.
/// </summary>
[ApiController]
[Route("api/visitors")]
public class VisitorsController(VisitorsRepository repository, ILogger<VisitorsController> logger) : ControllerBase
{
    /// <summary>Возвращает посетители.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<VisitorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<VisitorResponse>> GetAll()
    {
        try
        {
            return Ok(repository.GetAll());
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: получение списка посетителей");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Возвращает посетителя по идентификатору.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(VisitorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<VisitorResponse> GetById(int id)
    {
        try
        {
            var result = repository.GetById(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: получение записи Visitor");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Создаёт посетителя.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(VisitorResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<VisitorResponse> Create(VisitorRequest request)
    {
        try
        {
            if (!IsValid(request))
                return BadRequest("Invalid visitor data.");
            var result = repository.Create(request);
            logger.LogInformation("Создана запись Visitor с Id={Id}", result.Id);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: создание записи Visitor");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Изменяет посетителя.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(int id, VisitorRequest request)
    {
        try
        {
            if (!IsValid(request))
                return BadRequest("Invalid visitor data.");
            if (!repository.Update(id, request)) return NotFound();
            logger.LogInformation("Изменена запись Visitor с Id={Id}", id);
            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: изменение записи Visitor");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Удаляет посетителя.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            if (!repository.Delete(id)) return NotFound();
            logger.LogInformation("Удалена запись Visitor: Id={Id}", id);
            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: удаление записи Visitor");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Проверяет данные запроса.</summary>
    private static bool IsValid(VisitorRequest request)
    {
        return !string.IsNullOrWhiteSpace(request.FullName)
            && !string.IsNullOrWhiteSpace(request.Phone)
            && request.BirthDate != default
            && request.BirthDate <= DateOnly.FromDateTime(DateTime.Today);
    }
}
