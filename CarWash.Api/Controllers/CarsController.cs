using System.ComponentModel.DataAnnotations;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Операции с коллекцией автомобилей
/// </summary>
[ApiController]
[Route("api/cars")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")]
public class CarsController(ICarService service, ILogger<CarsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список автомобилей
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<CarResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CarResponse>>> GetAll(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение списка автомобилей");
        return Ok(await service.GetAllAsync(cancellationToken));
    }

    /// <summary>
    /// Получить автомобиль по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<CarResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<CarResponse>> GetById([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение записи из коллекции автомобилей: {Id}", id);
        return Ok(await service.GetByIdAsync(id, cancellationToken));
    }

    /// <summary>
    /// Создать автомобиль
    /// </summary>
    [HttpPost]
    [ProducesResponseType<CarResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status415UnsupportedMediaType, "application/problem+json")]
    public async Task<ActionResult<CarResponse>> Create(CarRequest request, CancellationToken cancellationToken = default)
    {
        var response = await service.CreateAsync(request, cancellationToken);
        logger.LogInformation("Создана запись в коллекции автомобилей: {Id}", response.Id);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Изменить автомобиль
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status415UnsupportedMediaType, "application/problem+json")]
    public async Task<IActionResult> Update([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CarRequest request, CancellationToken cancellationToken = default)
    {
        await service.UpdateAsync(id, request, cancellationToken);
        logger.LogInformation("Изменена запись в коллекции автомобилей: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Удалить автомобиль
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict, "application/problem+json")]
    public async Task<IActionResult> Delete([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        await service.DeleteAsync(id, cancellationToken);
        logger.LogInformation("Удалена запись из коллекции автомобилей: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Получить владельца автомобиля
    /// </summary>
    [HttpGet("{id:int}/customer")]
    [ProducesResponseType<CustomerResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<CustomerResponse>> GetCustomer([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Запрос: владельца автомобиля; Id: {Id}", id);
        return Ok(await service.GetCustomerAsync(id, cancellationToken));
    }

    /// <summary>
    /// Получить заказы автомобиля
    /// </summary>
    [HttpGet("{id:int}/orders")]
    [ProducesResponseType<IReadOnlyList<OrderResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<IReadOnlyList<OrderResponse>>> GetOrders([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Запрос: заказы автомобиля; Id: {Id}", id);
        return Ok(await service.GetOrdersAsync(id, cancellationToken));
    }
}
