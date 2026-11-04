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
public class ServicesController(IServiceRepository serviceRepository) : ControllerBase
{
    /// <summary>
    /// Возвращает все услуги.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ServiceResponse>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ServiceResponse>> GetAll()
    {
        return Ok(serviceRepository.GetAll().Select(MapToResponse));
    }

    /// <summary>
    /// Возвращает услугу по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ServiceResponse> GetById(int id)
    {
        var service = serviceRepository.GetById(id);
        return service is null ? NotFound() : Ok(MapToResponse(service));
    }

    /// <summary>
    /// Создает новую услугу.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ServiceResponse> Create([FromBody] ServiceRequest request)
    {
        var service = new Service
        {
            Name = request.Name,
            Category = request.Category,
            Cost = request.Cost,
            Duration = request.Duration
        };

        serviceRepository.Add(service);
        
        return CreatedAtAction(nameof(GetById), new { id = service.Id }, MapToResponse(service));
    }

    /// <summary>
    /// Обновляет данные услуги по идентификатору.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] ServiceRequest request)
    {
        var service = serviceRepository.GetById(id);
        if (service is null) return NotFound();

        service.Name = request.Name;
        service.Category = request.Category;
        service.Cost = request.Cost;
        service.Duration = request.Duration;

        serviceRepository.Update(service);
        return NoContent();
    }

    /// <summary>
    /// Удаляет услугу по идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        var service = serviceRepository.GetById(id);
        
        if (service is not null)
        {
            serviceRepository.Delete(service);
        }

        return NoContent();
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