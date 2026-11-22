using EnterpriseDevelopment.Application.Interfaces;
using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Contracts.DTOs.Analytics;

namespace EnterpriseDevelopment.Application.Services;

/// <summary>
/// Сервис аналитики заказов и ресторанов
/// </summary>
public class AnalyticsService(
    IRestaurantRepository restaurantRepository,
    IOrderRepository orderRepository) : IAnalyticsService


{

    /// <summary>
    /// Получает пять ресторанов с наибольшим количеством заказов
    /// </summary>
    /// <returns>Коллекция ресторанов с количеством заказов</returns>
    public IEnumerable<TopRestaurantDto> GetTopRestaurants()
    {
        return orderRepository.GetAll()
            .GroupBy(order => order.RestaurantId)
            .Select(group => new TopRestaurantDto(
                group.Key,
                group.First().Restaurant?.Name ?? string.Empty,
                group.Count()))
            .OrderByDescending(item => item.OrderCount)
            .ThenBy(item => item.RestaurantId)
            .Take(5)
            .ToList();
    }

    /// <summary>
    /// Получает все заказы с минимальным временем доставки
    /// </summary>
    /// <returns>Коллекция заказов с минимальным временем доставки</returns>
    public IEnumerable<FastestOrderDto> GetFastestOrders()
    {
        var orders = orderRepository.GetAll().ToList();

        if (orders.Count == 0)
        {
            return [];
        }

        var minTime = orders.Min(order => order.DeliveryTime);

        return orders
            .Where(order => order.DeliveryTime == minTime)
            .Select(order => new FastestOrderDto(
                order.Id,
                order.ClientId,
                order.RestaurantId,
                order.DeliveryTime))
            .ToList();
    }


    /// <summary>
    /// Получает уникальных клиентов указанного ресторана
    /// </summary>
    /// <returns>Коллекция клиентов ресторана</returns>
    public IEnumerable<RestaurantClientDto> GetRestaurantClients(int restaurantId)
    {
        if (restaurantRepository.GetById(restaurantId) is null)
        {
            throw new KeyNotFoundException("Ресторан не найден");
        }

        return orderRepository.GetAll()
            .Where(order => order.RestaurantId == restaurantId)
            .Where(order => order.Client is not null)
            .GroupBy(order => order.ClientId)
            .Select(group => new RestaurantClientDto(
                group.Key,
                group.First().Client!.FullName,
                group.First().Client!.PhoneNumber))
            .OrderBy(client => client.ClientId)
            .ToList();
    }



    /// <summary>
    /// Получает статистику заказанных блюд по категориям за указанный период
    /// </summary>
    /// <returns>Коллекция категорий с количеством заказанных блюд</returns>
    public IEnumerable<CategoryStatisticsDto> GetCategoryStatistics(
        DateTimeOffset startDate,
        DateTimeOffset endDate)
    {
        if (startDate > endDate)
        {
            throw new ArgumentException(
                "Начальная дата не может быть позже конечной");
        }

        return orderRepository.GetAll()
            .Where(order =>
                order.CreatedAt >= startDate &&
                order.CreatedAt <= endDate)
            .SelectMany(order => order.Dishes)
            .GroupBy(dish => dish.CategoryId)
            .Select(group => new CategoryStatisticsDto(
                group.Key,
                group.First().Category?.Name ?? string.Empty,
                group.Count()))
            .OrderByDescending(item => item.OrderCount)
            .ThenBy(item => item.CategoryId)
            .ToList();
    }



    /// <summary>
    /// Получает клиентов с наибольшей суммой заказов
    /// </summary>
    /// <returns>Коллекция клиентов с максимальной суммой заказов</returns>
    public IEnumerable<TopClientDto> GetTopClients()
    {
        var clients = orderRepository.GetAll()
            .GroupBy(order => order.ClientId)
            .Select(group => new
            {
                ClientId = group.Key,
                FullName = group.First().Client?.FullName ?? string.Empty,
                TotalSpent = group.Sum(order => order.TotalAmount)
            })
            .ToList();

        if (clients.Count == 0)
        {
            return [];
        }

        var maxSpent = clients.Max(client => client.TotalSpent);

        return clients
            .Where(client => client.TotalSpent == maxSpent)
            .OrderBy(client => client.ClientId)
            .Select(client => new TopClientDto(
                client.ClientId,
                client.FullName,
                client.TotalSpent))
            .ToList();
    }

}

