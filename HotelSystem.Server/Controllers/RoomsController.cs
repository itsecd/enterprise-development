using HotelSystem.Application.Contracts.Bookings;
using HotelSystem.Application.Contracts.Rooms;
using HotelSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.Controllers;

/// <summary>
/// Контроллер для управления номерами гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status422UnprocessableEntity)]
public class RoomsController(
    RoomService roomService,
    BookingService bookingService,
    ILogger<RoomsController> logger)
    : BaseCrudController<RoomRequest, RoomResponse>(roomService, logger)
{
    /// <summary>
    /// Возвращает бронирования номера
    /// </summary>
    [HttpGet("{id:int}/bookings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public ActionResult<List<BookingResponse>> GetBookings(int id)
    {
        var bookings = bookingService.GetByRoomId(id);

        if (bookings is null)
        {
            logger.LogWarning(
                "Не удалось получить бронирования: номер с ID {Id} не найден",
                id);

            return NotFound();
        }

        logger.LogInformation(
            "Получены бронирования номера с ID {Id}. Количество: {Count}",
            id,
            bookings.Count);

        return Ok(bookings);
    }
}