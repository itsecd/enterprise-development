using Microsoft.AspNetCore.Mvc;
using Museum.Application.Repositories;
using Museum.Contracts;

namespace Museum.Api.Controllers;

/// <summary>
/// REST API для связей экскурсий и выставок.
/// </summary>
[ApiController]
[Route("api/tour-exhibitions")]
public class TourExhibitionsController(TourExhibitionsRepository repository, ILogger<TourExhibitionsController> logger) : ControllerBase
{
    /// <summary>Возвращает связи экскурсий и выставок.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TourExhibitionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<TourExhibitionResponse>> GetAll()
    {
        try
        {
            return Ok(repository.GetAll());
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: получение списка связей экскурсий и выставок");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Возвращает связь экскурсии и выставки по идентификатору.</summary>
    [HttpGet("{tourId:int}/{exhibitionId:int}")]
    [ProducesResponseType(typeof(TourExhibitionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TourExhibitionResponse> GetById(int tourId, int exhibitionId)
    {
        try
        {
            var result = repository.GetById(tourId, exhibitionId);
            if (result is null) return NotFound();
            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: получение записи TourExhibition");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Создаёт связь экскурсии и выставки.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(TourExhibitionResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TourExhibitionResponse> Create(TourExhibitionRequest request)
    {
        try
        {
            var (status, result) = repository.Create(request);
            if (status == RepositoryChangeResult.InvalidReference)
                return BadRequest("Tour or exhibition not found.");
            if (status == RepositoryChangeResult.Conflict)
                return Conflict("This relation already exists.");
            logger.LogInformation("Создана связь TourId={TourId}, ExhibitionId={ExhibitionId}", result!.TourId, result.ExhibitionId);
            return CreatedAtAction(nameof(GetById),
                new { tourId = result.TourId, exhibitionId = result.ExhibitionId }, result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: создание записи TourExhibition");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Изменяет связь экскурсии и выставки.</summary>
    [HttpPut("{tourId:int}/{exhibitionId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(int tourId, int exhibitionId, TourExhibitionRequest request)
    {
        try
        {
            var status = repository.Update(tourId, exhibitionId, request);
            if (status == RepositoryChangeResult.NotFound) return NotFound();
            if (status == RepositoryChangeResult.InvalidReference)
                return BadRequest("Tour or exhibition not found.");
            if (status == RepositoryChangeResult.Conflict) return Conflict("This relation already exists.");
            logger.LogInformation("Изменена запись TourExhibition: TourId={TourId}, ExhibitionId={ExhibitionId}", tourId, exhibitionId);
            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: изменение записи TourExhibition");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Удаляет связь экскурсии и выставки.</summary>
    [HttpDelete("{tourId:int}/{exhibitionId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int tourId, int exhibitionId)
    {
        try
        {
            if (!repository.Delete(tourId, exhibitionId)) return NotFound();
            logger.LogInformation("Удалена запись TourExhibition: TourId={TourId}, ExhibitionId={ExhibitionId}", tourId, exhibitionId);
            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка: удаление записи TourExhibition");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }
}
