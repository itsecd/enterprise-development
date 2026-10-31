using CarWash.Application.Contracts;
using CarWash.Domain;
using CarWash.Domain.Entities;

namespace CarWash.Application.Services;

/// <summary>
/// Аналитическая служба автомойки
/// </summary>
public class AnalyticsService(IRepository<Order, int> orderRepository) : IAnalyticsService
{
    /// <inheritdoc/>
    public List<TopCustomerDto> GetTopFiveCustomersByVisitCount()
    {
        return orderRepository.ReadAll()
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
    public List<int> GetCarsCurrentlyAtCarWash()
    {
        var now = DateTime.Now;

        return orderRepository.ReadAll()
            .Where(order =>
                order.StartTime <= now &&
                now < order.StartTime.Add(order.Procedure.Duration))
            .Select(order => order.CarId)
            .ToList();
    }

    /// <inheritdoc/>
    public List<int> GetTopPopularProcedures()
    {
        return orderRepository.ReadAll()
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
    public DateTime GetWashBoxReleaseTime(int washBoxNumber)
    {
        return orderRepository.ReadAll()
            .Where(order => order.WashBoxNumber == washBoxNumber)
            .Select(order =>
                order.StartTime.Add(order.Procedure.Duration))
            .Max();
    }

    /// <inheritdoc/>
    public decimal CalculateProcedureRevenue(int procedureId)
    {
        return orderRepository.ReadAll()
            .Where(order => order.ProcedureId == procedureId)
            .Sum(order => order.Procedure.Price);
    }
}
