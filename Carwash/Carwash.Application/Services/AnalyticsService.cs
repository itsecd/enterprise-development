using Carwash.Application.Contracts;
using Carwash.Application.Contracts.Cars;
using Carwash.Application.Contracts.Analytics;
using Carwash.Domain;
using Carwash.Domain.Entities;

namespace Carwash.Application.Services;

/// <summary>
/// Служба для аналитических запросов к данным приложения
/// </summary>
/// <param name="orderRepository">Репозиторий заказов</param>
public class AnalyticsService(IRepository<Order, int> orderRepository) : IAnalyticsService
{
    /// <inheritdoc/>
    public async Task<DateTime?> GetBoxReleaseTime(int boxNumber)
    {
        var orders = await orderRepository.ReadAll();
        var currentTime = DateTime.Now;

        var ordersForBox = orders
            .Where(order => order.BoxNumber == boxNumber)
            .ToList();

        if (ordersForBox.Count == 0)
            return null;

        var releaseTime = ordersForBox
            .Where(order =>
                order.BoxNumber == boxNumber &&
                order.StartTime <= currentTime &&
                order.EndTime > currentTime)
            .Max(order => (DateTime?)order.EndTime);

        return releaseTime ?? currentTime;
    }

    /// <inheritdoc/>
    public async Task<IList<CarDto>> GetCarsCurrentlyAtCarWash()
    {
        var orders = await orderRepository.ReadAll();
        var currentTime = DateTime.Now;

        var result = orders
            .Where(order =>
                order.StartTime <= currentTime &&
                order.EndTime > currentTime)
            .Select(order => order.Car)
            .DistinctBy(car => car.Id)
            .ToList();

        return Mapper.Map(result) ?? [];
    }

    /// <inheritdoc/>
    public async Task<IList<ClientVisitDto>> GetTop5ClientsByVisits()
    {
        var orders = await orderRepository.ReadAll();

        return orders
            .GroupBy(order => order.Client)
            .Select(group => new
            {
                Client = group.Key,
                VisitCount = group.Count()
            })
            .OrderByDescending(item => item.VisitCount)
            .ThenBy(item => item.Client.Id)
            .Take(5)
            .Select(item => Mapper.Map(item.Client, item.VisitCount))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<PopularServiceDto>> GetTop5MostPopularServices()
    {
        var orders = await orderRepository.ReadAll();

        return orders
            .GroupBy(order => order.Service)
            .Select(group => new
            {
                Service = group.Key,
                OrderCount = group.Count()
            })
            .OrderByDescending(item => item.OrderCount)
            .ThenBy(item => item.Service.Id)
            .Take(5)
            .Select(item => Mapper.Map(item.Service, item.OrderCount))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<ServiceRevenueDto>> GetTotalRevenueByServices()
    {
        var orders = await orderRepository.ReadAll();

        return orders
            .GroupBy(order => order.Service)
            .Select(group => new
            {
                Service = group.Key,
                Revenue = group.Sum(order => order.Service.Price)
            })
            .OrderBy(item => item.Service.Id)
            .Select(item => Mapper.Map(item.Service, item.Revenue))
            .ToList();
    }
}
