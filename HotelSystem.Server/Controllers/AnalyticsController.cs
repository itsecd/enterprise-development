using HotelSystem.Application.Contracts.Analytics;
using HotelSystem.Application.Contracts.Clients;
using HotelSystem.Application.Contracts.Rooms;
using HotelSystem.Application.Services;
using HotelSystem.Domain.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.Controllers;

/// <summary>
/// Контроллер для выполнения аналитических запросов по данным гостиницы
/// </summary>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public class AnalyticsController(
    AnalyticsService analyticsService,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// 1. Выводит клиентов, проживавших в номерах указанного типа, упорядоченных по ФИО
    /// </summary>
    /// <param name="category">Категория номера</param>
    [HttpGet("clients-by-room-category/{category}")]
    [ProducesResponseType<List<ClientResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public ActionResult<List<ClientResponse>> GetClientsByRoomCategory(
        RoomCategory category)
    {
        logger.LogInformation(
            "Получен запрос клиентов по категории номера {Category}",
            category);

        if (!Enum.IsDefined(category))
        {
            logger.LogWarning(
                "Получена неизвестная категория номера {Category}",
                category);

            return Problem(
                detail: "Указана несуществующая категория номера",
                statusCode: StatusCodes.Status400BadRequest);
        }

        return Ok(analyticsService.GetClientsByRoomCategory(category));
    }

    /// <summary>
    /// 2. Выводит номера, занятые на дату проверки
    /// </summary>
    [HttpGet("occupied-rooms")]
    [ProducesResponseType<List<RoomResponse>>(StatusCodes.Status200OK)]
    public ActionResult<List<RoomResponse>> GetCurrentlyBookedRooms()
    {
        logger.LogInformation("Получен запрос занятых номеров");

        return Ok(analyticsService.GetCurrentlyBookedRooms());
    }

    /// <summary>
    /// 3. Выводит топ-5 наиболее часто бронируемых номеров
    /// </summary>
    [HttpGet("top-booked-rooms")]
    [ProducesResponseType<List<TopBookedRoomResponse>>(StatusCodes.Status200OK)]
    public ActionResult<List<TopBookedRoomResponse>>
        GetTop5MostFrequentlyBookedRooms()
    {
        logger.LogInformation("Получен запрос топ-5 бронируемых номеров");

        return Ok(analyticsService.GetTop5MostFrequentlyBookedRooms());
    }

    /// <summary>
    /// 4. Выводит количество бронирований для каждого номера
    /// </summary>
    [HttpGet("booking-count-by-room")]
    [ProducesResponseType<List<RoomBookingCountResponse>>(StatusCodes.Status200OK)]
    public ActionResult<List<RoomBookingCountResponse>>
        GetBookingCountForEachRoom()
    {
        logger.LogInformation("Получен запрос количества бронирований по номерам");

        return Ok(analyticsService.GetBookingCountForEachRoom());
    }

    /// <summary>
    /// 5. Выводит топ-5 клиентов по суммарной стоимости проживания
    /// </summary>
    [HttpGet("top-clients-by-cost")]
    [ProducesResponseType<List<TopClientByCostResponse>>(StatusCodes.Status200OK)]
    public ActionResult<List<TopClientByCostResponse>>
        GetTop5ClientsByTotalCost()
    {
        logger.LogInformation("Получен запрос топ-5 клиентов по стоимости проживания");

        return Ok(analyticsService.GetTop5ClientsByTotalCost());
    }
}