using FoodDelivery.Application.Contracts.DTOs.Analytics;
using FoodDelivery.Application.Contracts.Interfaces;
using FoodDelivery.Application.Interfaces;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Предоставляет аналитическую информацию о заказах
/// </summary>
public class AnalyticsService(
    IOrderRepository orderRepository,
    ICategoryRepository categoryRepository)
    : IAnalyticsService
{
    /// <inheritdoc />
    public async Task<IList<CategoryOrderSummaryDto>>
        GetOrdersByCategoryForPeriodAsync(
            DateTime periodStart,
            DateTime periodEnd)
    {
        if (periodEnd <= periodStart)
        {
            throw new ArgumentException(
                "Дата окончания должна быть позже даты начала");
        }

        var orders = (await orderRepository.GetAllAsync())
            .Where(order =>
                order.CreatedAt >= periodStart &&
                order.CreatedAt < periodEnd)
            .ToList();

        var categories = await categoryRepository.GetAllAsync();

        return orders
            .SelectMany(order => order.Items
                .Select(item => new
                {
                    Order = order,
                    CategoryId = item.Dish.CategoryId
                }))
            .GroupBy(item => item.CategoryId)
            .Select(group =>
            {
                var category = categories.FirstOrDefault(
                    item => item.Id == group.Key);

                if (category is null)
                {
                    throw new InvalidOperationException(
                        $"Категория с ID {group.Key} не найдена");
                }

                var categoryOrders = group
                    .Select(item => item.Order)
                    .DistinctBy(order => order.Id)
                    .ToList();

                return new CategoryOrderSummaryDto(
                    category.Id,
                    category.Name,
                    categoryOrders.Count,
                    categoryOrders.Average(order => order.TotalAmount),
                    categoryOrders.Sum(order => order.TotalAmount));
            })
            .OrderBy(item => item.CategoryId)
            .ToList();
    }

    /// <inheritdoc />
    public async Task<IList<CustomerOrderSummaryDto>>
        GetCustomersByRestaurantAsync(int restaurantId)
    {
        var orders = await orderRepository.GetAllAsync();

        return orders
            .Where(order => order.RestaurantId == restaurantId)
            .Select(order => order.Customer)
            .DistinctBy(customer => customer.Id)
            .OrderBy(customer => customer.LastName)
            .ThenBy(customer => customer.FirstName)
            .ThenBy(customer => customer.Patronymic)
            .Select(customer => new CustomerOrderSummaryDto(
                customer.Id,
                customer.LastName,
                customer.FirstName,
                customer.Patronymic,
                orders
                    .Where(order => order.CustomerId == customer.Id)
                    .Sum(order => order.TotalAmount)))
            .ToList();
    }

    /// <inheritdoc />
    public async Task<CustomerOrderSummaryDto?>
        GetCustomerWithMaxTotalOrderAmountAsync()
    {
        var orders = await orderRepository.GetAllAsync();

        return orders
            .GroupBy(order => order.CustomerId)
            .Select(group =>
            {
                var customer = group.First().Customer;

                return new CustomerOrderSummaryDto(
                    customer.Id,
                    customer.LastName,
                    customer.FirstName,
                    customer.Patronymic,
                    group.Sum(order => order.TotalAmount));
            })
            .OrderByDescending(customer => customer.TotalOrderAmount)
            .ThenBy(customer => customer.CustomerId)
            .FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<IList<OrderDeliveryTimeDto>>
        GetOrdersWithMinDeliveryTimeAsync()
    {
        var orders = await orderRepository.GetAllAsync();

        if (orders.Count == 0)
        {
            return [];
        }

        var minimumDeliveryTime = orders
            .Min(order => order.DeliveredAt - order.CreatedAt);

        return orders
            .Where(order =>
                order.DeliveredAt - order.CreatedAt ==
                minimumDeliveryTime)
            .OrderBy(order => order.Id)
            .Select(order => new OrderDeliveryTimeDto(
                order.Id,
                order.DeliveredAt - order.CreatedAt))
            .ToList();
    }

    /// <inheritdoc />
    public async Task<IList<RestaurantOrderCountDto>>
        GetTopRestaurantsByOrderCountAsync(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(count),
                "Количество ресторанов должно быть положительным");
        }

        var orders = await orderRepository.GetAllAsync();

        return orders
            .GroupBy(order => order.RestaurantId)
            .Select(group =>
            {
                var restaurant = group.First().Restaurant;

                return new RestaurantOrderCountDto(
                    group.Key,
                    restaurant.Name,
                    group.Count());
            })
            .OrderByDescending(restaurant => restaurant.OrderCount)
            .ThenBy(restaurant => restaurant.RestaurantId)
            .Take(count)
            .ToList();
    }
}