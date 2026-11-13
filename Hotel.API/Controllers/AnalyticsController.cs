using Hotel.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Controllers;

/// <summary>
/// Контроллер для аналитических запросов
/// </summary>
/// <param name="service">Сервис аналитики</param>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(IAnalyticsService service) : ControllerBase
{
    /// <summary>
    /// Получение клиентов по типу комнаты
    /// </summary>
    /// <param name="roomTypeId">Id типа комнаты</param>
    /// <returns>Список клиентов</returns>
    [HttpGet("clients-by-room-type/{roomTypeId:int}")]
    public async Task<IActionResult> GetClientsByRoomType(int roomTypeId) => Ok(await service.GetClientsByRoomTypeAsync(roomTypeId));

    /// <summary>
    /// Получение занятых номеров на дату
    /// </summary>
    /// <param name="date">Дата (если не указана, берется текущая)</param>
    /// <returns>Список номеров</returns>
    [HttpGet("booked-rooms")]
    public async Task<IActionResult> GetBookedRooms([FromQuery] DateTime? date)
    {
        var currentDate = date ?? DateTime.Today;
        return Ok(await service.GetBookedRoomsAsync(currentDate));
    }

    /// <summary>
    /// Топ 5 часто бронируемых номеров
    /// </summary>
    /// <returns>Список номеров</returns>
    [HttpGet("top5-booked-rooms")]
    public async Task<IActionResult> GetTop5() => Ok(await service.GetTop5BookedRoomsAsync());

    /// <summary>
    /// Количество броней на каждый номер
    /// </summary>
    /// <returns>Список с количеством броней</returns>
    [HttpGet("bookings-count")]
    public async Task<IActionResult> GetCount() => Ok(await service.GetBookingsCountPerRoomAsync());

    /// <summary>
    /// Получение топ 5 клиентов по стоимости проживания
    /// </summary>
    /// <returns>Список клиентов</returns>
    [HttpGet("top5-clients")]
    public async Task<IActionResult> GetTop5Clients() => Ok(await service.GetTop5ClientsByStayAsync());
}