using FoodDelivery.Domain.Analytics;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Выполняет аналитические запросы к текущим коллекциям приложения
/// </summary>
/// <param name="data">Данные приложения</param>
public sealed class AnalyticsRepository(FoodDeliveryData data)
    : IAnalyticsRepository
{
    /// <inheritdoc/>
    public IReadOnlyList<RestaurantOrderStatistics> GetTopRestaurants()
    {
        return this.Execute<IReadOnlyList<RestaurantOrderStatistics>>(() =>
        {
            var counts = data.Orders
                .GroupBy(order => order.Restaurant.Id)
                .ToDictionary(group => group.Key, group => group.Count());

            return data.Restaurants
                .Select(restaurant => new RestaurantOrderStatistics(
                    restaurant,
                    counts.GetValueOrDefault(restaurant.Id)))
                .OrderByDescending(item => item.OrderCount)
                .ThenBy(item => item.Restaurant.Id)
                .Take(5)
                .ToArray();
        });
    }

    /// <inheritdoc/>
    public IReadOnlyList<Order> GetOrdersWithMinimumDeliveryTime()
    {
        return this.Execute<IReadOnlyList<Order>>(() =>
        {
            var deliveredOrders = data.Orders
                .Where(order => order.DeliveredAt.HasValue)
                .ToArray();

            if (deliveredOrders.Length == 0)
            {
                return [];
            }

            var minimumTime = deliveredOrders
                .Min(order => order.DeliveredAt!.Value - order.CreatedAt);

            return deliveredOrders
                .Where(order =>
                    order.DeliveredAt!.Value - order.CreatedAt == minimumTime)
                .OrderBy(order => order.Id)
                .ToArray();
        });
    }

    /// <inheritdoc/>
    public IReadOnlyList<Customer> GetRestaurantCustomers(int restaurantId)
    {
        return this.Execute<IReadOnlyList<Customer>>(() =>
        {
            return data.Orders
                .Where(order => order.Restaurant.Id == restaurantId)
                .Select(order => order.Customer)
                .DistinctBy(customer => customer.Id)
                .OrderBy(customer => customer.LastName)
                .ThenBy(customer => customer.FirstName)
                .ThenBy(customer => customer.Patronymic)
                .ThenBy(customer => customer.Id)
                .ToArray();
        });
    }

    /// <inheritdoc/>
    public IReadOnlyList<CategoryOrderStatistics> GetCategoryStatistics(
        DateTime start,
        DateTime end)
    {
        return this.Execute<IReadOnlyList<CategoryOrderStatistics>>(() =>
        {
            var orders = data.Orders
                .Where(order =>
                    order.CreatedAt >= start &&
                    order.CreatedAt <= end)
                .ToArray();

            return data.Categories
                .OrderBy(category => category.Id)
                .Select(category =>
                {
                    var categoryOrders = orders
                        .Where(order => order.Items.Any(
                            item => item.Dish.Category.Id == category.Id))
                        .ToArray();

                    var orderCount = categoryOrders.Length;
                    var totalAmount = categoryOrders.Sum(order => order.TotalAmount);

                    var averageAmount = orderCount == 0
                        ? 0m
                        : Math.Round(totalAmount / orderCount, 2);

                    return new CategoryOrderStatistics(
                        category,
                        orderCount,
                        averageAmount,
                        totalAmount);
                })
                .ToArray();
        });
    }

    /// <inheritdoc/>
    public CustomerSpending? GetTopSpendingCustomer()
    {
        return this.Execute<CustomerSpending?>(() =>
        {
            return data.Orders
                .GroupBy(order => order.Customer.Id)
                .Select(group => new CustomerSpending(
                    data.Customers.Single(customer => customer.Id == group.Key),
                    group.Sum(order => order.TotalAmount)))
                .OrderByDescending(item => item.TotalAmount)
                .ThenBy(item => item.Customer.Id)
                .FirstOrDefault();
        });
    }

    /// <inheritdoc/>
    public T Execute<T>(Func<T> operation)
    {
        lock (InMemorySynchronization.For(data))
        {
            return operation();
        }
    }

    /// <inheritdoc/>
    public void Execute(Action operation)
    {
        lock (InMemorySynchronization.For(data))
        {
            operation();
        }
    }
}
