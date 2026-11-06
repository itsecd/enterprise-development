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
    public IReadOnlyList<RestaurantOrderStatisticsDto> GetTopRestaurants()
    {
        return repository.Execute<IReadOnlyList<RestaurantOrderStatisticsDto>>(() =>
        {
            return repository.GetTopRestaurants()
                .Select(item => new RestaurantOrderStatisticsDto(
                    item.Restaurant.Id,
                    item.Restaurant.Name,
                    item.OrderCount))
                .ToArray();
        });
    }

    /// <inheritdoc/>
    public IReadOnlyList<OrderDto> GetOrdersWithMinimumDeliveryTime()
    {
        return repository.Execute<IReadOnlyList<OrderDto>>(() =>
        {
            return repository.GetOrdersWithMinimumDeliveryTime()
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
        });
    }

    /// <inheritdoc/>
    public IReadOnlyList<CustomerDto> GetRestaurantCustomers(int restaurantId)
    {
        return repository.Execute<IReadOnlyList<CustomerDto>>(() =>
        {
            if (restaurantRepository.GetById(restaurantId) is null)
            {
                throw new ResourceNotFoundException(
                    $"Ресторан с идентификатором {restaurantId} не найден");
            }

            return repository.GetRestaurantCustomers(restaurantId)
                .Select(ToCustomerDto)
                .ToArray();
        });
    }

    /// <inheritdoc/>
    public IReadOnlyList<CategoryOrderStatisticsDto> GetCategoryStatistics(
        DateTime start,
        DateTime end)
    {
        return repository.Execute<IReadOnlyList<CategoryOrderStatisticsDto>>(() =>
        {
            return repository.GetCategoryStatistics(start, end)
                .Select(item => new CategoryOrderStatisticsDto(
                    item.Category.Id,
                    item.Category.Name,
                    item.OrderCount,
                    item.AverageAmount,
                    item.TotalAmount))
                .ToArray();
        });
    }

    /// <inheritdoc/>
    public CustomerSpendingDto? GetTopSpendingCustomer()
    {
        return repository.Execute<CustomerSpendingDto?>(() =>
        {
            var result = repository.GetTopSpendingCustomer();

            return result is null
                ? null
                : new CustomerSpendingDto(
                    ToCustomerDto(result.Customer),
                    result.TotalAmount);
        });
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
