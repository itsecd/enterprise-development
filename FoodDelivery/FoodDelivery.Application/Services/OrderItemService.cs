using FoodDelivery.Application.Contracts.Orders;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Выполняет операции с позициями заказа
/// </summary>
/// <param name="repository">Репозиторий заказов</param>
/// <param name="dishRepository">Репозиторий блюд</param>
/// <param name="transaction">Транзакция записи связанных данных заказа</param>
public sealed class OrderItemService(
    IOrderRepository repository,
    IDishRepository dishRepository,
    IOrderWriteTransaction transaction) : IOrderItemService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<OrderItemDto>> GetAllAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var items = await repository.GetItemsAsync(orderId, cancellationToken)
            ?? throw MissingOrder(orderId);

        return items.Select(ToDto).ToArray();
    }

    /// <inheritdoc/>
    public async Task<OrderItemDto> GetByIdAsync(int orderId, int itemId, CancellationToken cancellationToken = default)
    {
        var item = await repository.GetItemAsync(orderId, itemId, cancellationToken)
            ?? throw MissingItem(orderId, itemId);

        return ToDto(item);
    }

    /// <inheritdoc/>
    public Task<OrderItemDto> CreateAsync(int orderId, AddOrderItemRequest request, CancellationToken cancellationToken = default)
    {
        return transaction.ExecuteAsync(token => AddItemAsync(orderId, request, token), cancellationToken);
    }

    private async Task<OrderItemDto> AddItemAsync(
        int orderId, AddOrderItemRequest request, CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(orderId, cancellationToken)
            ?? throw MissingOrder(orderId);

        var dish = await dishRepository.GetByIdAsync(request.DishId, cancellationToken)
            ?? throw new InvalidReferenceException(
                $"Блюдо с идентификатором {request.DishId} не найдено");

        if (dish.Restaurant.Id != order.Restaurant.Id)
        {
            throw new ResourceConflictException(
                "Блюдо должно принадлежать ресторану заказа");
        }

        var position = await repository.AddItemAsync(orderId, new OrderItem
        {
            Id = 0,
            OrderId = orderId,
            DishId = dish.Id,
            Dish = dish,
            Quantity = request.Quantity,
            UnitPrice = dish.Price
        }, cancellationToken) ?? throw MissingOrder(orderId);

        return ToDto(position);
    }

    /// <inheritdoc/>
    public async Task<OrderItemDto> UpdateAsync(
        int orderId,
        int itemId,
        UpdateOrderItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var item = await repository.UpdateItemAsync(orderId, itemId, request.Quantity, cancellationToken)
            ?? throw MissingItem(orderId, itemId);

        return ToDto(item);
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int orderId, int itemId, CancellationToken cancellationToken = default)
    {
        await repository.DeleteItemAsync(orderId, itemId, cancellationToken);
    }

    private static OrderItemDto ToDto(OrderItem item)
    {
        return new OrderItemDto
        {
            Id = item.Id,
            DishId = item.Dish.Id,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice,
            TotalAmount = item.UnitPrice * item.Quantity
        };
    }

    private static ResourceNotFoundException MissingOrder(int orderId)
    {
        return new ResourceNotFoundException(
            $"Заказ с идентификатором {orderId} не найден");
    }

    private static ResourceNotFoundException MissingItem(int orderId, int itemId)
    {
        return new ResourceNotFoundException(
            $"Позиция {itemId} заказа {orderId} не найдена");
    }
}
