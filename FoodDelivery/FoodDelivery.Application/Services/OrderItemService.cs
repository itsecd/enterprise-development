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
public sealed class OrderItemService(
    IOrderRepository repository,
    IDishRepository dishRepository) : IOrderItemService
{
    /// <inheritdoc/>
    public IReadOnlyList<OrderItemDto> GetAll(int orderId)
    {
        return repository.Execute<IReadOnlyList<OrderItemDto>>(() =>
        {
            var items = repository.GetItems(orderId)
                ?? throw MissingOrder(orderId);

            return items.Select(ToDto).ToArray();
        });
    }

    /// <inheritdoc/>
    public OrderItemDto GetById(int orderId, int itemId)
    {
        return repository.Execute<OrderItemDto>(() =>
        {
            var item = repository.GetItem(orderId, itemId)
                ?? throw MissingItem(orderId, itemId);

            return ToDto(item);
        });
    }

    /// <inheritdoc/>
    public OrderItemDto Create(int orderId, AddOrderItemRequest request)
    {
        return repository.Execute<OrderItemDto>(() =>
        {
            var order = repository.GetById(orderId)
                ?? throw MissingOrder(orderId);

            var dish = dishRepository.GetById(request.DishId)
                ?? throw new InvalidReferenceException(
                    $"Блюдо с идентификатором {request.DishId} не найдено");

            if (dish.Restaurant.Id != order.Restaurant.Id)
            {
                throw new ResourceConflictException(
                    "Блюдо должно принадлежать ресторану заказа");
            }

            var position = repository.AddItem(orderId, new OrderItem
            {
                Id = 0,
                Dish = dish,
                Quantity = request.Quantity,
                UnitPrice = dish.Price
            }) ?? throw MissingOrder(orderId);

            return ToDto(position);
        });
    }

    /// <inheritdoc/>
    public OrderItemDto Update(
        int orderId,
        int itemId,
        UpdateOrderItemRequest request)
    {
        return repository.Execute<OrderItemDto>(() =>
        {
            var item = repository.UpdateItem(orderId, itemId, request.Quantity)
                ?? throw MissingItem(orderId, itemId);

            return ToDto(item);
        });
    }

    /// <inheritdoc/>
    public void Delete(int orderId, int itemId)
    {
        repository.Execute(() =>
        {
            repository.DeleteItem(orderId, itemId);
        });
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
