using Microsoft.AspNetCore.Mvc;
using Museum.Api.Contracts;
using Museum.Domain.Data;
using Museum.Domain.Entities;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/tours")]
public class ToursController(MuseumContext context) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<TourResponse>> GetAll()
    {
        lock (context)
        {
            var tours = context.Tours
                .OrderBy(tour => tour.Id)
                .Select(ToResponse)
                .ToList();

            return Ok(tours);
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult<TourResponse> GetById(int id)
    {
        lock (context)
        {
            var tour = context.Tours
                .FirstOrDefault(tour => tour.Id == id);

            if (tour is null)
                return NotFound();

            return Ok(ToResponse(tour));
        }
    }

    [HttpPost]
    public ActionResult<TourResponse> Create(TourRequest request)
    {
        if (!IsValid(request))
            return BadRequest("Invalid tour data.");

        lock (context)
        {
            var nextId = context.Tours.Count == 0
                ? 1
                : context.Tours.Max(tour => tour.Id) + 1;

            var tour = new Tour
            {
                Id = nextId,
                StartDateTime = request.StartDateTime,
                Duration = request.Duration
            };

            context.Tours.Add(tour);

            return CreatedAtAction(
                nameof(GetById),
                new { id = tour.Id },
                ToResponse(tour));
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, TourRequest request)
    {
        if (!IsValid(request))
            return BadRequest("Invalid tour data.");

        lock (context)
        {
            var tour = context.Tours
                .FirstOrDefault(tour => tour.Id == id);

            if (tour is null)
                return NotFound();

            tour.StartDateTime = request.StartDateTime;
            tour.Duration = request.Duration;

            return NoContent();
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        lock (context)
        {
            var tour = context.Tours
                .FirstOrDefault(tour => tour.Id == id);

            if (tour is null)
                return NotFound();

            foreach (var ticket in tour.Tickets.ToList())
            {
                ticket.Visitor.Tickets.Remove(ticket);
                tour.Tickets.Remove(ticket);
                context.Tickets.Remove(ticket);
            }

            foreach (var relation in tour.TourExhibitions.ToList())
            {
                relation.Exhibition.TourExhibitions.Remove(relation);
                tour.TourExhibitions.Remove(relation);
                context.TourExhibitions.Remove(relation);
            }

            context.Tours.Remove(tour);

            return NoContent();
        }
    }

    private static TourResponse ToResponse(Tour tour)
    {
        return new TourResponse(
            tour.Id,
            tour.StartDateTime,
            tour.Duration,
            tour.TourExhibitions
                .Select(relation => relation.ExhibitionId)
                .OrderBy(id => id)
                .ToArray()
        );
    }

    private static bool IsValid(TourRequest request)
    {
        return request.StartDateTime != default
            && request.Duration > TimeSpan.Zero;
    }
}
