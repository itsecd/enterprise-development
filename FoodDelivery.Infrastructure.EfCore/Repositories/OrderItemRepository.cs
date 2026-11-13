using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.EfCore.Repositories;

/// <summary>
/// Реализует репозиторий позиций заказов с использованием efcore
/// </summary>
public class OrderItemRepository(FoodDeliveryDbContext dbContext)
    : EfRepository<OrderItem>(dbContext),
      IOrderItemRepository
{
    /// <inheritdoc />
    public override async Task<OrderItem?> UpdateAsync(
        OrderItem orderItem)
    {
        var existingOrderItem =
            await GetByIdAsync(orderItem.Id);

        if (existingOrderItem is null)
        {
            return null;
        }

        existingOrderItem.OrderId = orderItem.OrderId;
        existingOrderItem.DishId = orderItem.DishId;
        existingOrderItem.Dish = orderItem.Dish;
        existingOrderItem.Quantity = orderItem.Quantity;

        await DbContext.SaveChangesAsync();

        return existingOrderItem;
    }
}