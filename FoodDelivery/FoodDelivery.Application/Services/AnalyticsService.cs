using FoodDelivery.Application.Contracts.Analytics;
using FoodDelivery.Application.Contracts.Customers;
using FoodDelivery.Application.Contracts.Orders;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Предоставляет аналитические результаты в виде контрактов приложения
/// </summary>
/// <param name="repository">Аналитический репозиторий</param>
/// <param name="restaurantRepository">Репозиторий ресторанов</param>
public sealed class AnalyticsService(
    IAnalyticsRepository repository,
    IRestaurantRepository restaurantRepository) : IAnalyticsService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<RestaurantOrderStatisticsDto>> GetTopRestaurantsAsync(CancellationToken cancellationToken = default)
    {
        return (await repository.GetTopRestaurantsAsync(cancellationToken))
            .Select(item => new RestaurantOrderStatisticsDto(
                item.Restaurant.Id,
                item.Restaurant.Name,
                item.OrderCount))
            .ToArray();
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<OrderDto>> GetOrdersWithMinimumDeliveryTimeAsync(CancellationToken cancellationToken = default)
    {
        return (await repository.GetOrdersWithMinimumDeliveryTimeAsync(cancellationToken))
            .Select(order => new OrderDto
            {
                Id = order.Id,
                CustomerId = order.Customer.Id,
                RestaurantId = order.Restaurant.Id,
                CreatedAt = order.CreatedAt,
                DeliveredAt = order.DeliveredAt,
                TotalAmount = order.TotalAmount
            })
            .ToArray();
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<CustomerDto>> GetRestaurantCustomersAsync(int restaurantId, CancellationToken cancellationToken = default)
    {
        if (await restaurantRepository.GetByIdAsync(restaurantId, cancellationToken) is null)
        {
            throw new ResourceNotFoundException(
                $"Ресторан с идентификатором {restaurantId} не найден");
        }

        return (await repository.GetRestaurantCustomersAsync(restaurantId, cancellationToken))
            .Select(ToCustomerDto)
            .ToArray();
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<CategoryOrderStatisticsDto>> GetCategoryStatisticsAsync(
        DateTime start,
        DateTime end,
        CancellationToken cancellationToken = default)
    {
        return (await repository.GetCategoryStatisticsAsync(start, end, cancellationToken))
            .Select(item => new CategoryOrderStatisticsDto(
                item.Category.Id,
                item.Category.Name,
                item.OrderCount,
                item.AverageAmount,
                item.TotalAmount))
            .ToArray();
    }

    /// <inheritdoc/>
    public async Task<CustomerSpendingDto> GetTopSpendingCustomerAsync(CancellationToken cancellationToken = default)
    {
        var result = await repository.GetTopSpendingCustomerAsync(cancellationToken)
            ?? throw new ResourceNotFoundException("В системе нет заказов");

        return new CustomerSpendingDto(
            ToCustomerDto(result.Customer),
            result.TotalAmount);
    }

    private static CustomerDto ToCustomerDto(Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            LastName = customer.LastName,
            FirstName = customer.FirstName,
            Patronymic = customer.Patronymic,
            Phone = customer.Phone,
            DeliveryAddress = customer.DeliveryAddress
        };
    }
}
