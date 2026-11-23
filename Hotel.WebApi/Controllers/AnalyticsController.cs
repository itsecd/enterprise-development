using Hotel.Application.Contracts;
using Hotel.Application.Contracts.Booking;
using Hotel.Domain.Shared.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebAPI.Controllers;

/// <summary>
/// Контроллер для аналитических запросов
/// </summary>
/// <param name="analyticsService">Интерфейс службы</param>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    /// <summary>
    /// Получение клиентов, проживавших в номерах указанного типа
    /// </summary>
    /// <param name="category">Категория комнат</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Коллекция клиентов</returns>
    [HttpGet("clients-by-room-type/{category}")]
    [ProducesResponseType(typeof(ClientsByRoomTypeDto), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetClientsByRoomType(string category, CancellationToken ct)
    {
        if (!Enum.TryParse<RoomCategory>(category, true, out var roomCategory))
        {
            return BadRequest($"Категории '{category}' не существует.");
        }

        var result = await analyticsService.GetClientsByRoomTypeAsync(roomCategory, ct);
        return Ok(result);
    }

    /// <summary>
    /// Получение списка текущих аренд
    /// </summary>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Текущие аренды</returns>
    [HttpGet("current-bookings")]
    [ProducesResponseType(typeof(IEnumerable<BookingResponseDto>), 200)]
    public async Task<IActionResult> GetCurrentBookings(CancellationToken ct)
        => Ok(await analyticsService.GetCurrentBookingsAsync(ct));

    /// <summary>
    /// Получение 5 самых часто снимаемых номеров
    /// </summary>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>5 Комнат</returns>
    [HttpGet("top5-rooms")]
    [ProducesResponseType(typeof(IEnumerable<RoomBookingCountDto>), 200)]
    public async Task<IActionResult> GetTop5Rooms(CancellationToken ct)
        => Ok(await analyticsService.GetTop5FrequentlyBookedRoomsAsync(ct));

    /// <summary>
    /// Получение списка всех комнат с кол-вом снятий
    /// </summary>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Комнаты с кол-вом снятий</returns>
    [HttpGet("rooms-booking-count")]
    [ProducesResponseType(typeof(IEnumerable<RoomBookingCountDto>), 200)]
    public async Task<IActionResult> GetRoomsBookingCount(CancellationToken ct)
        => Ok(await analyticsService.GetBookingCountForEachRoomAsync(ct));

    /// <summary>
    /// Получить 5 клиентов с наивысшей суммой снятий номеров
    /// </summary>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>5 Клиентов с суммой снятий</returns>
    [HttpGet("top5-clients")]
    [ProducesResponseType(typeof(IEnumerable<ClientTotalSpentDto>), 200)]
    public async Task<IActionResult> GetTop5Clients(CancellationToken ct)
        => Ok(await analyticsService.GetTop5ClientsByTotalSpentAsync(ct));
}