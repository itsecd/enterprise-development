using Microsoft.AspNetCore.Mvc;
using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;

namespace Museum.Api.Controllers;

/// <summary>Обрабатывает HTTP-запросы для работы с билетами</summary>
[ApiController]
[Route("api/[controller]")]
public sealed class TicketsController(ITicketService ticketService) : ControllerBase
{
    /// <summary>Возвращает список всех билетов</summary>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TicketDto>>> GetAll()
    {
        var tickets = await ticketService.GetAllAsync();
        return Ok(tickets);
    }

    /// <summary>Возвращает билет по его идентификатору</summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TicketDto>> GetById(int id)
    {
        var ticket = await ticketService.GetByIdAsync(id);

        if (ticket is null)
            return NotFound();

        return Ok(ticket);
    }

    /// <summary>Оформляет новый билет</summary>
    [HttpPost]
    public async Task<ActionResult<TicketDto>> Create(TicketEditDto dto)
    {
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
            return BadRequest(exception.Message);
        }
    }

    /// <summary>Обновляет сведения о билете</summary>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, TicketEditDto dto)
    {
        try
        {
            var updated = await ticketService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    /// <summary>Удаляет билет по его идентификатору</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await ticketService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
