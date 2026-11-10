using Microsoft.AspNetCore.Mvc;
using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;

namespace Museum.Api.Controllers;

/// <summary>Обрабатывает HTTP-запросы для работы с билетами</summary>
[ApiController]
[Route("api/[controller]")]
public sealed class TicketsController(
    ITicketService ticketService,
    ILogger<TicketsController> logger) : ControllerBase
{
    /// <summary>Возвращает список всех билетов</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TicketDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TicketDto>>> GetAll()
    {
        var tickets = await ticketService.GetAllAsync();
        return Ok(tickets);
    }

    /// <summary>Возвращает билет по его идентификатору</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> GetById(int id)
    {
        var ticket = await ticketService.GetByIdAsync(id);

        if (ticket is null)
        {
            logger.LogWarning("Билет {TicketId} не найден", id);
            return NotFound();
        }

        return Ok(ticket);
    }

    /// <summary>Оформляет новый билет</summary>
    [HttpPost]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TicketDto>> Create(TicketEditDto dto)
    {
        logger.LogInformation(
            "Оформление билета на экскурсию {TourId} для посетителя {VisitorId}",
            dto.TourId,
            dto.VisitorId);

        try
        {
            var ticket = await ticketService.CreateAsync(dto);


            return CreatedAtAction(
                nameof(GetById),
                new { id = ticket.Id },
                ticket);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Не удалось оформить билет");
            return BadRequest(exception.Message);
        }
    }

    /// <summary>Обновляет сведения о билете</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, TicketEditDto dto)
    {
        logger.LogInformation("Обновление билета {TicketId}", id);

        try
        {
            var updated = await ticketService.UpdateAsync(id, dto);

            if (!updated)
            {
                logger.LogWarning("Билет {TicketId} для обновления не найден", id);
                return NotFound();
            }

            return NoContent();
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Не удалось обновить билет {TicketId}", id);
            return BadRequest(exception.Message);
        }
    }

    /// <summary>Удаляет билет по его идентификатору</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation("Удаление билета {TicketId}", id);

        var deleted = await ticketService.DeleteAsync(id);

        if (!deleted)
        {
            logger.LogWarning("Билет {TicketId} для удаления не найден", id);
            return NotFound();
        }

        return NoContent();
    }
}
