using DryCleaner.Application.Contracts.DTOs.Analytics;
using DryCleaner.Application.Contracts.Interfaces;
using DryCleaner.Domain.Repositories;
using DryCleaner.Domain.Entities;
using DryCleaner.Shared.Enums;

namespace DryCleaner.Application.Services;

/// <summary>
/// Аналитическая информация
/// </summary>
public class AnalyticsService(IOrderRepository orderRepository) : IAnalyticsService
{
    /// <inheritdoc />
    public async Task<IList<OrderInProgressDto>> GetOrdersInProgressAsync()
    {
        var orders = await orderRepository.GetInProgressOrdersAsync();

        return orders.Select(o => new OrderInProgressDto(
            o.Id,
            $"{o.Customer.LastName} {o.Customer.FirstName}",
            o.AcceptanceDate,
            o.Status.ToString()))
            .ToList();
    }

    /// <inheritdoc />
    public async Task<IList<CustomerOrderCountDto>> GetTop5CustomersByItemsAsync(DateOnly periodStart, DateOnly periodEnd)
    {
        var orders = await orderRepository.GetOrdersByPeriodAsync(periodStart, periodEnd);

        return orders
            .GroupBy(o => o.CustomerId)
            .Select(group => new CustomerOrderCountDto(
                group.Key,
                $"{group.First().Customer.LastName} {group.First().Customer.FirstName}",
                group.Count()))
            .OrderByDescending(x => x.OrderCount)
            .ThenBy(x => x.CustomerName)
            .Take(5)
            .ToList();
    }

    /// <inheritdoc />
    public async Task<IList<OrderProcessingTimeDto>> GetCustomersWithLongestProcessingAsync()
    {
        var orders = await orderRepository.GetOrdersWithMaxCompletionDaysAsync();
        if (!orders.Any()) return new List<OrderProcessingTimeDto>();

        var maxDays = orders.First().CompletionDays;

        return orders
            .Select(o => o.Customer)
            .DistinctBy(c => c.Id)
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .Select(c => new OrderProcessingTimeDto(
                c.Id,
                $"{c.LastName} {c.FirstName}",
                maxDays))
            .ToList();
    }

    /// <inheritdoc />
    public async Task<IList<CategoryPopularityDto>> GetTop5PopularCategoriesAsync(DateOnly start, DateOnly end)
    {
        var orders = await orderRepository.GetOrdersByPeriodAsync(start, end);

        return orders
            .GroupBy(o => o.Item.CategoryId)
            .Select(group => new CategoryPopularityDto(
                group.Key,
                group.First().Item.Category.Name,
                group.Count()))
            .OrderByDescending(x => x.OrderCount)
            .ThenBy(x => x.CategoryName)
            .Take(5)
            .ToList();
    }

    /// <inheritdoc />
    public async Task<IList<CategoryPopularityDto>> GetTop5UnpopularCategoriesAsync(DateOnly start, DateOnly end)
    {
        var orders = await orderRepository.GetOrdersByPeriodAsync(start, end);

        return orders
            .GroupBy(o => o.Item.CategoryId)
            .Select(group => new CategoryPopularityDto(
                group.Key,
                group.First().Item.Category.Name,
                group.Count()))
            .Where(x => x.OrderCount > 0)
            .OrderBy(x => x.OrderCount)
            .ThenBy(x => x.CategoryName)
            .Take(5)
            .ToList();
    }

    /// <inheritdoc />
    public async Task<TotalSpentCustomerDto?> GetCustomerWithMaxTotalSpentAsync()
    {
        var orders = await orderRepository.GetAllWithDetailsAsync();

        return orders
            .GroupBy(o => o.CustomerId)
            .Select(group => new TotalSpentCustomerDto(
                group.Key,
                group.First().Customer.LastName,
                group.First().Customer.FirstName,
                group.First().Customer.Patronymic,
                group.Sum(o => o.Item.Category.Price)))
            .OrderByDescending(c => c.TotalSpentAmount)
            .ThenBy(c => c.CustomerId)
            .FirstOrDefault();
    }
}