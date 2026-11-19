using AutoService.Contracts.Dtos;
using AutoService.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoService.Api.Controllers;

/// <summary>
/// Контроллер для управления автомобилями.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CarsController(ICarService carService) : ControllerBase
{
    /// <summary>
    /// Получить список всех автомобилей.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetAll()
    {
        var cars = await carService.GetAllAsync();
        return Ok(cars);
    }

    /// <summary>
    /// Получить автомобиль по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<CarDto>> GetById(Guid id)
    {
        var car = await carService.GetByIdAsync(id);
        if (car == null) return NotFound();
        return Ok(car);
    }

    /// <summary>
    /// Добавить новый автомобиль.
    /// </summary>
    /// <param name="newCar">Данные нового автомобиля.</param>
    [HttpPost]
    public async Task<ActionResult<CarDto>> Create([FromBody] CarDto newCar)
    {
        var created = await carService.CreateAsync(newCar);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновить данные существующего автомобиля.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    /// <param name="updatedCar">Новые данные автомобиля.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CarUpdateDto updatedCar)
    {
        await carService.UpdateAsync(id, updatedCar);
        return NoContent();
    }

    /// <summary>
    /// Удалить автомобиль по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор автомобиля.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await carService.DeleteAsync(id);
        return NoContent();
    }
}