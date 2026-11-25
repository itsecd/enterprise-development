using System.ComponentModel.DataAnnotations;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Операции с коллекцией клиентов
/// </summary>
[ApiController]
[Route("api/customers")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")]
public class CustomersController(ICustomerService service, ILogger<CustomersController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список клиентов
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<CustomerResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CustomerResponse>>> GetAll(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение списка клиентов");
        return Ok(await service.GetAllAsync(cancellationToken));
    }

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<CustomerResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<CustomerResponse>> GetById([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение записи из коллекции клиентов: {Id}", id);
        return Ok(await service.GetByIdAsync(id, cancellationToken));
    }

    /// <summary>
    /// Создать клиента
    /// </summary>
    [HttpPost]
    [ProducesResponseType<CustomerResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status415UnsupportedMediaType, "application/problem+json")]
    public async Task<ActionResult<CustomerResponse>> Create(CustomerRequest request, CancellationToken cancellationToken = default)
    {
        var response = await service.CreateAsync(request, cancellationToken);
        logger.LogInformation("Создана запись в коллекции клиентов: {Id}", response.Id);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Изменить клиента
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status415UnsupportedMediaType, "application/problem+json")]
    public async Task<IActionResult> Update([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CustomerRequest request, CancellationToken cancellationToken = default)
    {
        await service.UpdateAsync(id, request, cancellationToken);
        logger.LogInformation("Изменена запись в коллекции клиентов: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict, "application/problem+json")]
    public async Task<IActionResult> Delete([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        await service.DeleteAsync(id, cancellationToken);
        logger.LogInformation("Удалена запись из коллекции клиентов: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Получить автомобили клиента
    /// </summary>
    [HttpGet("{id:int}/cars")]
    [ProducesResponseType<IReadOnlyList<CarResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<IReadOnlyList<CarResponse>>> GetCars([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Запрос: автомобили клиента; Id: {Id}", id);
        return Ok(await service.GetCarsAsync(id, cancellationToken));
    }

    /// <summary>
    /// Получить заказы клиента
    /// </summary>
    [HttpGet("{id:int}/orders")]
    [ProducesResponseType<IReadOnlyList<OrderResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<IReadOnlyList<OrderResponse>>> GetOrders([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Запрос: заказы клиента; Id: {Id}", id);
        return Ok(await service.GetOrdersAsync(id, cancellationToken));
    }
}
