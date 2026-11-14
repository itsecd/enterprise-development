using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Application.Services;
using FoodDelivery.Contracts.Restaurants;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Контроллер для управления ресторанами-партнёрами.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RestaurantsController(IRestaurantService restaurantService, ILogger<RestaurantsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех ресторанов.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<RestaurantDto>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос списка всех ресторанов");
            return Ok(restaurantService.GetAll());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка ресторанов");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Получить ресторан по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<RestaurantDto> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос ресторана с Id = {Id}", id);
            var restaurant = restaurantService.GetById(id);
            if (restaurant is null)
            {
                logger.LogWarning("Ресторан с Id = {Id} не найден", id);
                return NotFound(new { Message = $"Ресторан с Id = {id} не найден." });
            }
            return Ok(restaurant);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении ресторана с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Добавить новый ресторан.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<RestaurantDto> Create([FromBody] RestaurantCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Создание нового ресторана: {Name}", dto.Name);
            var created = restaurantService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании ресторана");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Обновить данные ресторана.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(int id, [FromBody] RestaurantCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Обновление ресторана с Id = {Id}", id);
            if (!restaurantService.Update(id, dto))
            {
                logger.LogWarning("Ресторан с Id = {Id} не найден для обновления", id);
                return NotFound(new { Message = $"Ресторан с Id = {id} не найден." });
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении ресторана с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Удалить ресторан по идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            logger.LogInformation("Удаление ресторана с Id = {Id}", id);
            restaurantService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении ресторана с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }
}