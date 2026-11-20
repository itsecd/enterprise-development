using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Pets;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы с породами
/// </summary>
[ApiController]
[Route("api/breeds")]
public sealed class BreedController(
    IBreedService service,
    ILogger<BreedController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все породы
    /// </summary>
    [HttpGet]
    [EndpointSummary("Get all breeds")]
    [ProducesResponseType(typeof(IReadOnlyList<BreedDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<BreedDto>>> GetAll()
    {
        logger.LogInformation("Получение списка пород");

        var result = await service.BreedsAsync(HttpContext.RequestAborted);

        logger.LogInformation("Получено пород: {Count}", result.Count);
        return Ok(result);
    }

    /// <summary>
    /// Получить породу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Get breed by ID")]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BreedDto>> Get(int id)
    {
        logger.LogInformation("Получение породы с Id {Id}", id);

        var result = await service.GetAsync(id, HttpContext.RequestAborted);

        if (result is null)
        {
            logger.LogWarning("Порода с Id {Id} не найдена", id);
            return NotFound();
        }

        return Ok(result);
    }

    /// <summary>
    /// Создать породу
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Create breed")]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BreedDto>> Create(SaveBreedRequest request)
    {
        logger.LogInformation("Создание новой породы");

        var result = await service.CreateAsync(
            request,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Порода с Id {Id} создана",
            result.Id);

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Id },
            result);
    }

    /// <summary>
    /// Обновить породу
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Update breed")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BreedDto>> Update(
        int id,
        SaveBreedRequest request)
    {
        logger.LogInformation(
            "Обновление породы с Id {Id}",
            id);

        var result = await service.UpdateAsync(
            id,
            request,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Порода с Id {Id} обновлена",
            id);

        return Ok(result);
    }

    /// <summary>
    /// Удалить породу
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Delete breed")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        logger.LogInformation(
            "Удаление породы с Id {Id}",
            id);

        await service.DeleteBreedAsync(
            id,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Порода с Id {Id} удалена",
            id);

        return NoContent();
    }

    /// <summary>
    /// Посчитать повторные приёмы по породе
    /// </summary>
    /// <param name="id">Идентификатор породы</param>
    [HttpGet("{id:int}/repeated-appointments/count")]
    [EndpointSummary("Count repeated appointments by breed")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> RepeatedCount(int id)
    {
        logger.LogInformation(
            "Получение количества повторных приёмов для породы с Id {Id}",
            id);

        var result = await service.RepeatedAppointmentsAsync(
            id,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Для породы с Id {Id} найдено повторных приёмов: {Count}",
            id,
            result);

        return Ok(result);
    }

    /// <summary>
    /// Получить питомцев по породе
    /// </summary>
    /// <param name="id">Идентификатор породы</param>
    [HttpGet("{id:int}/pets")]
    [EndpointSummary("Get pets by breed")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> GetPets(int id)
    {
        logger.LogInformation(
            "Получение питомцев породы с Id {Id}",
            id);

        var result = await service.PetsByBreedAsync(
            id,
            HttpContext.RequestAborted);

        logger.LogInformation(
            "Для породы с Id {Id} найдено питомцев: {Count}",
            id,
            result.Count);

        return Ok(result);
    }
}
