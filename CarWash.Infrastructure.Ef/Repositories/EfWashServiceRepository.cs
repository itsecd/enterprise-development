using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.Ef.Repositories;

/// <summary>
/// Репозиторий услуг и аналитики выручки в PostgreSQL
/// </summary>
public class EfWashServiceRepository(CarWashDbContext context) : EfRepository<WashService>(context), IWashServiceRepository
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<(WashService WashService, int OrderCount)>> GetTop5PopularServicesAsync(CancellationToken cancellationToken = default)
    {
        var result = await Context.WashServices.AsNoTracking().Select(service => new
            {
                WashService = service,
                OrderCount = Context.Orders.Count(order => order.WashServiceId == service.Id)
            })
            .OrderByDescending(item => item.OrderCount).ThenBy(item => item.WashService.Id)
            .Take(5).ToListAsync(cancellationToken);
        return result.Select(item => (item.WashService, item.OrderCount)).ToList();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<(WashService WashService, decimal Revenue)>> GetRevenuePerServiceAsync(CancellationToken cancellationToken = default)
    {
        var result = await Context.WashServices.AsNoTracking().OrderBy(service => service.Id)
            .Select(service => new
            {
                WashService = service,
                Revenue = Context.Orders.Where(order => order.WashServiceId == service.Id)
                    .Sum(order => (decimal?)order.WashService.Cost) ?? 0m
            }).ToListAsync(cancellationToken);
        return result.Select(item => (item.WashService, item.Revenue)).ToList();
    }
}
