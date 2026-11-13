using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции для работы с покупателями
/// </summary>
[ApiController]
[Route("api/customers")]
public class CustomersController(
    ICustomerService customerService,
    ILogger<CustomersController> logger)
    : ControllerBase
{
    /// <summary>
    /// Возвращает всех покупателей
    /// </summary>
    [HttpGet]
    [ProducesResponseType(
        typeof(IList<CustomerDto>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<CustomerDto>>> GetAll()
    {
        try
        {
            logger.LogInformation(
                "Получен запрос на получение всех покупателей");

            var customers = await customerService.GetAllAsync();

            return Ok(customers);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при получении списка покупателей");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Возвращает покупателя по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(
        typeof(CustomerDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerDto>> GetById(int id)
    {
        try
        {
            var customer = await customerService.GetAsync(id);

            if (customer is null)
            {
                return NotFound(new
                {
                    message = "Покупатель не найден"
                });
            }

            return Ok(customer);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при получении покупателя {CustomerId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Создаёт покупателя
    /// </summary>
    [HttpPost]
    [ProducesResponseType(
        typeof(CustomerDto),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerDto>> Create(
        CustomerCreateUpdateDto dto)
    {
        try
        {
            var customer =
                await customerService.CreateAsync(dto);

            logger.LogInformation(
                "Создан покупатель {CustomerId}",
                customer.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = customer.Id },
                customer);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Некорректные данные при создании покупателя");

            return BadRequest(new
            {
                message = "Переданы некорректные данные"
            });
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при создании покупателя");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Обновляет данные покупателя
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(
        typeof(CustomerDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerDto>> Update(
        int id,
        CustomerCreateUpdateDto dto)
    {
        try
        {
            var existingCustomer =
                await customerService.GetAsync(id);

            if (existingCustomer is null)
            {
                return NotFound(new
                {
                    message = "Покупатель не найден"
                });
            }

            var updatedCustomer =
                await customerService.UpdateAsync(dto, id);

            return Ok(updatedCustomer);
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(
                exception,
                "Не найдена связанная сущность при обновлении покупателя {CustomerId}",
                id);

            return BadRequest(new
            {
                message =
                    "Невозможно обновить покупателя из-за отсутствующей связанной сущности"
            });
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Некорректные данные при обновлении покупателя {CustomerId}",
                id);

            return BadRequest(new
            {
                message = "Переданы некорректные данные"
            });
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при обновлении покупателя {CustomerId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Удаляет покупателя
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted =
                await customerService.DeleteAsync(id);

            if (deleted)
            {
                logger.LogInformation(
                    "Удалён покупатель {CustomerId}",
                    id);
            }

            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при удалении покупателя {CustomerId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }
}