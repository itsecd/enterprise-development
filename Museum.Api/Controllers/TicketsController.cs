using Microsoft.AspNetCore.Mvc;
using Museum.Api.Dtos;
using Museum.Api.Storage;
using Museum.Domain;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController(MuseumStore store) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Ticket>> GetAll()
    {
        return store.Tickets;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Ticket> GetById(int id)
    {
        var ticket = store.Tickets.FirstOrDefault(t => t.Id == id);
        if (ticket is null)
        {
            return NotFound();
        }

        return ticket;
    }

    [HttpPost]
    public ActionResult<Ticket> Create(TicketRequest request)
    {
        var tour = store.Tours.FirstOrDefault(t => t.Id == request.TourId);
        var visitor = store.Visitors.FirstOrDefault(v => v.Id == request.VisitorId);
        if (tour is null || visitor is null)
        {
            return BadRequest("Экскурсия или посетитель не найдены");
        }

        var ticket = new Ticket
        {
            Id = store.Tickets.Select(t => t.Id).DefaultIfEmpty(0).Max() + 1,
            Tour = tour,
            Visitor = visitor,
            Type = request.Type,
            Price = request.Price
        };
        store.Tickets.Add(ticket);

        return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Ticket> Update(int id, TicketRequest request)
    {
        var ticket = store.Tickets.FirstOrDefault(t => t.Id == id);
        if (ticket is null)
        {
            return NotFound();
        }

        var tour = store.Tours.FirstOrDefault(t => t.Id == request.TourId);
        var visitor = store.Visitors.FirstOrDefault(v => v.Id == request.VisitorId);
        if (tour is null || visitor is null)
        {
            return BadRequest("Экскурсия или посетитель не найдены");
        }

        ticket.Tour = tour;
        ticket.Visitor = visitor;
        ticket.Type = request.Type;
        ticket.Price = request.Price;

        return ticket;
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var ticket = store.Tickets.FirstOrDefault(t => t.Id == id);
        if (ticket is null)
        {
            return NotFound();
        }

        store.Tickets.Remove(ticket);

        return NoContent();
    }
}
