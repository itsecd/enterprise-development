using CarWash.Domain.Data;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Репозиторий услуг в памяти для тестов
/// </summary>
public class InMemoryWashServiceRepository(CarWashData data, IOrderRepository orders)
    : InMemoryRepository<WashService>(data.WashServices), IWashServiceRepository
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<(WashService WashService, int OrderCount)>> GetTop5PopularServicesAsync(CancellationToken cancellationToken = default)
    {
        var counts = (await orders.GetAllAsync(cancellationToken))
            .GroupBy(order => order.WashServiceId).ToDictionary(group => group.Key, group => group.Count());
        return Items.Values.Select(service => (WashService: service, OrderCount: counts.GetValueOrDefault(service.Id)))
            .OrderByDescending(item => item.OrderCount).ThenBy(item => item.WashService.Id).Take(5).ToList();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<(WashService WashService, decimal Revenue)>> GetRevenuePerServiceAsync(CancellationToken cancellationToken = default)
    {
        var revenue = (await orders.GetAllAsync(cancellationToken)).GroupBy(order => order.WashServiceId)
            .ToDictionary(group => group.Key, group => group.Sum(order => order.WashService.Cost));
        return Items.Values.OrderBy(service => service.Id)
            .Select(service => (WashService: service, Revenue: revenue.GetValueOrDefault(service.Id))).ToList();
    }
}
