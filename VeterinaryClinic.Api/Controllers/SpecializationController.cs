using Microsoft.AspNetCore.Mvc;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы с специализациями
/// </summary>
[ApiController]
[Route("api/specializations")]
public sealed class SpecializationController(ISpecializationService service) : ControllerBase
{
    /// <summary>
    /// Получить все специализации
    /// </summary>
    [HttpGet]
    [EndpointSummary("Получить все специализации")]
    [ProducesResponseType(typeof(IReadOnlyList<SpecializationDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<SpecializationDto>> GetAll() => Ok(service.Specializations());

    /// <summary>
    /// Получить специализацию по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Получить специализацию по идентификатору")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<SpecializationDto> Get(int id)
    {
        var entity = service.Get(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    /// <summary>
    /// Создать специализацию
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Создать специализацию")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<SpecializationDto> Create(SaveSpecializationRequest request)
    {
        var entity = service.Create(request);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    /// <summary>
    /// Обновить специализацию
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Обновить специализацию")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<SpecializationDto> Update(int id, SaveSpecializationRequest request)
        => Ok(service.Update(id, request));

    /// <summary>
    /// Удалить специализацию
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Удалить специализацию")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        service.DeleteSpecialization(id);
        return NoContent();
    }

    /// <summary>
    /// Получить ветеринаров по специализации
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/veterinarians")]
    [EndpointSummary("Получить ветеринаров по специализации")]
    public ActionResult<IReadOnlyList<VeterinarianDto>> GetVeterinarians(int id)
        => Ok(service.VeterinariansBySpecialization(id));
}
