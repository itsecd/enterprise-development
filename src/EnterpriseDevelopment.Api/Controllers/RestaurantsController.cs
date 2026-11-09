using EnterpriseDevelopment.Application.Interfaces;
using EnterpriseDevelopment.Contracts.DTOs.Restaurants;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseDevelopment.Api.Controllers;

/// <summary>
/// Контроллер для управления ресторанами
/// </summary>
[ApiController]
[Route("restaurants")]
public class RestaurantsController(
    IRestaurantService service,
    ILogger<RestaurantsController> logger) : ControllerBase
{
    /// <summary>
    /// Получает все рестораны
    /// </summary>
    /// <returns>Список ресторанов</returns>
    [HttpGet]
    public ActionResult<IEnumerable<RestaurantDto>> GetAll()
    {
        logger.LogInformation("Запрошен список ресторанов");

        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получает ресторан по идентификатору
    /// </summary>
    /// <returns>Данные ресторана</returns>
    [HttpGet("{id:int}")]
    public ActionResult<RestaurantDto> GetById(int id)
    {
        logger.LogInformation("Запрошен ресторан с ID {RestaurantId}", id);

        var restaurant = service.GetById(id);

        if (restaurant is null)
        {
            logger.LogWarning("Ресторан с ID {RestaurantId} не найден", id);
            return NotFound();
        }

        return Ok(restaurant);
    }

    /// <summary>
    /// Создаёт ресторан
    /// </summary>
    /// <returns>Данные созданного ресторана</returns>
    [HttpPost]
    public ActionResult<RestaurantDto> Create(CreateRestaurantDto dto)
    {
        var restaurant = service.Create(dto);

        logger.LogInformation(
            "Создан ресторан с ID {RestaurantId}",
            restaurant.Id);

        return CreatedAtAction(
            nameof(GetById),
            new { id = restaurant.Id },
            restaurant);
    }

    /// <summary>
    /// Обновляет ресторан
    /// </summary>
    /// <returns>Результат обновления</returns>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UpdateRestaurantDto dto)
    {
        var updated = service.Update(id, dto);

        if (!updated)
        {
            logger.LogWarning("Ресторан с ID {RestaurantId} не найден", id);
            return NotFound();
        }

        logger.LogInformation("Обновлён ресторан с ID {RestaurantId}", id);

        return NoContent();
    }

    /// <summary>
    /// Удаляет ресторан
    /// </summary>
    /// <returns>Результат удаления</returns>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        service.Delete(id);

        logger.LogInformation("Обработано удаление ресторана с ID {RestaurantId}", id);

        return NoContent();
    }
}
