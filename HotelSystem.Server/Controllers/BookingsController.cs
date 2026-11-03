using HotelSystem.Application.Contracts.Bookings;
using HotelSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.Controllers;

/// <summary>
/// Контроллер для управления бронированиями гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status422UnprocessableEntity)]
public class BookingsController(
    BookingService bookingService,
    ILogger<BookingsController> logger)
    : BaseCrudController<BookingRequest, BookingResponse>(bookingService, logger);