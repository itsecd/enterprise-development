using Hotel.Application.Contracts.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления бронированиями
/// </summary>
public class BookingsController(IBookingService bookingService, ILogger<BookingsController> logger)
    : CrudControllerBase<BookingDto, CreateBookingDto, int>(bookingService, logger)
{
    /// <summary>
    /// Получить список текущих бронирований
    /// </summary>
    [EndpointSummary("Получить список текущих бронирований")]
    [HttpGet("current")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<IReadOnlyList<BookingDto>> GetCurrentBookings()
    {
        logger.LogInformation("Вызван метод {Method} контроллера {Controller}", nameof(GetCurrentBookings), GetType().Name);

        try
        {
            var result = bookingService.GetBookedRooms();

            logger.LogInformation("Метод {Method} контроллера {Controller} успешно выполнен", nameof(GetCurrentBookings), GetType().Name);

            return Ok(result);
        }

        catch (Exception exception)
        {
            logger.LogError(exception, "При получении текущих бронирований произошла ошибка");

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Произошла внутренняя ошибка сервера" });
        }
    }
}