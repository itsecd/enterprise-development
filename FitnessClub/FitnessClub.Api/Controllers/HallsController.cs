using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции для работы с залами.
/// </summary>
[ApiController]
[Route("api/halls")]
[Produces("application/json")]
/// <param name="service">Сервис залов.</param>
public class HallsController(IHallService service) : ControllerBase
{
    /// <summary>Возвращает все залы.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<HallDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<HallDto>>> GetAll()
    {
        return Ok(await service.GetAllAsync());
    }

    /// <summary>Возвращает зал по ID.</summary>
    /// <param name="id">ID зала.</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(HallDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HallDto>> GetById(int id)
    {
        var hall = await service.GetByIdAsync(id);

        return hall is null ? NotFound() : Ok(hall);
    }

    /// <summary>Создаёт зал.</summary>
    /// <param name="dto">Данные нового зала.</param>
    [HttpPost]
    [ProducesResponseType(typeof(HallDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HallDto>> Create([FromBody] CreateHallDto dto)
    {
        var hall = await service.CreateAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = hall.Id }, hall);
    }

    /// <summary>Обновляет зал.</summary>
    /// <param name="id">ID зала.</param>
    /// <param name="dto">Новые данные зала.</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateHallDto dto)
    {
        var updated = await service.UpdateAsync(id, dto);

        return updated ? NoContent() : NotFound();
    }

    /// <summary>Удаляет зал.</summary>
    /// <param name="id">ID зала.</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await service.DeleteAsync(id);

        return deleted ? NoContent() : NotFound();
    }
}