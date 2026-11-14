using System.ComponentModel.DataAnnotations;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Операции с коллекцией услуг
/// </summary>
[ApiController]
[Route("api/wash-services")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")]
public class WashServicesController(IWashServiceService service, ILogger<WashServicesController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список услуг
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<WashServiceResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<WashServiceResponse>>> GetAll(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение списка услуг");
        return Ok(await service.GetAllAsync(cancellationToken));
    }

    /// <summary>
    /// Получить услугу по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<WashServiceResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<WashServiceResponse>> GetById([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение записи из коллекции услуг: {Id}", id);
        return Ok(await service.GetByIdAsync(id, cancellationToken));
    }

    /// <summary>
    /// Создать услугу
    /// </summary>
    [HttpPost]
    [ProducesResponseType<WashServiceResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status415UnsupportedMediaType, "application/problem+json")]
    public async Task<ActionResult<WashServiceResponse>> Create(WashServiceRequest request, CancellationToken cancellationToken = default)
    {
        var response = await service.CreateAsync(request, cancellationToken);
        logger.LogInformation("Создана запись в коллекции услуг: {Id}", response.Id);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Изменить услугу
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status415UnsupportedMediaType, "application/problem+json")]
    public async Task<IActionResult> Update([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, WashServiceRequest request, CancellationToken cancellationToken = default)
    {
        await service.UpdateAsync(id, request, cancellationToken);
        logger.LogInformation("Изменена запись в коллекции услуг: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Удалить услугу
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict, "application/problem+json")]
    public async Task<IActionResult> Delete([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        await service.DeleteAsync(id, cancellationToken);
        logger.LogInformation("Удалена запись из коллекции услуг: {Id}", id);
        return NoContent();
    }

    /// <summary>
    /// Получить заказы с услугой
    /// </summary>
    [HttpGet("{id:int}/orders")]
    [ProducesResponseType<IReadOnlyList<OrderResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<IReadOnlyList<OrderResponse>>> GetOrders([Range(1, int.MaxValue, ErrorMessage = "Идентификатор должен быть положительным.")] int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Запрос: заказы с услугой; Id: {Id}", id);
        return Ok(await service.GetOrdersAsync(id, cancellationToken));
    }
}
