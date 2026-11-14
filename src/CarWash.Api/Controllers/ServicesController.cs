using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Контроллер для работы с услугами
/// </summary>
/// <param name="serviceService">Сервис для работы с услугами</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public class ServicesController(IServiceService serviceService, ILogger<ServicesController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех услуг
    /// </summary>
    /// <returns>Список услуг</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ServiceDto>))]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос списка услуг");
            return Ok(await serviceService.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка услуг");
            return Problem(detail: "Не удалось получить список услуг", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить услугу по ID
    /// </summary>
    /// <param name="id">ID услуги</param>
    /// <returns>Данные услуги</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(ServiceDto))]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ServiceDto>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос услуги с ID {Id}", id);
            var item = await serviceService.GetByIdAsync(id);
            if (item == null)
            {
                logger.LogWarning("Услуга с ID {Id} не найдена", id);
                return Problem(detail: $"Услуга с ID {id} не найдена", statusCode: StatusCodes.Status404NotFound);
            }

            return Ok(item);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении услуги с ID {Id}", id);
            return Problem(detail: "Не удалось получить услугу", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Создать услугу
    /// </summary>
    /// <param name="dto">Данные услуги</param>
    /// <returns>Созданный объект</returns>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(ServiceDto))]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceDto>> Create([FromBody] ServiceEditDto dto)
    {
        try
        {
            var created = await serviceService.CreateAsync(dto);
            logger.LogInformation("Создана услуга с ID {Id}", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании услуги");
            return Problem(detail: "Не удалось создать услугу", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Изменить услугу
    /// </summary>
    /// <param name="id">ID услуги</param>
    /// <param name="dto">Новые данные услуги</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] ServiceEditDto dto)
    {
        try
        {
            await serviceService.UpdateAsync(id, dto);
            logger.LogInformation("Обновлена услуга с ID {Id}", id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не удалось изменить услугу с ID {Id}: {Message}", id, ex.Message);
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при изменении услуги с ID {Id}", id);
            return Problem(detail: "Не удалось изменить услугу", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Удалить услугу
    /// </summary>
    /// <param name="id">ID услуги</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await serviceService.DeleteAsync(id);
            logger.LogInformation("Удалена услуга с ID {Id}", id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не удалось удалить услугу с ID {Id}: {Message}", id, ex.Message);
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении услуги с ID {Id}", id);
            return Problem(detail: "Не удалось удалить услугу", statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
