using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы с питомцами
/// </summary>
[ApiController]
[Route("api/pets")]
public sealed class PetController(
    IPetService service,
    ILogger<PetController> logger) : ControllerBase
{
    /// <summary>
    /// Получить всех питомцев
    /// </summary>
    [HttpGet]
    [EndpointSummary("Get all pets")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> GetAll()
    {
        logger.LogInformation("Получение списка питомцев");

        var result = await service.PetsAsync(
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Получено питомцев: {Count}",
            result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Получить питомца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get pet by ID")]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetDto>> Get(int id)
    {
        logger.LogInformation(
            "Получение питомца с Id {Id}",
            id);

        var result = await service.GetAsync(
            id,
            HttpContext.RequestAborted);

        if (result is null)
        {
            logger.LogWarning(
                "Питомец с Id {Id} не найден",
                id);

            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Создать питомца
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Create pet")]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PetDto>> Create(SavePetRequest request)
    {
        logger.LogInformation("Создание нового питомца");

        var result = await service.CreateAsync(
            request,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Питомец с Id {Id} создан",
            result.Id);

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Id },
            result);
    }

    /// <summary>
    /// Обновить питомца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Update pet")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PetDto>> Update(
        int id,
        SavePetRequest request)
    {
        logger.LogInformation(
            "Обновление питомца с Id {Id}",
            id);

        var result = await service.UpdateAsync(
            id,
            request,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Питомец с Id {Id} обновлён",
            id);

        return Ok(result);
    }

    /// <summary>
    /// Удалить питомца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete pet")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation(
            "Удаление питомца с Id {Id}",
            id);

        await service.DeletePetAsync(
            id,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Питомец с Id {Id} удалён",
            id);

        return NoContent();
    }

    /// <summary>
    /// Возвращает приёмы питомца
    /// </summary>
    /// <param name="id">Идентификатор питомца</param>
    [HttpGet("{id:int}/appointments")]
    [EndpointSummary("Get appointments by pet")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAppointments(int id)
    {
        logger.LogInformation(
            "Получение приёмов питомца с Id {Id}",
            id);

        var result = await service.AppointmentsByPetAsync(
            id,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Для питомца с Id {Id} найдено приёмов: {Count}",
            id,
            result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Возвращает владельца питомца
    /// </summary>
    /// <param name="id">Идентификатор питомца</param>
    [HttpGet("{id:int}/owner")]
    [EndpointSummary("Get owner of pet")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OwnerDto>> GetOwner(int id)
    {
        logger.LogInformation(
            "Получение владельца питомца с Id {Id}",
            id);

        var owner = await service.OwnerByPetAsync(
            id,
            HttpContext.RequestAborted);

        if (owner is null)
        {
            logger.LogWarning(
                "Владелец питомца с Id {Id} не найден",
                id);

            return NotFound();
        }

        return Ok(owner);
    }
}
