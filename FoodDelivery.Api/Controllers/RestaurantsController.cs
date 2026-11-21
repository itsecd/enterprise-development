using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции для работы с ресторанами
/// </summary>
[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(
    IRestaurantService restaurantService,
    ILogger<RestaurantsController> logger)
    : ControllerBase
{
    /// <summary>
    /// Возвращает все рестораны
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IList<RestaurantDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<RestaurantDto>>> GetAll()
    {
        try
        {
            logger.LogInformation(
                "Получен запрос на получение всех ресторанов");

            var restaurants =
                await restaurantService.GetAllAsync();

            return Ok(restaurants);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при получении списка ресторанов");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Возвращает ресторан по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(RestaurantDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RestaurantDto>> GetById(int id)
    {
        try
        {
            var restaurant =
                await restaurantService.GetAsync(id);

            if (restaurant is null)
            {
                return NotFound(
                    new { message = "Ресторан не найден" });
            }

            return Ok(restaurant);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при получении ресторана {RestaurantId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Создаёт ресторан
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(RestaurantDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RestaurantDto>> Create(
        RestaurantCreateUpdateDto dto)
    {
        try
        {
            var restaurant =
                await restaurantService.CreateAsync(dto);

            logger.LogInformation(
                "Создан ресторан {RestaurantId}",
                restaurant.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = restaurant.Id },
                restaurant);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Некорректные данные при создании ресторана");

            return BadRequest(
                new { message = "Переданы некорректные данные" });
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при создании ресторана");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
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
    public async Task<ActionResult<RestaurantDto>> Update(
        int id,
        RestaurantCreateUpdateDto dto)
    {
        try
        {
            var existingRestaurant =
                await restaurantService.GetAsync(id);

            if (existingRestaurant is null)
            {
                return NotFound(
                    new { message = "Ресторан не найден" });
            }

            var updatedRestaurant =
                await restaurantService.UpdateAsync(dto, id);

            logger.LogInformation(
                "Обновлён ресторан {RestaurantId}",
                id);

            return Ok(updatedRestaurant);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Некорректные данные при обновлении ресторана {RestaurantId}",
                id);

            return BadRequest(
                new { message = "Переданы некорректные данные" });
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при обновлении ресторана {RestaurantId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Удаляет ресторан
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted =
                await restaurantService.DeleteAsync(id);

            if (deleted)
            {
                logger.LogInformation(
                    "Удалён ресторан {RestaurantId}",
                    id);
            }

            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при удалении ресторана {RestaurantId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }
}