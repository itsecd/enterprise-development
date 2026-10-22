using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализует репозиторий позиций заказов
/// </summary>
public class OrderItemRepository()
    : InMemoryRepository<OrderItem>(TestData.OrderItems),
      IOrderItemRepository
{
    /// <inheritdoc />
    public override OrderItem? Update(OrderItem orderItem)
    {
        var existingOrderItem = GetById(orderItem.Id);

        if (existingOrderItem is null)
        {
            return null;
        }

        existingOrderItem.OrderId = orderItem.OrderId;
        existingOrderItem.DishId = orderItem.DishId;
        existingOrderItem.Dish = orderItem.Dish;
        existingOrderItem.Quantity = orderItem.Quantity;

        return existingOrderItem;
    }
}
