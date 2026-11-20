using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы со специализациями
/// </summary>
[ApiController]
[Route("api/specializations")]
public sealed class SpecializationController(
    ISpecializationService service,
    ILogger<SpecializationController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все специализации
    /// </summary>
    [HttpGet]
    [EndpointSummary("Get all specializations")]
    [ProducesResponseType(typeof(IReadOnlyList<SpecializationDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SpecializationDto>>> GetAll()
    {
        logger.LogInformation("Получение списка специализаций");

        var result = await service.SpecializationsAsync(
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Получено специализаций: {Count}",
            result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Получить специализацию по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get specialization by ID")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpecializationDto>> Get(int id)
    {
        logger.LogInformation(
            "Получение специализации с Id {Id}",
            id);

        var result = await service.GetAsync(
            id,
            HttpContext.RequestAborted);

        if (result is null)
        {
            logger.LogWarning(
                "Специализация с Id {Id} не найдена",
                id);

            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Создать специализацию
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Create specialization")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SpecializationDto>> Create(
        SaveSpecializationRequest request)
    {
        logger.LogInformation("Создание новой специализации");

        var result = await service.CreateAsync(
            request,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Специализация с Id {Id} создана",
            result.Id);

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Id },
            result);
    }

    /// <summary>
    /// Обновить специализацию
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Update specialization")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpecializationDto>> Update(
        int id,
        SaveSpecializationRequest request)
    {
        logger.LogInformation(
            "Обновление специализации с Id {Id}",
            id);

        var result = await service.UpdateAsync(
            id,
            request,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Специализация с Id {Id} обновлена",
            id);

        return Ok(result);
    }

    /// <summary>
    /// Удалить специализацию
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete specialization")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation(
            "Удаление специализации с Id {Id}",
            id);

        await service.DeleteSpecializationAsync(
            id,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Специализация с Id {Id} удалена",
            id);

        return NoContent();
    }

    /// <summary>
    /// Получить ветеринаров по специализации
    /// </summary>
    /// <param name="id">Идентификатор специализации</param>
    [HttpGet("{id:int}/veterinarians")]
    [EndpointSummary("Get veterinarians by specialization")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<VeterinarianDto>>> GetVeterinarians(int id)
    {
        logger.LogInformation(
            "Получение ветеринаров специализации с Id {Id}",
            id);

        var result = await service.VeterinariansBySpecializationAsync(
            id,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Для специализации с Id {Id} найдено ветеринаров: {Count}",
            id,
            result.Count);

        return Ok(result);
    }
}
