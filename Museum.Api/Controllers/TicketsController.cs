using Microsoft.AspNetCore.Mvc;
using Museum.Api.Contracts;
using Museum.Domain.Data;
using Museum.Domain.Entities;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketsController(MuseumContext context) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<TicketResponse>> GetAll()
    {
        lock (context)
        {
            var tickets = context.Tickets
                .OrderBy(ticket => ticket.Id)
                .Select(ToResponse)
                .ToList();

            return Ok(tickets);
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult<TicketResponse> GetById(int id)
    {
        lock (context)
        {
            var ticket = context.Tickets
                .FirstOrDefault(ticket => ticket.Id == id);

            if (ticket is null)
                return NotFound();

            return Ok(ToResponse(ticket));
        }
    }

    [HttpPost]
    public ActionResult<TicketResponse> Create(TicketRequest request)
    {
        if (!IsValid(request))
            return BadRequest("Invalid ticket data.");

        lock (context)
        {
            var tour = context.Tours
                .FirstOrDefault(tour => tour.Id == request.TourId);

            var visitor = context.Visitors
                .FirstOrDefault(visitor => visitor.Id == request.VisitorId);

            if (tour is null || visitor is null)
                return BadRequest("Tour or visitor not found.");

            var nextId = context.Tickets.Count == 0
                ? 1
                : context.Tickets.Max(ticket => ticket.Id) + 1;

            var ticket = new Ticket
            {
                Id = nextId,
                TourId = tour.Id,
                VisitorId = visitor.Id,
                Type = request.Type,
                Price = request.Price,
                Tour = tour,
                Visitor = visitor
            };

            context.Tickets.Add(ticket);
            tour.Tickets.Add(ticket);
            visitor.Tickets.Add(ticket);

            return CreatedAtAction(
                nameof(GetById),
                new { id = ticket.Id },
                ToResponse(ticket));
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, TicketRequest request)
    {
        if (!IsValid(request))
            return BadRequest("Invalid ticket data.");

        lock (context)
        {
            var ticket = context.Tickets
                .FirstOrDefault(ticket => ticket.Id == id);

            if (ticket is null)
                return NotFound();

            var tour = context.Tours
                .FirstOrDefault(tour => tour.Id == request.TourId);

            var visitor = context.Visitors
                .FirstOrDefault(visitor => visitor.Id == request.VisitorId);

            if (tour is null || visitor is null)
                return BadRequest("Tour or visitor not found.");

            ticket.Tour.Tickets.Remove(ticket);
            ticket.Visitor.Tickets.Remove(ticket);

            ticket.TourId = tour.Id;
            ticket.VisitorId = visitor.Id;
            ticket.Type = request.Type;
            ticket.Price = request.Price;
            ticket.Tour = tour;
            ticket.Visitor = visitor;

            tour.Tickets.Add(ticket);
            visitor.Tickets.Add(ticket);

            return NoContent();
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        lock (context)
        {
            var ticket = context.Tickets
                .FirstOrDefault(ticket => ticket.Id == id);

            if (ticket is null)
                return NotFound();

            ticket.Tour.Tickets.Remove(ticket);
            ticket.Visitor.Tickets.Remove(ticket);
            context.Tickets.Remove(ticket);

            return NoContent();
        }
    }

    private static TicketResponse ToResponse(Ticket ticket)
    {
        return new TicketResponse(
            ticket.Id,
            ticket.TourId,
            ticket.VisitorId,
            ticket.Type,
            ticket.Price
        );
    }

    private static bool IsValid(TicketRequest request)
    {
        return request.TourId > 0
            && request.VisitorId > 0
            && Enum.IsDefined(request.Type)
            && request.Price >= 0;
    }
}
