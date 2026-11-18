using FoodDelivery.Application.Contracts.Orders;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Выполняет операции с заказами
/// </summary>
/// <param name="repository">Репозиторий заказов</param>
/// <param name="customerRepository">Репозиторий клиентов</param>
/// <param name="restaurantRepository">Репозиторий ресторанов</param>
public sealed class OrderService(
    IOrderRepository repository,
    ICustomerRepository customerRepository,
    IRestaurantRepository restaurantRepository)
    : CrudServiceBase<Order, OrderDto, OrderRequest>, IOrderService
{
    /// <inheritdoc/>
    protected override IRepository<Order> Repository => repository;

    /// <inheritdoc/>
    public IReadOnlyList<OrderDto> GetByCustomer(int customerId)
    {
        return repository.Execute<IReadOnlyList<OrderDto>>(() =>
        {
            if (customerRepository.GetById(customerId) is null)
            {
                throw new ResourceNotFoundException(
                    $"Клиент с идентификатором {customerId} не найден");
            }

            return repository.GetByCustomer(customerId)
                .Select(ToDto)
                .ToArray();
        });
    }

    /// <inheritdoc/>
    public IReadOnlyList<OrderDto> GetByRestaurant(int restaurantId)
    {
        return repository.Execute<IReadOnlyList<OrderDto>>(() =>
        {
            if (restaurantRepository.GetById(restaurantId) is null)
            {
                throw new ResourceNotFoundException(
                    $"Ресторан с идентификатором {restaurantId} не найден");
            }

            return repository.GetByRestaurant(restaurantId)
                .Select(ToDto)
                .ToArray();
        });
    }

    /// <inheritdoc/>
    protected override OrderDto ToDto(Order entity)
    {
        return new OrderDto
        {
            Id = entity.Id,
            CustomerId = entity.Customer.Id,
            RestaurantId = entity.Restaurant.Id,
            CreatedAt = entity.CreatedAt,
            DeliveredAt = entity.DeliveredAt,
            TotalAmount = entity.TotalAmount
        };
    }

    /// <inheritdoc/>
    protected override Order ToEntity(OrderRequest request, int id)
    {
        var customer = customerRepository.GetById(request.CustomerId)
            ?? throw new InvalidReferenceException(
                $"Клиент с идентификатором {request.CustomerId} не найден");

        var restaurant = restaurantRepository.GetById(request.RestaurantId)
            ?? throw new InvalidReferenceException(
                $"Ресторан с идентификатором {request.RestaurantId} не найден");

        var existingOrder = id == 0 ? null : repository.GetById(id);

        if (existingOrder is not null &&
            existingOrder.Items.Count > 0 &&
            existingOrder.Restaurant.Id != restaurant.Id)
        {
            throw new ResourceConflictException(
                "Нельзя изменить ресторан заказа, пока в заказе есть позиции");
        }

        return new Order
        {
            Id = id,
            Customer = customer,
            Restaurant = restaurant,
            CreatedAt = request.CreatedAt,
            DeliveredAt = request.DeliveredAt,
            Items = existingOrder?.Items ?? []
        };
    }
}
