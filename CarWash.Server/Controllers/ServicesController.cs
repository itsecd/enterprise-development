using CarWash.Domain.Entities;
using CarWash.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController(InMemoryDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Service>> GetAll() => Ok(dataStore.Services);

    [HttpGet("{id}")]
    public ActionResult<Service> GetById(int id)
    {
        var service = dataStore.Services.FirstOrDefault(s => s.Id == id);
        return service is null ? NotFound() : Ok(service);
    }

    [HttpPost]
    public ActionResult<Service> Create([FromBody] Service service)
    {
        service.Id = dataStore.Services.Any() ? dataStore.Services.Max(s => s.Id) + 1 : 1;
        dataStore.Services.Add(service);
        return CreatedAtAction(nameof(GetById), new { id = service.Id }, service);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Service updatedService)
    {
        var service = dataStore.Services.FirstOrDefault(s => s.Id == id);
        if (service is null) return NotFound();

        service.Name = updatedService.Name;
        service.Category = updatedService.Category;
        service.Price = updatedService.Price;
        service.DurationMinutes = updatedService.DurationMinutes;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var service = dataStore.Services.FirstOrDefault(s => s.Id == id);
        if (service is null) return NotFound();

        dataStore.Services.Remove(service);
        return NoContent();
    }
}