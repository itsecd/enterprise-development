using Microsoft.AspNetCore.Mvc;
using Museum.Api.Contracts;
using Museum.Domain.Data;
using Museum.Domain.Entities;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/tour-exhibitions")]
public class TourExhibitionsController(MuseumContext context) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<TourExhibitionResponse>> GetAll()
    {
        lock (context)
        {
            var relations = context.TourExhibitions
                .OrderBy(relation => relation.TourId)
                .ThenBy(relation => relation.ExhibitionId)
                .Select(ToResponse)
                .ToList();

            return Ok(relations);
        }
    }

    [HttpGet("{tourId:int}/{exhibitionId:int}")]
    public ActionResult<TourExhibitionResponse> GetById(
        int tourId,
        int exhibitionId)
    {
        lock (context)
        {
            var relation = context.TourExhibitions
                .FirstOrDefault(relation =>
                    relation.TourId == tourId &&
                    relation.ExhibitionId == exhibitionId);

            if (relation is null)
                return NotFound();

            return Ok(ToResponse(relation));
        }
    }

    [HttpPost]
    public ActionResult<TourExhibitionResponse> Create(
        TourExhibitionRequest request)
    {
        lock (context)
        {
            var tour = context.Tours
                .FirstOrDefault(tour => tour.Id == request.TourId);

            var exhibition = context.Exhibitions
                .FirstOrDefault(exhibition =>
                    exhibition.Id == request.ExhibitionId);

            if (tour is null || exhibition is null)
                return BadRequest("Tour or exhibition not found.");

            var exists = context.TourExhibitions.Any(relation =>
                relation.TourId == request.TourId &&
                relation.ExhibitionId == request.ExhibitionId);

            if (exists)
                return Conflict("This relation already exists.");

            var relation = new TourExhibition
            {
                TourId = tour.Id,
                ExhibitionId = exhibition.Id,
                Tour = tour,
                Exhibition = exhibition
            };

            context.TourExhibitions.Add(relation);
            tour.TourExhibitions.Add(relation);
            exhibition.TourExhibitions.Add(relation);

            return CreatedAtAction(
                nameof(GetById),
                new
                {
                    tourId = relation.TourId,
                    exhibitionId = relation.ExhibitionId
                },
                ToResponse(relation));
        }
    }

    [HttpPut("{tourId:int}/{exhibitionId:int}")]
    public IActionResult Update(
        int tourId,
        int exhibitionId,
        TourExhibitionRequest request)
    {
        lock (context)
        {
            var relation = context.TourExhibitions
                .FirstOrDefault(relation =>
                    relation.TourId == tourId &&
                    relation.ExhibitionId == exhibitionId);

            if (relation is null)
                return NotFound();

            var newTour = context.Tours
                .FirstOrDefault(tour => tour.Id == request.TourId);

            var newExhibition = context.Exhibitions
                .FirstOrDefault(exhibition =>
                    exhibition.Id == request.ExhibitionId);

            if (newTour is null || newExhibition is null)
                return BadRequest("Tour or exhibition not found.");

            var exists = context.TourExhibitions.Any(other =>
                !ReferenceEquals(other, relation) &&
                other.TourId == request.TourId &&
                other.ExhibitionId == request.ExhibitionId);

            if (exists)
                return Conflict("This relation already exists.");

            relation.Tour.TourExhibitions.Remove(relation);
            relation.Exhibition.TourExhibitions.Remove(relation);

            relation.TourId = newTour.Id;
            relation.ExhibitionId = newExhibition.Id;
            relation.Tour = newTour;
            relation.Exhibition = newExhibition;

            newTour.TourExhibitions.Add(relation);
            newExhibition.TourExhibitions.Add(relation);

            return NoContent();
        }
    }

    [HttpDelete("{tourId:int}/{exhibitionId:int}")]
    public IActionResult Delete(int tourId, int exhibitionId)
    {
        lock (context)
        {
            var relation = context.TourExhibitions
                .FirstOrDefault(relation =>
                    relation.TourId == tourId &&
                    relation.ExhibitionId == exhibitionId);

            if (relation is null)
                return NotFound();

            relation.Tour.TourExhibitions.Remove(relation);
            relation.Exhibition.TourExhibitions.Remove(relation);
            context.TourExhibitions.Remove(relation);

            return NoContent();
        }
    }

    private static TourExhibitionResponse ToResponse(
        TourExhibition relation)
    {
        return new TourExhibitionResponse(
            relation.TourId,
            relation.ExhibitionId
        );
    }
}
