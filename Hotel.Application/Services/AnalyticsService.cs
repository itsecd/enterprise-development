using Hotel.Application.Interfaces;
using Hotel.Application.Mapping;
using Hotel.Contracts.DTO;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces;

namespace Hotel.Application.Services;

/// <summary>
/// Сервис для выполнения аналитических запросов
/// </summary>
public class AnalyticsService(IRepository<Booking> bookingRepository) : IAnalyticsService
{
    /// <summary>
    /// Получение клиентов по типу комнаты
    /// </summary>
    /// <param name="roomTypeId">Id типа комнаты</param>
    /// <returns>Список клиентов</returns>
    public async Task<IReadOnlyList<ClientDto>> GetClientsByRoomTypeAsync(int roomTypeId)
    {
        var bookings = await bookingRepository.GetByConditionAsync(b => b.Room.RoomType.Id == roomTypeId);
        return bookings
            .Select(b => b.Client)
            .DistinctBy(c => c.Id)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .ThenBy(c => c.Patronymic)
            .Select(c => c.ToDto())
            .ToList();
    }

    /// <summary>
    /// Получение номеров, занятых в указанную дату
    /// </summary>
    /// <param name="date">Дата проверки</param>
    /// <returns>Список номеров</returns>
    public async Task<IReadOnlyList<int>> GetBookedRoomsAsync(DateTime date)
    {
        var bookings = await bookingRepository.GetByConditionAsync(b => b.DateArrival <= date && b.DateArrival.AddDays(b.DayCount) >= date);
        return bookings
            .Select(b => b.Room.Number)
            .Distinct()
            .OrderBy(n => n)
            .ToList();
    }

    /// <summary>
    /// Получение топ 5 наиболее часто бронируемых номеров
    /// </summary>
    /// <returns>Список номеров</returns>
    public async Task<IReadOnlyList<int>> GetTop5BookedRoomsAsync()
    {
        var bookings = await bookingRepository.GetAllAsync();
        return bookings
            .GroupBy(b => b.Room.Number)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key)
            .Take(5)
            .Select(g => g.Key)
            .ToList();
    }

    /// <summary>
    /// Получение количества бронирований для каждого номера
    /// </summary>
    /// <returns>Список с количеством бронирований</returns>
    public async Task<IReadOnlyList<WorkFrequencyDto>> GetBookingsCountPerRoomAsync()
    {
        var bookings = await bookingRepository.GetAllAsync();
        return bookings
            .GroupBy(b => b.Room.Number)
            .OrderBy(g => g.Key)
            .Select(g => new WorkFrequencyDto(g.Key, g.Count()))
            .ToList();
    }

    /// <summary>
    /// Получение топ 5 клиентов по суммарной стоимости проживания
    /// </summary>
    /// <returns>Список клиентов с суммами</returns>
    public async Task<IReadOnlyList<TopClientDto>> GetTop5ClientsByStayAsync()
    {
        var bookings = await bookingRepository.GetAllAsync();
        return bookings
            .GroupBy(b => b.Client.Id)
            .OrderByDescending(g => g.Sum(b => b.DayCount * b.Room.RoomType.Price))
            .ThenBy(g => g.Key)
            .Take(5)
            .Select(g => new TopClientDto(
                g.Key,
                g.First().Client.LastName + " " + g.First().Client.FirstName,
                g.Sum(b => b.DayCount * b.Room.RoomType.Price)))
            .ToList();
    }
}