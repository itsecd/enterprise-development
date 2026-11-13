using CarWash.Application.Mapping;
using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using CarWash.Domain.Interfaces;

namespace CarWash.Application.Services;

public class AnalyticsService(IOrderRepository orderRepository) : IAnalyticsService
{
    public async Task<IEnumerable<TopClientDto>> GetTop5ClientsAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        return orders
            .GroupBy(o => o.Client)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.Id)
            .Take(5)
            .Select(g => new TopClientDto
            {
                Client = g.Key.ToDto(),
                VisitCount = g.Count()
            });
    }

    public async Task<IEnumerable<CurrentCarDto>> GetCurrentCarsAsync(DateTime? time)
    {
        var currentTime = time ?? DateTime.Now;
        var orders = await orderRepository.GetAllAsync();

        return orders
            .Where(o => o.StartTime <= currentTime && currentTime < o.StartTime.AddMinutes(o.Service.DurationMinutes))
            .Select(o => new CurrentCarDto
            {
                LicensePlate = o.Car.LicensePlate,
                Brand = o.Car.Brand,
                ServiceName = o.Service.Name,
                BoxNumber = o.BoxNumber
            });
    }

    public async Task<IEnumerable<TopServiceDto>> GetTop5PopularServicesAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        return orders
            .GroupBy(o => o.Service)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.Id)
            .Take(5)
            .Select(g => new TopServiceDto
            {
                Service = g.Key.ToDto(),
                UsageCount = g.Count()
            });
    }

    public async Task<BoxFreeTimeDto> GetBoxFreeTimeAsync(int boxNumber, DateTime? time)
    {
        var currentTime = time ?? DateTime.Now;
        var orders = await orderRepository.GetAllAsync();

        var busyOrders = orders
            .Where(o => o.BoxNumber == boxNumber && o.StartTime.AddMinutes(o.Service.DurationMinutes) > currentTime)
            .OrderBy(o => o.StartTime)
            .ToList();

        var freeTime = currentTime;

        foreach (var order in busyOrders)
        {
            var orderEndTime = order.StartTime.AddMinutes(order.Service.DurationMinutes);

            if (order.StartTime <= freeTime)
            {
                if (orderEndTime > freeTime) freeTime = orderEndTime;
            }
            else
            {
                break;
            }
        }

        return new BoxFreeTimeDto { BoxNumber = boxNumber, FreeTime = freeTime };
    }

    public async Task<Dictionary<string, decimal>> GetRevenueForEachServiceAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        return orders
            .GroupBy(o => o.Service.Name)
            .ToDictionary(g => g.Key, g => g.Sum(o => o.Service.Price));
    }
}