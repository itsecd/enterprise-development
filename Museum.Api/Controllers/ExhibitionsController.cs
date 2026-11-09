using Microsoft.AspNetCore.Mvc;
using Museum.Api.Contracts;
using Museum.Domain.Data;
using Museum.Domain.Entities;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/exhibitions")]
public class ExhibitionsController(MuseumContext context) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<ExhibitionResponse>> GetAll()
    {
        lock (context)
        {
            var exhibitions = context.Exhibitions
                .OrderBy(exhibition => exhibition.Id)
                .Select(ToResponse)
                .ToList();

            return Ok(exhibitions);
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult<ExhibitionResponse> GetById(int id)
    {
        lock (context)
        {
            var exhibition = context.Exhibitions
                .FirstOrDefault(exhibition => exhibition.Id == id);

            if (exhibition is null)
                return NotFound();

            return Ok(ToResponse(exhibition));
        }
    }

    [HttpPost]
    public ActionResult<ExhibitionResponse> Create(
        ExhibitionRequest request)
    {
        if (!IsValid(request))
            return BadRequest("Invalid exhibition data.");

        lock (context)
        {
            var nextId = context.Exhibitions.Count == 0
                ? 1
                : context.Exhibitions.Max(exhibition => exhibition.Id) + 1;

            var exhibition = new Exhibition
            {
                Id = nextId,
                Name = request.Name,
                Theme = request.Theme,
                HallNumber = request.HallNumber,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            context.Exhibitions.Add(exhibition);

            return CreatedAtAction(
                nameof(GetById),
                new { id = exhibition.Id },
                ToResponse(exhibition));
        }
    }

    [HttpPut("{id:int}")]
    public IActionResult Update(int id, ExhibitionRequest request)
    {
        if (!IsValid(request))
            return BadRequest("Invalid exhibition data.");

        lock (context)
        {
            var exhibition = context.Exhibitions
                .FirstOrDefault(exhibition => exhibition.Id == id);

            if (exhibition is null)
                return NotFound();

            exhibition.Name = request.Name;
            exhibition.Theme = request.Theme;
            exhibition.HallNumber = request.HallNumber;
            exhibition.StartDate = request.StartDate;
            exhibition.EndDate = request.EndDate;

            return NoContent();
        }
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        lock (context)
        {
            var exhibition = context.Exhibitions
                .FirstOrDefault(exhibition => exhibition.Id == id);

            if (exhibition is null)
                return NotFound();

            var relations = context.TourExhibitions
                .Where(relation => relation.ExhibitionId == id)
                .ToList();

            foreach (var relation in relations)
            {
                relation.Tour.TourExhibitions.Remove(relation);
                context.TourExhibitions.Remove(relation);
            }

            context.Exhibitions.Remove(exhibition);

            return NoContent();
        }
    }

    private static ExhibitionResponse ToResponse(Exhibition exhibition)
    {
        return new ExhibitionResponse(
            exhibition.Id,
            exhibition.Name,
            exhibition.Theme,
            exhibition.HallNumber,
            exhibition.StartDate,
            exhibition.EndDate
        );
    }

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
