using Microsoft.AspNetCore.Mvc;
using Museum.Application.Repositories;
using Museum.Contracts;

namespace Museum.Api.Controllers;

/// <summary>
/// REST API для билетов.
/// </summary>
[ApiController]
[Route("api/tickets")]
public class TicketsController(TicketsRepository repository, ILogger<TicketsController> logger) : ControllerBase
{
    /// <summary>Возвращает билеты.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TicketResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<TicketResponse>> GetAll()
    {
        try
        {
            return Ok(repository.GetAll());
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: получение списка билетов");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Возвращает билет по идентификатору.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TicketResponse> GetById(int id)
    {
        try
        {
            var result = repository.GetById(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: получение записи Ticket");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Создаёт билет.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TicketResponse> Create(TicketRequest request)
    {
        try
        {
            if (!IsValid(request))
                return BadRequest("Invalid ticket data.");
            var result = repository.Create(request);
            if (result is null)
                return BadRequest("Tour or visitor not found.");
            logger.LogInformation("Создана запись Ticket с Id={Id}", result!.Id);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: создание записи Ticket");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Изменяет билет.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(int id, TicketRequest request)
    {
        try
        {
            if (!IsValid(request))
                return BadRequest("Invalid ticket data.");
            var status = repository.Update(id, request);
            if (status == RepositoryChangeResult.NotFound) return NotFound();
            if (status == RepositoryChangeResult.InvalidReference)
                return BadRequest("Tour or visitor not found.");
            logger.LogInformation("Изменена запись Ticket: Id={Id}", id);
            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: изменение записи Ticket");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Удаляет билет.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            if (!repository.Delete(id)) return NotFound();
            logger.LogInformation("Удалена запись Ticket: Id={Id}", id);
            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: удаление записи Ticket");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Проверяет данные запроса.</summary>
    private static bool IsValid(TicketRequest request)
    {
        return request.TourId > 0
            && request.VisitorId > 0
            && Enum.IsDefined(request.Type)
            && request.Price >= 0;
    }
}
