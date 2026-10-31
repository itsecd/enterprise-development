using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController(ICarService carService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CarDto>))]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetAll()
    {
        return Ok(await carService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(CarDto))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<CarDto>> GetById(int id)
    {
        var car = await carService.GetByIdAsync(id);
        return car == null ? NotFound() : Ok(car);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(CarDto))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<CarDto>> Create([FromBody] CarEditDto dto)
    {
        var created = await carService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] CarEditDto dto)
    {
        try
        {
            await carService.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        await carService.DeleteAsync(id);
        return NoContent();
    }
}