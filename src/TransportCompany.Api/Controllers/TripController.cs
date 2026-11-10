using Microsoft.AspNetCore.Mvc;
using TransportCompany.Contracts.Dto;
using TransportCompany.Contracts.Interfaces;

namespace TransportCompany.Api.Controllers;

/// <summary>
/// Контроллер для управления рейсами транспортной компании
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TripController(ITripService tripService, ILogger<TripController> logger) : ControllerBase
{
    private readonly ITripService _tripService = tripService;
    private readonly ILogger<TripController> _logger = logger;

    /// <summary>
    /// Получить список всех рейсов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TripDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<TripDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var trips = await _tripService.GetAllAsync(cancellationToken);
            return Ok(trips);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка рейсов");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить рейс по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TripDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TripDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var trip = await _tripService.GetByIdAsync(id, cancellationToken);
            if (trip == null)
            {
                return NotFound();
            }
            return Ok(trip);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении рейса с Id {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создать новый рейс
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TripDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TripDto>> Create([FromBody] TripEditDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var createdTrip = await _tripService.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = createdTrip.Id }, createdTrip);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании рейса");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить данные рейса
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(int id, [FromBody] TripEditDto dto, CancellationToken cancellationToken)
    {
        try
        {
            await _tripService.UpdateAsync(id, dto, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении рейса с Id {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить рейс
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            await _tripService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении рейса с Id {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}