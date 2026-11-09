using Microsoft.AspNetCore.Mvc;
using Museum.Api.Contracts;
using Museum.Domain.Data;
using Museum.Domain.Entities;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/visitors")]
public class VisitorsController(MuseumContext context) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<VisitorResponse>> GetAll()
    {
        lock (context)
        {
            var visitors = context.Visitors
                .OrderBy(visitor => visitor.Id)
                .Select(ToResponse)
                .ToList();

            return Ok(visitors);
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult<VisitorResponse> GetById(int id)
    {
        lock (context)
        {
            var visitor = context.Visitors
                .FirstOrDefault(visitor => visitor.Id == id);

            if (visitor is null)
                return NotFound();

            return Ok(ToResponse(visitor));
        }
    }

    [HttpPost]
    public ActionResult<VisitorResponse> Create(VisitorRequest request)
    {
        if (!IsValid(request))
            return BadRequest("Invalid visitor data.");

        lock (context)
        {
            var nextId = context.Visitors.Count == 0
                ? 1
                : context.Visitors.Max(visitor => visitor.Id) + 1;

            var visitor = new Visitor
            {
                Id = nextId,
                FullName = request.FullName,
                Phone = request.Phone,
                BirthDate = request.BirthDate
            };

            context.Visitors.Add(visitor);

            return CreatedAtAction(
                nameof(GetById),
                new { id = visitor.Id },
                ToResponse(visitor));
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, VisitorRequest request)
    {
        if (!IsValid(request))
            return BadRequest("Invalid visitor data.");

        lock (context)
        {
            var visitor = context.Visitors
                .FirstOrDefault(visitor => visitor.Id == id);

            if (visitor is null)
                return NotFound();

            visitor.FullName = request.FullName;
            visitor.Phone = request.Phone;
            visitor.BirthDate = request.BirthDate;

            return NoContent();
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        lock (context)
        {
            var visitor = context.Visitors
                .FirstOrDefault(visitor => visitor.Id == id);

            if (visitor is null)
                return NotFound();

            var tickets = context.Tickets
                .Where(ticket => ticket.VisitorId == id)
                .ToList();

            foreach (var ticket in tickets)
            {
                ticket.Tour.Tickets.Remove(ticket);
                visitor.Tickets.Remove(ticket);
                context.Tickets.Remove(ticket);
            }

            context.Visitors.Remove(visitor);

            return NoContent();
        }
    }

    private static VisitorResponse ToResponse(Visitor visitor)
    {
        return new VisitorResponse(
            visitor.Id,
            visitor.FullName,
            visitor.Phone,
            visitor.BirthDate
        );
    }

    private static bool IsValid(VisitorRequest request)
    {
        return !string.IsNullOrWhiteSpace(request.FullName)
            && !string.IsNullOrWhiteSpace(request.Phone)
            && request.BirthDate != default
            && request.BirthDate <= DateOnly.FromDateTime(DateTime.Today);
    }
}
