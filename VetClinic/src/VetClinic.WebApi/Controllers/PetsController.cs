using Microsoft.AspNetCore.Mvc;
using VetClinic.Application.Dto;
using VetClinic.Application.Mapping;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;

namespace VetClinic.WebApi.Controllers;

/// <summary>
/// Управление питомцами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PetsController(
    IRepository<Pet> repository,
    IRepository<Breed> breedRepository,
    IRepository<Owner> ownerRepository,
    IVetClinicService clinicService,
    ILogger<PetsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех питомцев
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<PetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PetDto>>> GetAll()
    {
        try
        {
            var pets = await repository.GetAllAsync();
            return Ok(pets.Select(p => p.ToDto()));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка питомцев");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить питомцев записанных к выбранному врачу
    /// </summary>
    [HttpGet("by-doctor/{doctorId:int}")]
    [ProducesResponseType(typeof(IEnumerable<PetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PetDto>>> GetPetsByDoctor(int doctorId)
    {
        try
        {
            var pets = await clinicService.GetPetsByDoctorOrderedByNameAsync(doctorId);
            return Ok(pets.Select(p => p.ToDto()));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении питомцев врача с идентификатором {DoctorId}", doctorId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить питомца по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PetDto>> GetById(int id)
    {
        try
        {
            var pet = await repository.GetByIdAsync(id);
            if (pet is null)
            {
                return NotFound();
            }

            return Ok(pet.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении питомца с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Зарегистрировать нового питомца
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PetDto>> Create([FromBody] CreatePetDto dto)
    {
        try
        {
            var breed = await breedRepository.GetByIdAsync(dto.BreedId);
            var owner = await ownerRepository.GetByIdAsync(dto.OwnerId);

            if (breed is null || owner is null)
            {
                return BadRequest("Указанная порода или владелец не найдены");
            }

            var pet = new Pet
            {
                Name = dto.Name,
                Species = dto.Species!.Value,
                BreedId = dto.BreedId,
                Breed = breed,
                BirthDate = dto.BirthDate!.Value,
                WeightKg = dto.WeightKg,
                OwnerId = dto.OwnerId,
                Owner = owner
            };

            await repository.AddAsync(pet);
            owner.Pets.Add(pet);

            return CreatedAtAction(nameof(GetById), new { id = pet.Id }, pet.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при регистрации питомца");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить данные питомца
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] CreatePetDto dto)
    {
        try
        {
            var pet = await repository.GetByIdAsync(id);
            if (pet is null)
            {
                return NotFound();
            }

            var breed = await breedRepository.GetByIdAsync(dto.BreedId);
            var owner = await ownerRepository.GetByIdAsync(dto.OwnerId);

            if (breed is null || owner is null)
            {
                return BadRequest("Указанная порода или владелец не найдены");
            }

            pet.Name = dto.Name;
            pet.Species = dto.Species!.Value;
            pet.BreedId = dto.BreedId;
            pet.Breed = breed;
            pet.BirthDate = dto.BirthDate!.Value;
            pet.WeightKg = dto.WeightKg;
            pet.OwnerId = dto.OwnerId;
            pet.Owner = owner;

            await repository.UpdateAsync(pet);

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении питомца с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить питомца
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
            logger.LogError(ex, "Ошибка при удалении питомца с идентификатором {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}