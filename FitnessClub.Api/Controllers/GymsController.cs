
using FitnessClub.Api.Dtos.Gyms;
using FitnessClub.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для управления залами фитнес-клуба и проверки их доступности для занятий.
/// </summary>
[ApiController]
[Route("api/gyms")]
public class GymsController(GymService gymService) : ControllerBase
{
    /// <summary>
    /// Получить все залы.
    /// </summary>
    [HttpGet]
    public ActionResult<IReadOnlyList<GymDto>> GetAll()
    {
        return Ok(gymService.GetAll());
    }

    /// <summary>
    /// Получить зал по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<GymDto> GetById(int id)
    {
        var gym = gymService.GetById(id);

        if (gym is null)
        {
            return NotFound();
        }

        return Ok(gym);
    }

    /// <summary>
    /// Проверить доступность зала в текущий момент.
    /// </summary>
    [HttpGet("{id:int}/availability")]
    public ActionResult<bool> GetAvailability(int id)
    {
        var availability = gymService.IsAvailable(id);

        if (availability is null)
        {
            return NotFound();
        }

        return Ok(availability.Value);
    }

    /// <summary>
    /// Создать новый зал.
    /// </summary>
    [HttpPost]
    public ActionResult<GymDto> Create(CreateGymDto dto)
    {
        try
        {
            var gym = gymService.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = gym.Id },
                gym);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Изменить данные зала по идентификатору.
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, CreateGymDto dto)
    {
        try
        {
            if (!gymService.Update(id, dto))
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Удалить зал по идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            if (!gymService.Delete(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}
