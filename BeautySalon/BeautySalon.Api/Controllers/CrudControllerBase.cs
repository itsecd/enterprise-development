using BeautySalon.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Базовый контроллер для CRUD-операций над сущностями
/// </summary>
/// <typeparam name="TDto">Dto для get запросов</typeparam>
/// <typeparam name="TCreateUpdateDto">Dto для put и post запросов</typeparam>
/// <typeparam name="TKey">Тип Id</typeparam>
/// <param name="applicationService">Служба для CRUD операций с Dto</param>
/// <param name="logger">Логгер</param>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationService<TDto, TCreateUpdateDto, TKey> applicationService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger) : ControllerBase
{
    /// <summary>
    /// Получение сущности
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Полученная сущность</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Get(TKey id)
    {
        try
        {
            var result = await applicationService.Get(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting entity.");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Получение всех сущностей
    /// </summary>
    /// <returns>Список полученных сущностей</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
    {
        try
        {
            var result = await applicationService.GetAll();

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting all entities.");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Создание сущности
    /// </summary>
    /// <param name="dto">Dto</param>
    /// <returns>Созданная сущность</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Create(TCreateUpdateDto dto)
    {
        try
        {
            var result = await applicationService.Create(dto);

            return CreatedAtAction(nameof(Create), result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating entity.");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Обновление сущности
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="dto">Dto</param>
    /// <returns>Обновленная сущность</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Update(TKey id, TCreateUpdateDto dto)
    {
        try
        {
            var result = await applicationService.Update(dto, id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating entity.");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Удаление сущности
    /// </summary>
    /// <param name="id">Id</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(TKey id)
    {
        try
        {
            await applicationService.Delete(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting entity.");
            return StatusCode(500, "Internal server error");
        }
    }
}
