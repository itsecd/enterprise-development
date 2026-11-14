using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Контроллер для работы с клиентами и их автомобилями
/// </summary>
/// <param name="clientService">Сервис для работы с клиентами и их автомобилями</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public class ClientsController(IClientService clientService, ILogger<ClientsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех клиентов
    /// </summary>
    /// <returns>Список клиентов</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ClientDto>))]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос списка клиентов");
            return Ok(await clientService.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка клиентов");
            return Problem(detail: "Не удалось получить список клиентов", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить клиента по ID
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <returns>Данные клиента</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(ClientDto))]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос клиента с ID {Id}", id);
            var item = await clientService.GetByIdAsync(id);
            if (item == null)
            {
                logger.LogWarning("Клиент с ID {Id} не найден", id);
                return Problem(detail: $"Клиент с ID {id} не найден", statusCode: StatusCodes.Status404NotFound);
            }

            return Ok(item);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении клиента с ID {Id}", id);
            return Problem(detail: "Не удалось получить клиента", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Создать клиента
    /// </summary>
    /// <param name="dto">Данные клиента</param>
    /// <returns>Созданный объект</returns>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(ClientDto))]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientDto>> Create([FromBody] ClientEditDto dto)
    {
        try
        {
            var created = await clientService.CreateAsync(dto);
            logger.LogInformation("Создан клиент с ID {Id}", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании клиента");
            return Problem(detail: "Не удалось создать клиента", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Изменить клиента
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <param name="dto">Новые данные клиента</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] ClientEditDto dto)
    {
        try
        {
            await clientService.UpdateAsync(id, dto);
            logger.LogInformation("Обновлён клиент с ID {Id}", id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не удалось изменить клиента с ID {Id}: {Message}", id, ex.Message);
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при изменении клиента с ID {Id}", id);
            return Problem(detail: "Не удалось изменить клиента", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    /// <param name="id">ID клиента</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await clientService.DeleteAsync(id);
            logger.LogInformation("Удалён клиент с ID {Id}", id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не удалось удалить клиента с ID {Id}: {Message}", id, ex.Message);
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(ex, "Не удалось удалить клиента с ID {Id}: {Message}", id, ex.Message);
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status409Conflict);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении клиента с ID {Id}", id);
            return Problem(detail: "Не удалось удалить клиента", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить автомобили клиента
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <returns>Список автомобилей клиента</returns>
    [HttpGet("{id:int}/cars")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CarDto>))]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetCars(int id)
    {
        try
        {
            logger.LogInformation("Запрос автомобилей клиента с ID {Id}", id);
            var cars = await clientService.GetCarsAsync(id);
            if (cars == null)
            {
                logger.LogWarning("Клиент с ID {Id} не найден", id);
                return Problem(detail: $"Клиент с ID {id} не найден", statusCode: StatusCodes.Status404NotFound);
            }

            return Ok(cars);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении автомобилей клиента с ID {Id}", id);
            return Problem(detail: "Не удалось получить автомобили клиента", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Привязать автомобиль к клиенту
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <param name="carId">ID автомобиля</param>
    [HttpPost("{id:int}/cars/{carId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddCar(int id, int carId)
    {
        try
        {
            await clientService.AddCarAsync(id, carId);
            logger.LogInformation("Автомобиль с ID {CarId} привязан к клиенту с ID {Id}", carId, id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не удалось привязать автомобиль {CarId} к клиенту {Id}: {Message}", carId, id, ex.Message);
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(ex, "Не удалось привязать автомобиль {CarId} к клиенту {Id}: {Message}", carId, id, ex.Message);
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status409Conflict);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при привязке автомобиля {CarId} к клиенту {Id}", carId, id);
            return Problem(detail: "Не удалось привязать автомобиль к клиенту", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Отвязать автомобиль от клиента
    /// </summary>
    /// <param name="id">ID клиента</param>
    /// <param name="carId">ID автомобиля</param>
    [HttpDelete("{id:int}/cars/{carId:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveCar(int id, int carId)
    {
        try
        {
            await clientService.RemoveCarAsync(id, carId);
            logger.LogInformation("Автомобиль с ID {CarId} отвязан от клиента с ID {Id}", carId, id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не удалось отвязать автомобиль {CarId} от клиента {Id}: {Message}", carId, id, ex.Message);
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при отвязке автомобиля {CarId} от клиента {Id}", carId, id);
            return Problem(detail: "Не удалось отвязать автомобиль от клиента", statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
