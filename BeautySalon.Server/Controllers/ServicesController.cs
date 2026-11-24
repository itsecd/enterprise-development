using BeautySalon.Contracts.Services;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Server.Controllers;

/// <summary>
/// CRUD-операции над услугами салона.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ServicesController(IServiceRepository serviceRepository, ILogger<ServicesController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает все услуги.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ServiceResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ServiceResponse>>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение списка всех услуг.");
            var items = await serviceRepository.GetAllAsync(); 
            return Ok(items.Select(MapToResponse));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка услуг.");
            return Problem("Внутренняя ошибка сервера при получении услуг.");
        }
    }

    /// <summary>
    /// Возвращает услугу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceResponse>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос на получение услуги с ID: {ServiceId}", id);
            var service = await serviceRepository.GetByIdAsync(id);
            if (service is null)
            {
                logger.LogWarning("Услуга с ID {ServiceId} не найдена.", id);
                return NotFound();
            }
            return Ok(MapToResponse(service));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении услуги с ID: {ServiceId}", id);
            return Problem("Внутренняя ошибка сервера при получении услуги.");
        }
    }

    /// <summary>
    /// Создает новую услугу.
    /// </summary>
    /// <param name="request">Данные новой услуги.</param>
    [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ServiceResponse>> Create([FromBody] ServiceRequest request)
    {
        try
        {
            logger.LogInformation("Запрос на создание новой услуги: {Name}", request.Name);
            
            var service = new Service
            {
                Name = request.Name,
                Category = request.Category,
                Cost = request.Cost,
                Duration = request.Duration
            };

            await serviceRepository.AddAsync(service);
            
            logger.LogInformation("Услуга успешно создана с ID: {ServiceId}", service.Id);
            return CreatedAtAction(nameof(GetById), new { id = service.Id }, MapToResponse(service));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании услуги: {Name}", request.Name);
            return Problem("Внутренняя ошибка сервера при создании услуги.");
        }
    }

    /// <summary>
    /// Обновляет данные услуги по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    /// <param name="request">Новые данные услуги.</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] ServiceRequest request)
    {
        try
        {
            logger.LogInformation("Запрос на обновление услуги с ID: {ServiceId}", id);
            
            var service =  await serviceRepository.GetByIdAsync(id);
            if (service is null)
            {
                logger.LogWarning("Услуга с ID {ServiceId} не найдена для обновления.", id);
                return NotFound();
            }

            service.Name = request.Name;
            service.Category = request.Category;
            service.Cost = request.Cost;
            service.Duration = request.Duration;

            await serviceRepository.UpdateAsync(service);
            
            logger.LogInformation("Услуга с ID {ServiceId} успешно обновлена.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении услуги с ID: {ServiceId}", id);
            return Problem("Внутренняя ошибка сервера при обновлении услуги.");
        }
    }

    /// <summary>
    /// Удаляет услугу по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор услуги.</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            logger.LogInformation("Запрос на удаление услуги с ID: {ServiceId}", id);
            
            var service = await serviceRepository.GetByIdAsync(id);
            if (service is not null)
            {
                await serviceRepository.DeleteAsync(service);
                logger.LogInformation("Услуга с ID {ServiceId} успешно удалена.", id);
            }
            else
            {
                logger.LogWarning("Услуга с ID {ServiceId} не найдена (удаление пропущено).", id);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении услуги с ID: {ServiceId}", id);
            return Problem("Внутренняя ошибка сервера при удалении услуги.");
        }
    }

    private static ServiceResponse MapToResponse(Service service) => new()
    {
        Id = service.Id,
        Name = service.Name,
        Category = service.Category,
        Cost = service.Cost,
        Duration = service.Duration
    };
}