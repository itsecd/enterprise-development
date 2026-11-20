using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Veterinarians;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы с ветеринарами
/// </summary>
[ApiController]
[Route("api/veterinarians")]
public sealed class VeterinarianController(
    IVeterinarianService service,
    ILogger<VeterinarianController> logger) : ControllerBase
{
    /// <summary>
    /// Получить всех ветеринаров
    /// </summary>
    [HttpGet]
    [EndpointSummary("Get all veterinarians")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<VeterinarianDto>>> GetAll()
    {
        logger.LogInformation("Получение списка ветеринаров");

        var result = await service.VeterinariansAsync(
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Получено ветеринаров: {Count}",
            result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Получить ветеринара по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get veterinarian by ID")]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VeterinarianDto>> Get(int id)
    {
        logger.LogInformation(
            "Получение ветеринара с Id {Id}",
            id);

        var result = await service.GetAsync(
            id,
            HttpContext.RequestAborted);

        if (result is null)
        {
            logger.LogWarning(
                "Ветеринар с Id {Id} не найден",
                id);

            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Создать ветеринара
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Create veterinarian")]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VeterinarianDto>> Create(
        SaveVeterinarianRequest request)
    {
        logger.LogInformation("Создание нового ветеринара");

        var result = await service.CreateAsync(
            request,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Ветеринар с Id {Id} создан",
            result.Id);

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Id },
            result);
    }

    /// <summary>
    /// Обновить ветеринара
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Update veterinarian")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VeterinarianDto>> Update(
        int id,
        SaveVeterinarianRequest request)
    {
        logger.LogInformation(
            "Обновление ветеринара с Id {Id}",
            id);

        var result = await service.UpdateAsync(
            id,
            request,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Ветеринар с Id {Id} обновлён",
            id);

        return Ok(result);
    }

    /// <summary>
    /// Удалить ветеринара
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete veterinarian")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation(
            "Удаление ветеринара с Id {Id}",
            id);

        await service.DeleteVeterinarianAsync(
            id,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Ветеринар с Id {Id} удалён",
            id);

        return NoContent();
    }

    /// <summary>
    /// Получить ветеринаров по виду животного
    /// </summary>
    /// <param name="species">Вид животного</param>
    [HttpGet("by-species/{species}")]
    [EndpointSummary("Get veterinarians by animal species")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<VeterinarianDto>>> BySpecies(
        AnimalType species)
    {
        logger.LogInformation(
            "Получение ветеринаров по виду животного {Species}",
            species);

        var result = await service.VeterinariansBySpeciesAsync(
            species,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Для вида животного {Species} найдено ветеринаров: {Count}",
            species,
            result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Получить приёмы ветеринара
    /// </summary>
    /// <param name="id">Идентификатор ветеринара</param>
    [HttpGet("{id:int}/appointments")]
    [EndpointSummary("Get appointments by veterinarian")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAppointments(int id)
    {
        logger.LogInformation(
            "Получение приёмов ветеринара с Id {Id}",
            id);

        var result = await service.AppointmentsByVeterinarianAsync(
            id,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Для ветеринара с Id {Id} найдено приёмов: {Count}",
            id,
            result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Получить питомцев ветеринара без повторов
    /// </summary>
    /// <param name="id">Идентификатор ветеринара</param>
    [HttpGet("{id:int}/pets")]
    [EndpointSummary("Get pets by veterinarian")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> PetsByDoctor(int id)
    {
        logger.LogInformation(
            "Получение питомцев ветеринара с Id {Id}",
            id);

        var veterinarian = await service.GetAsync(
            id,
            HttpContext.RequestAborted);

        if (veterinarian is null)
        {
            logger.LogWarning(
                "Ветеринар с Id {Id} не найден",
                id);

            return NotFound();
        }

        var result = await service.PetsByVeterinarianAsync(
            id,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Для ветеринара с Id {Id} найдено питомцев: {Count}",
            id,
            result.Count);

        return Ok(result);
    }
}
