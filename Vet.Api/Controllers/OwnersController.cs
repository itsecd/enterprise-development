using Microsoft.AspNetCore.Mvc;
using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;

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
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение всех владельцев");
            var owners = await service.GetAllAsync();
            return Ok(owners);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении всех владельцев");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Получить владельца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор владельца</param>
    /// <returns>Данные владельца</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос владельца с Id {Id}", id);
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении владельца с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Создать нового владельца
    /// </summary>
    /// <param name="dto">Данные для создания владельца</param>
    /// <returns>Созданный владелец</returns>
    [HttpPost]
    [ProducesResponseType(typeof(OwnerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] OwnerCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Создание нового владельца {FullName}", dto.FullName);
            var created = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Ошибка валидации при создании владельца");
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректный запрос"
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при создании владельца");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
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
        try
        {
            logger.LogInformation("Обновление владельца с Id {Id}", id);
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
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Ошибка валидации при обновлении владельца с Id {Id}", id);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректный запрос"
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при обновлении владельца с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
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
        try
        {
            logger.LogInformation("Удаление владельца с Id {Id}", id);
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при удалении владельца с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Получить всех питомцев конкретного владельца
    /// </summary>
    /// <param name="id">Идентификатор владельца</param>
    /// <param name="petService">Сервис питомцев</param>
    /// <returns>Список питомцев владельца</returns>
    [HttpGet("{id:int}/pets")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPetsByOwner(
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