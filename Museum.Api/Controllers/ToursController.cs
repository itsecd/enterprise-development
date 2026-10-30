using Microsoft.AspNetCore.Mvc;
using Museum.Api.Dtos;
using Museum.Api.Storage;
using Museum.Domain;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToursController(MuseumStore store) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Tour>> GetAll()
    {
        return store.Tours;
    }

    [HttpGet("{id:int}")]
    public ActionResult<Tour> GetById(int id)
    {
        var tour = store.Tours.FirstOrDefault(t => t.Id == id);
        if (tour is null)
        {
            return NotFound();
        }

        return tour;
    }

    [HttpPost]
    public ActionResult<Tour> Create(TourRequest request)
    {
        var exhibitions = FindExhibitions(request.ExhibitionIds);
        if (exhibitions is null)
        {
            return BadRequest("Одна или несколько выставок не найдены");
        }

        var tour = new Tour
        {
            Id = store.Tours.Select(t => t.Id).DefaultIfEmpty(0).Max() + 1,
            Date = request.Date,
            StartTime = request.StartTime,
            DurationMinutes = request.DurationMinutes,
            Exhibitions = exhibitions
        };
        store.Tours.Add(tour);

        return CreatedAtAction(nameof(GetById), new { id = tour.Id }, tour);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Tour> Update(int id, TourRequest request)
    {
        var tour = store.Tours.FirstOrDefault(t => t.Id == id);
        if (tour is null)
        {
            return NotFound();
        }

        var exhibitions = FindExhibitions(request.ExhibitionIds);
        if (exhibitions is null)
        {
            return BadRequest("Одна или несколько выставок не найдены");
        }

        tour.Date = request.Date;
        tour.StartTime = request.StartTime;
        tour.DurationMinutes = request.DurationMinutes;
        tour.Exhibitions = exhibitions;

        return tour;
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var tour = store.Tours.FirstOrDefault(t => t.Id == id);
        if (tour is null)
        {
            return NotFound();
        }

        if (store.Tickets.Any(t => t.Tour.Id == id))
        {
            return Conflict("На экскурсию есть билеты");
        }

        store.Tours.Remove(tour);

        return NoContent();
    }

    private List<Exhibition>? FindExhibitions(List<int> ids)
    {
        var distinctIds = ids.Distinct().ToList();
        var exhibitions = store.Exhibitions.Where(e => distinctIds.Contains(e.Id)).ToList();

        return exhibitions.Count == distinctIds.Count ? exhibitions : null;
    }
}
