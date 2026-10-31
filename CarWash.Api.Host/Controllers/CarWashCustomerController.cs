using CarWash.Application.Contracts.Cars;
using CarWash.Application.Contracts.Customers;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Host.Controllers;

/// <summary>
/// Контроллер CRUD-операций над клиентами автомойки
/// </summary>
public class CarWashCustomerController(
    ICarWashCustomerService crudService,
    ILogger<CarWashCustomerController> logger)
    : CrudControllerBase<
        CarWashCustomerDto,
        CarWashCustomerCreateUpdateDto,
        int>(crudService, logger)
{
    /// <summary>
    /// Получение списка автомобилей клиента
    /// </summary>
    [HttpGet("{id}/cars")]
    [ProducesResponseType(typeof(List<CarDto>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult<List<CarDto>> GetCustomerCars(int id)
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller} с параметром {id}",
            nameof(GetCustomerCars), GetType().Name, id);

        try
        {
            var result = crudService.GetCustomerCars(id);

            logger.LogInformation(
                "Метод {method} контроллера {controller} успешно выполнен",
                nameof(GetCustomerCars), GetType().Name);

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(
                ex,
                "Клиент с идентификатором {id} не найден",
                id);

            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при выполнении метода {method} контроллера {controller}",
                nameof(GetCustomerCars), GetType().Name);

            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}