using DryCleaner.Application.Contracts.DTOs.Analytics;
using DryCleaner.Application.Contracts.Interfaces;
using DryCleaner.Domain.Repositories;
using DryCleaner.Domain.Entities;
using DryCleaner.Shared.Enums;

namespace DryCleaner.Application.Services;

/// <summary>
/// Аналитическая информация
/// </summary>
public class AnalyticsService(IRepository<Order> orderRepository) : IAnalyticsService
{
    /// <inheritdoc />
    public IList<OrderInProgressDto> GetOrdersInProgress()
    {
        return orderRepository.GetAll()
            .Where(o => o.Status == OrderStatus.InProgress)
            .OrderBy(o => o.AcceptanceDate)
            .Select(o => new OrderInProgressDto(
                o.Id,
                $"{o.Customer.LastName} {o.Customer.FirstName}",
                o.AcceptanceDate,
                o.Status.ToString()))
            .ToList();
    }

    /// <inheritdoc />
    public IList<CustomerOrderCountDto> GetTop5CustomersByItems(DateOnly periodStart, DateOnly periodEnd)
    {
        return orderRepository.GetAll()
            .Where(o => o.AcceptanceDate >= periodStart && o.AcceptanceDate <= periodEnd)
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
    public IList<OrderProcessingTimeDto> GetCustomersWithLongestProcessing()
    {
        var orders = orderRepository.GetAll().ToList();
        if (orders.Count == 0)
        {
            return [];
        }

        var maxDays = orders.Max(o => o.CompletionDays);

        return orders
            .Where(o => o.CompletionDays == maxDays)
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
    public IList<CategoryPopularityDto> GetTop5PopularCategories(DateOnly start, DateOnly end)
    {
        return orderRepository.GetAll()
            .Where(o => o.AcceptanceDate >= start && o.AcceptanceDate <= end)
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
    public IList<CategoryPopularityDto> GetTop5UnpopularCategories(DateOnly start, DateOnly end)
    {
        return orderRepository.GetAll()
            .Where(o => o.AcceptanceDate >= start && o.AcceptanceDate <= end)
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
    public TotalSpentCustomerDto? GetCustomerWithMaxTotalSpent()
    {
        return orderRepository.GetAll()
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