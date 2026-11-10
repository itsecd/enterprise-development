using Microsoft.AspNetCore.Mvc;
using Vet.Contracts.Dtos;
using Vet.Contracts.Interfaces;

namespace Vet.Api.Controllers;

/// <summary>
/// Контроллер управления владельцами
/// </summary>
/// <param name="service">Сервис владельцев</param>
/// <param name="logger">Логгер контроллера</param>
[ApiController]
[Route("api/[controller]")]
public sealed class OwnersController(
    IApplicationService<OwnerDto, OwnerCreateUpdateDto, int> service,
    ILogger<OwnersController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех владельцев
    /// </summary>
    /// <returns>Список всех владельцев</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<OwnerDto>>> GetAll()
    {
        var owners = await service.GetAllAsync();
        return Ok(owners);
    }

    /// <summary>
    /// Получить владельца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор владельца</param>
    /// <returns>Данные владельца</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OwnerDto>> GetById(int id)
    {
        var owner = await service.GetByIdAsync(id);
        if (owner is null)
        {
            logger.LogWarning("Владелец с Id {Id} не найден", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Владелец с идентификатором {id} не найден"
            });
        }

        return Ok(owner);
    }

    /// <summary>
    /// Создать нового владельца
    /// </summary>
    /// <param name="dto">Данные для создания владельца</param>
    /// <returns>Созданный владелец</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OwnerDto>> Create([FromBody] OwnerCreateUpdateDto dto)
    {
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновить существующего владельца
    /// </summary>
    /// <param name="id">Идентификатор владельца</param>
    /// <param name="dto">Обновленные данные владельца</param>
    /// <returns>Пустой ответ при успешном обновлении</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] OwnerCreateUpdateDto dto)
    {
        var updated = await service.UpdateAsync(id, dto);
        if (!updated)
        {
            logger.LogWarning("Владелец с Id {Id} не найден для обновления", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Владелец с идентификатором {id} не найден"
            });
        }

        return NoContent();
    }

    /// <summary>
    /// Удалить владельца
    /// </summary>
    /// <param name="id">Идентификатор владельца</param>
    /// <returns>Пустой ответ при успешном удалении</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await service.DeleteAsync(id);
        if (!deleted)
        {
            logger.LogWarning("Владелец с Id {Id} не найден для удаления", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Владелец с идентификатором {id} не найден"
            });
        }

        return NoContent();
    }

    /// <summary>
    /// Получить всех питомцев конкретного владельца
    /// </summary>
    /// <param name="id">Идентификатор владельца</param>
    /// <param name="petService">Сервис питомцев</param>
    /// <returns>Список питомцев владельца</returns>
    [HttpGet("{id:int}/pets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> GetPetsByOwner(
        int id,
        [FromServices] IApplicationService<PetDto, PetCreateUpdateDto, int> petService)
    {
        var owner = await service.GetByIdAsync(id);
        if (owner is null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Владелец с идентификатором {id} не найден"
            });
        }

        var allPets = await petService.GetAllAsync();
        var ownerPets = allPets.Where(p => p.Owner.Id == id).ToList();
        return Ok(ownerPets);
    }
}