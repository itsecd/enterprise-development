using Microsoft.AspNetCore.Mvc;
using VetClinic.Application.Dto;
using VetClinic.Application.Mapping;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Enums;
using VetClinic.Domain.Interfaces;

namespace VetClinic.WebApi.Controllers;

/// <summary>
/// Управление ветеринарными врачами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class VeterinariansController(
    IRepository<Veterinarian> repository,
    IRepository<Specialization> specializationRepository,
    IVetClinicService clinicService,
    ILogger<VeterinariansController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех ветеринаров
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VeterinarianDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<VeterinarianDto>>> GetAll()
    {
        try
        {
            var vets = await repository.GetAllAsync();
            return Ok(vets.Select(v => v.ToDto()));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка ветеринаров");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить ветеринаров с профилем по выбранному виду животных
    /// </summary>
    [HttpGet("by-species")]
    [ProducesResponseType(typeof(IEnumerable<VeterinarianDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<VeterinarianDto>>> GetBySpecies([FromQuery] AnimalSpecies species)
    {
        try
        {
            var vets = await clinicService.GetVeterinariansBySpeciesAsync(species);
            return Ok(vets.Select(v => v.ToDto()));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении ветеринаров по виду животных {Species}", species);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить врача по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VeterinarianDto>> GetById(int id)
    {
        try
        {
            var vet = await repository.GetByIdAsync(id);
            if (vet is null)
            {
                return NotFound();
            }

            return Ok(vet.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении ветеринара с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Добавить нового врача
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VeterinarianDto>> Create([FromBody] CreateVeterinarianDto dto)
    {
        try
        {
            var spec = await specializationRepository.GetByIdAsync(dto.SpecializationId);
            if (spec is null)
            {
                return BadRequest("Указанная специализация не найдена");
            }

            var vet = new Veterinarian
            {
                PassportNumber = dto.PassportNumber,
                FullName = dto.FullName,
                BirthYear = dto.BirthYear,
                ExperienceYears = dto.ExperienceYears,
                SpecializationId = dto.SpecializationId,
                Specialization = spec
            };

            await repository.AddAsync(vet);

            return CreatedAtAction(nameof(GetById), new { id = vet.Id }, vet.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при добавлении врача");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить данные врача
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] CreateVeterinarianDto dto)
    {
        try
        {
            var vet = await repository.GetByIdAsync(id);
            if (vet is null)
            {
                return NotFound();
            }

            var spec = await specializationRepository.GetByIdAsync(dto.SpecializationId);
            if (spec is null)
            {
                return BadRequest("Указанная специализация не найдена");
            }

            vet.PassportNumber = dto.PassportNumber;
            vet.FullName = dto.FullName;
            vet.BirthYear = dto.BirthYear;
            vet.ExperienceYears = dto.ExperienceYears;
            vet.SpecializationId = dto.SpecializationId;
            vet.Specialization = spec;

            await repository.UpdateAsync(vet);

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении данных врача с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить врача
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
            logger.LogError(ex, "Ошибка при удалении врача с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}