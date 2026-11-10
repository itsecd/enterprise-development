using Microsoft.AspNetCore.Mvc;
using VetClinic.Application.Dto;
using VetClinic.Application.Mapping;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.WebApi.Controllers;

/// <summary>
/// Управление приемами ветеринарной клиники
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(
    IRepository<Appointment> repository,
    IRepository<Pet> petRepository,
    IRepository<Veterinarian> vetRepository,
    IVetClinicService clinicService,
    ILogger<AppointmentsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все приемы
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetAll()
    {
        try
        {
            var appointments = await repository.GetAllAsync();
            return Ok(appointments.Select(a => a.ToDto()));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка приемов");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить прием по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AppointmentDto>> GetById(int id)
    {
        try
        {
            var appointment = await repository.GetByIdAsync(id);
            if (appointment is null)
            {
                return NotFound();
            }

            return Ok(appointment.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении приема с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить количество повторных приемов для выбранной породы
    /// </summary>
    [HttpGet("follow-up-count")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> GetFollowUpCount([FromQuery] int breedId)
    {
        try
        {
            var count = await clinicService.GetFollowUpAppointmentsCountByBreedAsync(breedId);
            return Ok(count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при подсчете повторных приемов для породы {BreedId}", breedId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить приемы за указанный месяц в выбранном кабинете
    /// </summary>
    [HttpGet("by-room")]
    [ProducesResponseType(typeof(IEnumerable<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetByRoomAndMonth(
        [FromQuery] int roomNumber,
        [FromQuery] DateTime date)
    {
        try
        {
            var appointments = await clinicService.GetCurrentMonthAppointmentsByRoomAsync(roomNumber, date);
            return Ok(appointments.Select(a => a.ToDto()));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении приемов для кабинета {RoomNumber}", roomNumber);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Записать на прием
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AppointmentDto>> Create([FromBody] CreateAppointmentDto dto)
    {
        try
        {
            var pet = await petRepository.GetByIdAsync(dto.PetId);
            var vet = await vetRepository.GetByIdAsync(dto.VeterinarianId);

            if (pet is null || vet is null)
            {
                return BadRequest("Указанный питомец или ветеринар не найден");
            }

            var appointment = new Appointment
            {
                DateTime = dto.DateTime!.Value,
                RoomNumber = dto.RoomNumber,
                IsFollowUp = dto.IsFollowUp,
                PetId = dto.PetId,
                Pet = pet,
                VeterinarianId = dto.VeterinarianId,
                Veterinarian = vet
            };

            await repository.AddAsync(appointment);

            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании записи на прием");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить данные приема
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] CreateAppointmentDto dto)
    {
        try
        {
            var appointment = await repository.GetByIdAsync(id);
            if (appointment is null)
            {
                return NotFound();
            }

            var pet = await petRepository.GetByIdAsync(dto.PetId);
            var vet = await vetRepository.GetByIdAsync(dto.VeterinarianId);

            if (pet is null || vet is null)
            {
                return BadRequest("Указанный питомец или ветеринар не найден");
            }

            appointment.DateTime = dto.DateTime!.Value;
            appointment.RoomNumber = dto.RoomNumber;
            appointment.IsFollowUp = dto.IsFollowUp;
            appointment.PetId = dto.PetId;
            appointment.Pet = pet;
            appointment.VeterinarianId = dto.VeterinarianId;
            appointment.Veterinarian = vet;

            await repository.UpdateAsync(appointment);

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении приема с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Отменить прием
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await repository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении приема с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}