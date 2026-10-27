using Hotel.Application.Contracts.Bookings;
using Hotel.Application.Contracts.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления комнатами и получения статистики по бронированиям
/// </summary>
public class RoomsController(IRoomService roomService, ILogger<RoomsController> logger)
    : CrudControllerBase<RoomDto, CreateRoomDto, int>(roomService, logger)
{
    /// <summary>
    /// Получить бронирования указанного номера
    /// </summary>
    [HttpGet("{id:int}/bookings")]
    [EndpointSummary("Получить бронирования указанного номера")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult<IReadOnlyList<BookingDto>> GetBookingsByRoomId(int id)
    {
        logger.LogInformation("Вызван метод {Method} контроллера {Controller}", nameof(GetBookingsByRoomId), GetType().Name);

        try
        {
            var result = roomService.GetBookingsByRoomId(id);

            if (result is null)
            {
                logger.LogWarning("Номер с идентификатором {RoomId} не найден", id);
                return NotFound();
            }

            logger.LogInformation("Метод {Method} контроллера {Controller} успешно выполнен", nameof(GetBookingsByRoomId), GetType().Name);

            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "При получении бронирований номера {RoomId} произошла ошибка", id);

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Произошла внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Получить пять комнат с наибольшим количеством бронирований
    /// </summary>
    [EndpointSummary("Получить пять комнат с наибольшим количеством бронирований")]
    [HttpGet("top-five-most-booked")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<IReadOnlyList<RoomBookingCountDto>> GetTopFiveMostBookedRooms()
    {
        logger.LogInformation("Вызван метод {Method} контроллера {Controller}", nameof(GetTopFiveMostBookedRooms), GetType().Name);

        try
        {
            var result = roomService.GetTopFiveMostBookedRooms();

            logger.LogInformation("Метод {Method} контроллера {Controller} успешно выполнен", nameof(GetTopFiveMostBookedRooms), GetType().Name);

            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "При получении пяти наиболее бронируемых комнат произошла ошибка");

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Произошла внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Получить количество бронирований для каждой комнаты
    /// </summary>
    [EndpointSummary("Получить количество бронирований для каждой комнаты")]
    [HttpGet("booking-counts")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<IReadOnlyList<RoomBookingCountDto>> GetBookingCountForEachRoom()
    {
        logger.LogInformation("Вызван метод {Method} контроллера {Controller}", nameof(GetBookingCountForEachRoom), GetType().Name);

        try
        {
            var result = roomService.GetBookingCountForEachRoom();

            logger.LogInformation("Метод {Method} контроллера {Controller} успешно выполнен", nameof(GetBookingCountForEachRoom), GetType().Name);

            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "При получении количества бронирований комнат произошла ошибка");

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Произошла внутренняя ошибка сервера." });
        }
    }
}