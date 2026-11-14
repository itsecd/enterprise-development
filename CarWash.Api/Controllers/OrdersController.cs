using System.ComponentModel.DataAnnotations;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Операции с коллекцией заказов
/// </summary>
[ApiController]
[Route("api/orders")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")]
public class OrdersController(IOrderService service, ILogger<OrdersController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список заказов
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<OrderResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<OrderResponse>>> GetAll(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение списка заказов");
        return Ok(await service.GetAllAsync(cancellationToken));
    }

    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<OrderResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<OrderResponse>> GetById([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение записи из коллекции заказов: {Id}", id);
        return Ok(await service.GetByIdAsync(id, cancellationToken));
    }

    /// <summary>
    /// Создать заказ
    /// </summary>
    [HttpPost]
    [ProducesResponseType<OrderResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status415UnsupportedMediaType, "application/problem+json")]
    public async Task<ActionResult<OrderResponse>> Create(OrderRequest request, CancellationToken cancellationToken = default)
    {
        var response = await service.CreateAsync(request, cancellationToken);
        logger.LogInformation("Создана запись в коллекции заказов: {Id}", response.Id);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Изменить заказ
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status415UnsupportedMediaType, "application/problem+json")]
    public async Task<IActionResult> Update([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, OrderRequest request, CancellationToken cancellationToken = default)
    {
        await service.UpdateAsync(id, request, cancellationToken);
        logger.LogInformation("Изменена запись в коллекции заказов: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Удалить заказ
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<IActionResult> Delete([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        await service.DeleteAsync(id, cancellationToken);
        logger.LogInformation("Удалена запись из коллекции заказов: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Получить клиента заказа
    /// </summary>
    [HttpGet("{id:int}/customer")]
    [ProducesResponseType<CustomerResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<CustomerResponse>> GetCustomer([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Запрос: клиента заказа; Id: {Id}", id);
        return Ok(await service.GetCustomerAsync(id, cancellationToken));
    }

    /// <summary>
    /// Получить автомобиль заказа
    /// </summary>
    [HttpGet("{id:int}/car")]
    [ProducesResponseType<CarResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<CarResponse>> GetCar([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Запрос: автомобиль заказа; Id: {Id}", id);
        return Ok(await service.GetCarAsync(id, cancellationToken));
    }

    /// <summary>
    /// Получить услугу заказа
    /// </summary>
    [HttpGet("{id:int}/wash-service")]
    [ProducesResponseType<WashServiceResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<WashServiceResponse>> GetWashService([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Запрос: услугу заказа; Id: {Id}", id);
        return Ok(await service.GetWashServiceAsync(id, cancellationToken));
    }
}
