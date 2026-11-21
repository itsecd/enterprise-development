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
/// <param name="transaction">Транзакция записи связанных данных заказа</param>
public sealed class OrderService(
    IOrderRepository repository,
    ICustomerRepository customerRepository,
    IRestaurantRepository restaurantRepository,
    IOrderWriteTransaction transaction)
    : CrudServiceBase<Order, OrderDto, OrderRequest>, IOrderService
{
    /// <inheritdoc/>
    protected override IRepository<Order> Repository => repository;

    /// <inheritdoc/>
    public override Task<OrderDto> UpdateAsync(
        int id, OrderRequest request, CancellationToken cancellationToken = default)
    {
        return transaction.ExecuteAsync(token => base.UpdateAsync(id, request, token), cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<OrderDto>> GetByCustomerAsync(int customerId, CancellationToken cancellationToken = default)
    {
        if (await customerRepository.GetByIdAsync(customerId, cancellationToken) is null)
        {
            throw new ResourceNotFoundException(
                $"Клиент с идентификатором {customerId} не найден");
        }

        return (await repository.GetByCustomerAsync(customerId, cancellationToken))
            .Select(ToDto)
            .ToArray();
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<OrderDto>> GetByRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default)
    {
        if (await restaurantRepository.GetByIdAsync(restaurantId, cancellationToken) is null)
        {
            throw new ResourceNotFoundException(
                $"Ресторан с идентификатором {restaurantId} не найден");
        }

        return (await repository.GetByRestaurantAsync(restaurantId, cancellationToken))
            .Select(ToDto)
            .ToArray();
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
    protected override async Task<Order> ToEntityAsync(
        OrderRequest request,
        int id,
        Order? existing,
        CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.CustomerId, cancellationToken)
            ?? throw new InvalidReferenceException(
                $"Клиент с идентификатором {request.CustomerId} не найден");

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId, cancellationToken)
            ?? throw new InvalidReferenceException(
                $"Ресторан с идентификатором {request.RestaurantId} не найден");

        if (existing is not null &&
            existing.Items.Count > 0 &&
            existing.Restaurant.Id != restaurant.Id)
        {
            throw new ResourceConflictException(
                "Нельзя изменить ресторан заказа, пока в заказе есть позиции");
        }

        return new Order
        {
            Id = id,
            CustomerId = customer.Id,
            RestaurantId = restaurant.Id,
            Customer = customer,
            Restaurant = restaurant,
            CreatedAt = request.CreatedAt,
            DeliveredAt = request.DeliveredAt,
            Items = existing?.Items ?? []
        };
    }
}
