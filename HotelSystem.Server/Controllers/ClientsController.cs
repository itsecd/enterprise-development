using HotelSystem.Application.Contracts.Bookings;
using HotelSystem.Application.Contracts.Clients;
using HotelSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.Controllers;

/// <summary>
/// Контроллер для управления клиентами гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(
    ClientService clientService,
    BookingService bookingService,
    ILogger<ClientsController> logger)
    : BaseCrudController<ClientRequest, ClientResponse>(clientService, logger)
{
    /// <summary>
    /// Возвращает бронирования клиента
    /// </summary>
    [HttpGet("{id:int}/bookings")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public ActionResult<List<BookingResponse>> GetBookings(int id)
    {
        var bookings = bookingService.GetByClientId(id);

        if (bookings is null)
        {
            logger.LogWarning(
                "Не удалось получить бронирования: клиент с ID {Id} не найден",
                id);

            return NotFound();
        }

        logger.LogInformation(
            "Получены бронирования клиента с ID {Id}. Количество: {Count}",
            id,
            bookings.Count);

        return Ok(bookings);
    }
}