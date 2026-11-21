using Microsoft.AspNetCore.Mvc;
using TransportCompany.Contracts.Dto;
using TransportCompany.Contracts.Interfaces;

namespace TransportCompany.Api.Controllers;

/// <summary>
/// Контроллер для управления моделями транспортных средств
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VehicleModelController(IVehicleModelService service, ILogger<VehicleModelController> logger) : ControllerBase
{
    private readonly IVehicleModelService _service = service;
    private readonly ILogger<VehicleModelController> _logger = logger;

    /// <summary>
    /// Получить список всех моделей транспортных средств
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VehicleModelDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<VehicleModelDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetAllAsync(cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении моделей ТС");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить модель транспортного средства по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(VehicleModelDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<VehicleModelDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetByIdAsync(id, cancellationToken);
            if (result == null) return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении модели ТС {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создать новую модель транспортного средства
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(VehicleModelDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<VehicleModelDto>> Create([FromBody] VehicleModelEditDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании модели ТС");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить данные модели транспортного средства
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(int id, [FromBody] VehicleModelEditDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, dto, cancellationToken);
            if (updated == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении модели ТС {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить модель транспортного средства
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var isDeleted = await _service.DeleteAsync(id, cancellationToken);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении модели ТС {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}