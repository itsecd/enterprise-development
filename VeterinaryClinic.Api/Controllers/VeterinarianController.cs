using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Api.Controllers;

/// <summary>
/// Контроллер для работы с ветеринарами
/// </summary>
[ApiController]
[Route("api/veterinarians")]
public sealed class VeterinarianController(IVeterinarianService service) : ControllerBase
{
    /// <summary>
    /// Получить всех ветеринаров
    /// </summary>
    [HttpGet]
    [EndpointSummary("Получить всех ветеринаров")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<VeterinarianDto>> GetAll() => Ok(service.Veterinarians());

    /// <summary>
    /// Получить ветеринара по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}")]
    [EndpointSummary("Получить ветеринара по идентификатору")]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VeterinarianDto> Get(int id)
    {
        var entity = service.Get(id);
        return entity is null ? NotFound() : Ok(entity);
    }

    /// <summary>
    /// Создать ветеринара
    /// </summary>
    /// <param name="request">Данные запроса</param>
    [HttpPost]
    [EndpointSummary("Создать ветеринара")]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<VeterinarianDto> Create(SaveVeterinarianRequest request)
    {
        var entity = service.Create(request);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    /// <summary>
    /// Обновить ветеринара
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Данные запроса</param>
    [HttpPut("{id:int}")]
    [EndpointSummary("Обновить ветеринара")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<VeterinarianDto> Update(int id, SaveVeterinarianRequest request)
        => Ok(service.Update(id, request));

    /// <summary>
    /// Удалить ветеринара
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpDelete("{id:int}")]
    [EndpointSummary("Удалить ветеринара")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        service.DeleteVeterinarian(id);
        return NoContent();
    }

    /// <summary>
    /// Получить ветеринаров по виду животного
    /// </summary>
    /// <param name="species">Вид животного</param>
    [HttpGet("by-species/{species}")]
    [EndpointSummary("Получить ветеринаров по виду животного")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<VeterinarianDto>> BySpecies(AnimalType species)
        => Ok(service.VeterinariansBySpecies(species));

    /// <summary>
    /// Получить приёмы ветеринара
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/appointments")]
    [EndpointSummary("Получить приёмы ветеринара")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IReadOnlyList<AppointmentDto>> GetAppointments(int id) =>
        Ok(service.AppointmentsByVeterinarian(id));

    /// <summary>
    /// Возвращает питомцев ветеринара без повторов
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    [HttpGet("{id:int}/pets")]
    [EndpointSummary("Получить питомцев ветеринара")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<PetDto>> PetsByDoctor(int id) => service.Get(id) is not null
        ? Ok(service.PetsByVeterinarian(id)) : NotFound();
}
