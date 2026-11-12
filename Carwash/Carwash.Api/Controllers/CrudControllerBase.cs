using Microsoft.AspNetCore.Mvc;
using Carwash.Application.Contracts;

namespace Carwash.Api.Controllers;

/// <summary>
/// Базовый контроллер для CRUD-операций над сущностями
/// </summary>
/// <typeparam name="TDto">Dto для Get запросов</typeparam>
/// <typeparam name="TCreateUpdateDto">Dto для Create/Update запросов</typeparam>
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
    /// Получение объекта по Id
    /// </summary>
    /// <param name="id">Id объекта</param>
    /// <returns>Полученный объект</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Get(TKey id)
    {
        try
        {
            var result = await applicationService.Read(id);
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting entity by ID.");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Получение списка всех объектов
    /// </summary>
    /// <returns>Список всех объектов</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
    {
        try
        {
            var result = await applicationService.ReadAll();
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting all entities.");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Создание объекта
    /// </summary>
    /// <param name="dto">Dto</param>
    /// <returns>Созданный объект</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Create(TCreateUpdateDto dto)
    {
        try
        {
            var result = await applicationService.Create(dto);
            return result == null ? BadRequest() : CreatedAtAction(nameof(Get), result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating entity.");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Обновление объекта
    /// </summary>
    /// <param name="id">Id объекта</param>
    /// <param name="dto">Dto</param>
    /// <returns>Обновленный объект</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Update(TKey id, TCreateUpdateDto dto)
    {
        try
        {
            var result = await applicationService.Update(dto, id);
            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating entity.");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Удаление объекта
    /// </summary>
    /// <param name="id">Id объекта</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(TKey id)
    {
        try
        {
            var result = await applicationService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting entity.");
            return StatusCode(500, "Internal server error");
        }
    }
}
