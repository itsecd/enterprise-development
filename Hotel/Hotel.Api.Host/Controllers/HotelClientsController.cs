using Hotel.Application.Contracts.Bookings;
using Hotel.Application.Contracts.HotelClients;
using Hotel.Domain.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления клиентами и получения статистики по их бронированиям
/// </summary>
public class HotelClientsController(IHotelClientService hotelClientService, ILogger<HotelClientsController> logger)
    : CrudControllerBase<HotelClientDto, CreateHotelClientDto, int>(hotelClientService, logger)
{
    /// <summary>
    /// Получить бронирования указанного клиента
    /// </summary>
    [HttpGet("{id:int}/bookings")]
    [EndpointSummary("Получить бронирования указанного клиента")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult<IReadOnlyList<BookingDto>> GetBookingsByClientId(int id)
    {
        logger.LogInformation("Вызван метод {Method} контроллера {Controller}", nameof(GetBookingsByClientId), GetType().Name);

        try
        {
            var result = hotelClientService.GetBookingsByClientId(id);

            if (result is null)
            {
                logger.LogWarning("Клиент с идентификатором {ClientId} не найден", id);
                return NotFound();
            }

            logger.LogInformation("Метод {Method} контроллера {Controller} успешно выполнен", nameof(GetBookingsByClientId), GetType().Name);

            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "При получении бронирований клиента {ClientId} произошла ошибка", id);

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Произошла внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Получить пять клиентов с наибольшей общей стоимостью проживания
    /// </summary>
    [EndpointSummary("Получить пять клиентов с наибольшей общей стоимостью проживания")]
    [HttpGet("top-five-by-total-stay-cost")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<IReadOnlyList<HotelClientStayCostDto>> GetTopFiveClientsByTotalStayCost()
    {
        logger.LogInformation("Вызван метод {Method} контроллера {Controller}", nameof(GetTopFiveClientsByTotalStayCost), GetType().Name);

        try
        {
            var result = hotelClientService.GetTopFiveClientsByTotalStayCost();

            logger.LogInformation("Метод {Method} контроллера {Controller} успешно выполнен", nameof(GetTopFiveClientsByTotalStayCost), GetType().Name);

            return Ok(result);
        }

        catch (Exception exception)
        {
            logger.LogError(exception, "При получении пяти клиентов с наибольшей стоимостью проживания произошла ошибка");

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Произошла внутренняя ошибка сервера." });
        }

    }

    /// <summary>
    /// Получить клиентов, бронировавших комнаты указанной категории
    /// </summary>
    [EndpointSummary("Получить клиентов, бронировавших комнаты указанной категории")]
    [HttpGet("by-room-category/{category}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<IReadOnlyList<HotelClientDto>> GetClientsByRoomCategory(
        RoomCategory category)
    {
        logger.LogInformation("Вызван метод {Method} контроллера {Controller}. Категория комнаты: {Category}", nameof(GetClientsByRoomCategory), GetType().Name, category);

        try
        {
            var result = hotelClientService.GetClientsByRoomCategory(category);

            logger.LogInformation("Метод {Method} контроллера {Controller} успешно выполнен", nameof(GetClientsByRoomCategory), GetType().Name);

            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "При получении клиентов по категории комнат произошла ошибка");

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Произошла внутренняя ошибка сервера." });
        }
    }
}