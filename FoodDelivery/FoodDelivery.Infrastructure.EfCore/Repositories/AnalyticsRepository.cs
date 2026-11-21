using FoodDelivery.Domain.Analytics;
using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace FoodDelivery.Infrastructure.EfCore.Repositories;

/// <summary>Выполняет аналитические запросы к MySQL</summary>
/// <param name="context">Контекст базы данных</param>
public sealed class AnalyticsRepository(FoodDeliveryDbContext context) : IAnalyticsRepository
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<RestaurantOrderStatistics>> GetTopRestaurantsAsync(
        CancellationToken cancellationToken = default)
    {
        return await context.Restaurants
            .Select(restaurant => new
            {
                Restaurant = restaurant,
                OrderCount = context.Orders.Count(order => order.RestaurantId == restaurant.Id)
            })
            .OrderByDescending(item => item.OrderCount)
            .ThenBy(item => item.Restaurant.Id)
            .Take(5)
            .Select(item => new RestaurantOrderStatistics(item.Restaurant, item.OrderCount))
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Order>> GetOrdersWithMinimumDeliveryTimeAsync(
        CancellationToken cancellationToken = default)
    {
        var deliveries = context.Orders
            .Where(order => order.DeliveredAt != null && order.DeliveredAt >= order.CreatedAt)
            .Select(order => new
            {
                Order = order,
                Duration = (long?)EF.Functions.DateDiffMicrosecond(order.CreatedAt, order.DeliveredAt)
            });

        var minimum = await deliveries.MinAsync(delivery => delivery.Duration, cancellationToken);
        if (minimum is null)
        {
            return [];
        }

        return await deliveries
            .Where(delivery => delivery.Duration == minimum)
            .Select(delivery => delivery.Order)
            .Include(order => order.Customer)
            .Include(order => order.Restaurant)
            .Include(order => order.Items)
            .OrderBy(order => order.Id)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Customer>> GetRestaurantCustomersAsync(
        int restaurantId, CancellationToken cancellationToken = default)
    {
        return await context.Customers
            .Where(customer => context.Orders.Any(order =>
                order.CustomerId == customer.Id && order.RestaurantId == restaurantId))
            .OrderBy(customer => customer.LastName)
            .ThenBy(customer => customer.FirstName)
            .ThenBy(customer => customer.Patronymic)
            .ThenBy(customer => customer.Id)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<CategoryOrderStatistics>> GetCategoryStatisticsAsync(
        DateTime start, DateTime end, CancellationToken cancellationToken = default)
    {
        var orders = context.Orders.Where(order => order.CreatedAt >= start && order.CreatedAt <= end);
        var totals = await context.Categories
            .OrderBy(category => category.Id)
            .Select(category => new
            {
                Category = category,
                OrderCount = orders.Count(order =>
                    order.Items.Any(item => item.Dish.CategoryId == category.Id)),
                TotalAmount = context.OrderItems
                    .Where(position => orders.Any(order => order.Id == position.OrderId &&
                        order.Items.Any(item => item.Dish.CategoryId == category.Id)))
                    .Sum(position => position.UnitPrice * position.Quantity)
            })
            .ToListAsync(cancellationToken);

        return totals.Select(item => new CategoryOrderStatistics(
            item.Category,
            item.OrderCount,
            item.OrderCount == 0 ? 0m : Math.Round(item.TotalAmount / item.OrderCount, 2),
            item.TotalAmount)).ToArray();
    }

    /// <inheritdoc/>
    public Task<CustomerSpending?> GetTopSpendingCustomerAsync(
        CancellationToken cancellationToken = default)
    {
        return context.Customers
            .Where(customer => context.Orders.Any(order => order.CustomerId == customer.Id))
            .Select(customer => new
            {
                Customer = customer,
                TotalAmount = context.OrderItems
                    .Where(position => context.Orders.Any(order =>
                        order.Id == position.OrderId && order.CustomerId == customer.Id))
                    .Sum(position => position.UnitPrice * position.Quantity)
            })
            .OrderByDescending(item => item.TotalAmount)
            .ThenBy(item => item.Customer.Id)
            .Select(item => new CustomerSpending(item.Customer, item.TotalAmount))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
