using CarWash.Domain.Entities;
using CarWash.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController(InMemoryDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Car>> GetAll()
    {
        return Ok(dataStore.Cars);
    }

    [HttpGet("{id}")]
    public ActionResult<Car> GetById(int id)
    {
        var car = dataStore.Cars.FirstOrDefault(c => c.Id == id);
        if (car is null)
        {
            return NotFound();
        }
        return Ok(car);
    }

    [HttpPost]
    public ActionResult<Car> Create([FromBody] Car car)
    {
        car.Id = dataStore.Cars.Any() ? dataStore.Cars.Max(c => c.Id) + 1 : 1;
        dataStore.Cars.Add(car);

        return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Car updatedCar)
    {
        var car = dataStore.Cars.FirstOrDefault(c => c.Id == id);
        if (car is null)
        {
            return NotFound();
        }

        car.LicensePlate = updatedCar.LicensePlate;
        car.Brand = updatedCar.Brand;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var car = dataStore.Cars.FirstOrDefault(c => c.Id == id);
        if (car is null)
        {
            return NotFound();
        }

        dataStore.Cars.Remove(car);
        return NoContent();
    }
}