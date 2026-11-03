using HotelSystem.Application.Contracts.Analytics;
using HotelSystem.Application.Contracts.Clients;
using HotelSystem.Application.Contracts.Rooms;
using HotelSystem.Application.Mapping;
using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Domain.Shared.Enums;

namespace HotelSystem.Application.Services;

/// <summary>
/// Сервис для выполнения аналитических запросов по данным гостиницы
/// </summary>
public class AnalyticsService(IRepository<Booking> bookingRepository)
{
    /// <summary>
    /// 1. Выводит клиентов, проживавших в номерах указанного типа, упорядоченных по ФИО
    /// </summary>
    public List<ClientResponse> GetClientsByRoomCategory(RoomCategory roomCategory)
    {
        return bookingRepository.GetAll()
            .Where(booking => booking.Room.RoomType.Category == roomCategory)
            .Select(booking => booking.Client)
            .DistinctBy(client => client.Id)
            .OrderBy(client => client.LastName)
            .ThenBy(client => client.FirstName)
            .ThenBy(client => client.Patronymic)
            .Select(client => client.ToResponse())
            .ToList();
    }

    /// <summary>
    /// 2. Выводит номера, занятые на дату проверки
    /// </summary>
    public List<RoomResponse> GetCurrentlyBookedRooms()
    {
        var currentDate = new DateTime(2026, 9, 10);

        return bookingRepository.GetAll()
            .Where(booking =>
                booking.CheckInDate <= currentDate &&
                booking.CheckInDate.AddDays(booking.NumberOfDays) > currentDate)
            .Select(booking => booking.Room)
            .DistinctBy(room => room.Id)
            .OrderBy(room => room.RoomNumber)
            .Select(room => room.ToResponse())
            .ToList();
    }

    /// <summary>
    /// 3. Выводит топ-5 наиболее часто бронируемых номеров
    /// </summary>
    public List<TopBookedRoomResponse> GetTop5MostFrequentlyBookedRooms()
    {
        return bookingRepository.GetAll()
            .GroupBy(booking => booking.Room)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key.RoomNumber)
            .Take(5)
            .Select(group => new TopBookedRoomResponse
            {
                RoomNumber = group.Key.RoomNumber,
                BookingCount = group.Count()
            })
            .ToList();
    }

    /// <summary>
    /// 4. Выводит количество бронирований для каждого номера
    /// </summary>
    public List<RoomBookingCountResponse> GetBookingCountForEachRoom()
    {
        return bookingRepository.GetAll()
            .GroupBy(booking => booking.Room.RoomNumber)
            .OrderBy(group => group.Key)
            .Select(group => new RoomBookingCountResponse
            {
                RoomNumber = group.Key,
                BookingCount = group.Count()
            })
            .ToList();
    }

    /// <summary>
    /// 5. Выводит топ-5 клиентов по суммарной стоимости проживания
    /// </summary>
    public List<TopClientByCostResponse> GetTop5ClientsByTotalCost()
    {
        return bookingRepository.GetAll()
            .GroupBy(booking => booking.Client)
            .Select(group => new
            {
                Client = group.Key,
                TotalCost = group.Sum(booking => booking.TotalCost)
            })
            .OrderByDescending(item => item.TotalCost)
            .Take(5)
            .Select(item => new TopClientByCostResponse
            {
                LastName = item.Client.LastName,
                FirstName = item.Client.FirstName,
                Patronymic = item.Client.Patronymic,
                TotalCost = item.TotalCost
            })
            .ToList();
    }
}