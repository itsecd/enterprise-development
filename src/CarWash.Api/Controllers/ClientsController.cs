using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Контроллер для работы с клиентами и их автомобилями
/// </summary>
/// <param name="clientService">Сервис клиентов</param>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    /// <summary>
    /// Получить список всех клиентов
    /// </summary>
    /// <returns>Список клиентов</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ClientDto>))]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
    {
        return Ok(await clientService.GetAllAsync());
    }

    /// <summary>
    /// Получить клиента по ID
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <returns>Данные клиента</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(ClientDto))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        var client = await clientService.GetByIdAsync(id);
        return client == null ? NotFound() : Ok(client);
    }

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    /// <param name="dto">Данные клиента</param>
    /// <returns>Созданный клиент</returns>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(ClientDto))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ClientDto>> Create([FromBody] ClientEditDto dto)
    {
        var created = await clientService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Изменить данные клиента
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <param name="dto">Новые данные клиента</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] ClientEditDto dto)
    {
        try
        {
            await clientService.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    /// <param name="id">ID клиента</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        await clientService.DeleteAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Получить автомобили клиента
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <returns>Список автомобилей клиента</returns>
    [HttpGet("{id:int}/cars")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CarDto>))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetCars(int id)
    {
        var cars = await clientService.GetCarsAsync(id);
        return cars == null ? NotFound() : Ok(cars);
    }

    /// <summary>
    /// Привязать автомобиль к клиенту
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <param name="carId">ID автомобиля</param>
    [HttpPost("{id:int}/cars/{carId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> AddCar(int id, int carId)
    {
        try
        {
            await clientService.AddCarAsync(id, carId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    /// <summary>
    /// Отвязать автомобиль от клиента
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <param name="carId">ID автомобиля</param>
    [HttpDelete("{id:int}/cars/{carId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> RemoveCar(int id, int carId)
    {
        try
        {
            await clientService.RemoveCarAsync(id, carId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
