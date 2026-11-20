using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы с владельцами
/// </summary>
[ApiController]
[Route("api/owners")]
public sealed class OwnerController(
    IOwnerService service,
    ILogger<OwnerController> logger) : ControllerBase
{
    /// <summary>
    /// Получить всех владельцев
    /// </summary>
    [HttpGet]
    [EndpointSummary("Get all owners")]
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<OwnerDto>>> GetAll()
    {
        logger.LogInformation("Получение списка владельцев");

        var result = await service.OwnersAsync(
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Получено владельцев: {Count}",
            result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Получить владельца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get owner by ID")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OwnerDto>> Get(int id)
    {
        logger.LogInformation(
            "Получение владельца с Id {Id}",
            id);

        var result = await service.GetAsync(
            id,
            HttpContext.RequestAborted);

        if (result is null)
        {
            logger.LogWarning(
                "Владелец с Id {Id} не найден",
                id);

            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Создать владельца
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Create owner")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OwnerDto>> Create(SaveOwnerRequest request)
    {
        logger.LogInformation("Создание нового владельца");

        var result = await service.CreateAsync(
            request,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Владелец с Id {Id} создан",
            result.Id);

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Id },
            result);
    }

    /// <summary>
    /// Обновить владельца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Update owner")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OwnerDto>> Update(
        int id,
        SaveOwnerRequest request)
    {
        logger.LogInformation(
            "Обновление владельца с Id {Id}",
            id);

        var result = await service.UpdateAsync(
            id,
            request,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Владелец с Id {Id} обновлён",
            id);

        return Ok(result);
    }

    /// <summary>
    /// Удалить владельца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete owner")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation(
            "Удаление владельца с Id {Id}",
            id);

        await service.DeleteOwnerAsync(
            id,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Удаление владельца с Id {Id} завершено",
            id);

        return NoContent();
    }

    /// <summary>
    /// Получить владельцев нескольких питомцев
    /// </summary>
    [HttpGet("multiple-pets")]
    [EndpointSummary("Get owners with multiple pets")]
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<OwnerDto>>> MultiplePets()
    {
        logger.LogInformation(
            "Получение владельцев с несколькими питомцами");

        var result = await service.OwnersWithMultiplePetsAsync(
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Найдено владельцев с несколькими питомцами: {Count}",
            result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Получить питомцев владельца
    /// </summary>
    /// <param name="id">Идентификатор владельца</param>
    [HttpGet("{id:int}/pets")]
    [EndpointSummary("Get pets by owner")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> GetPets(int id)
    {
        logger.LogInformation(
            "Получение питомцев владельца с Id {Id}",
            id);

        var result = await service.PetsByOwnerAsync(
            id,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Для владельца с Id {Id} найдено питомцев: {Count}",
            id,
            result.Count);

        return Ok(result);
    }
}
