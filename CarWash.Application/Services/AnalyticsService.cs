using CarWash.Application.Contracts;
using CarWash.Domain;
using CarWash.Domain.Entities;

namespace CarWash.Application.Services;

/// <summary>
/// Аналитическая служба автомойки
/// </summary>
public class AnalyticsService(
    IRepository<Order, int> orderRepository) : IAnalyticsService
{
    /// <inheritdoc/>
    public async Task<List<TopCustomerDto>> GetTopFiveCustomersByVisitCount()
    {
        var orders = await orderRepository.ReadAll();

        return orders
            .GroupBy(order => order.Car.CustomerId)
            .Select(group => new TopCustomerDto(
                group.Key,
                group.Count()))
            .OrderByDescending(item => item.VisitCount)
            .ThenBy(item => item.CustomerId)
            .Take(5)
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<List<int>> GetCarsCurrentlyAtCarWash()
    {
        var now = DateTime.Now;
        var orders = await orderRepository.ReadAll();

        return orders
            .Where(order =>
                order.StartTime <= now &&
                now < order.StartTime.Add(order.Procedure.Duration))
            .Select(order => order.CarId)
            .Distinct()
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<List<int>> GetTopPopularProcedures()
    {
        var orders = await orderRepository.ReadAll();

        return orders
            .GroupBy(order => order.ProcedureId)
            .Select(group => new
            {
                ProcedureId = group.Key,
                OrderCount = group.Count()
            })
            .OrderByDescending(item => item.OrderCount)
            .ThenBy(item => item.ProcedureId)
            .Take(5)
            .Select(item => item.ProcedureId)
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<DateTime> GetWashBoxReleaseTime(int washBoxNumber)
    {
        var orders = await orderRepository.ReadAll();

        return orders
            .Where(order => order.WashBoxNumber == washBoxNumber)
            .Select(order =>
                order.StartTime.Add(order.Procedure.Duration))
            .DefaultIfEmpty(DateTime.Now)
            .Max();
    }

    /// <inheritdoc/>
    public async Task<decimal> CalculateProcedureRevenue(int procedureId)
    {
        var orders = await orderRepository.ReadAll();

        return orders
            .Where(order => order.ProcedureId == procedureId)
            .Sum(order => order.Procedure.Price);
    }
}