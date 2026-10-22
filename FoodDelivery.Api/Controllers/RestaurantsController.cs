using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции для работы с ресторанами
/// </summary>
[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IRestaurantService restaurantService, ILogger<RestaurantsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает все рестораны
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IList<RestaurantDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<RestaurantDto>> GetAll()
    {
        try
        {
            logger.LogInformation("Получен запрос на получение всех ресторанов");
            return Ok(restaurantService.GetAll());
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при получении списка ресторанов");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Возвращает ресторан по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RestaurantDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<RestaurantDto> GetById(int id)
    {
        try
        {
            var restaurant = restaurantService.Get(id);

            if (restaurant is null)
            {
                return NotFound(new { message = "Ресторан не найден" });
            }

            return Ok(restaurant);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при получении ресторана {RestaurantId}", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Создаёт ресторан
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(RestaurantDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<RestaurantDto> Create(RestaurantCreateUpdateDto dto)
    {
        try
        {
            var restaurant = restaurantService.Create(dto);
            logger.LogInformation("Создан ресторан {RestaurantId}", restaurant.Id);
            return CreatedAtAction(nameof(GetById), new { id = restaurant.Id }, restaurant);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Некорректные данные при создании ресторана");
            return BadRequest(new { message = "Переданы некорректные данные" });
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при создании ресторана");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Обновляет ресторан
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(RestaurantDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<RestaurantDto> Update(int id, RestaurantCreateUpdateDto dto)
    {
        try
        {
            if (restaurantService.Get(id) is null)
            {
                return NotFound(new { message = "Ресторан не найден" });
            }

            return Ok(restaurantService.Update(dto, id));
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(exception, "Не найдена связанная сущность при обновлении ресторана {RestaurantId}", id);
            return BadRequest(new { message = "Невозможно обновить ресторан из-за отсутствующей связанной сущности" });
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Некорректные данные при обновлении ресторана {RestaurantId}", id);
            return BadRequest(new { message = "Переданы некорректные данные" });
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при обновлении ресторана {RestaurantId}", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Удаляет ресторан
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            var deleted = restaurantService.Delete(id);

            if (deleted)
            {
                logger.LogInformation("Удалён ресторан {RestaurantId}", id);
            }

            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при удалении ресторана {RestaurantId}", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }
}