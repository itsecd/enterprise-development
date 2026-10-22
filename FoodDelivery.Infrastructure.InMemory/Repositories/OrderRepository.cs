using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализует репозиторий заказов
/// </summary>
public class OrderRepository() : InMemoryRepository<Order>(TestData.Orders), IOrderRepository
{
    /// <inheritdoc />
    public override Order? Update(Order order)
    {
        var existingOrder = GetById(order.Id);

        if (existingOrder is null)
        {
            return null;
        }

        existingOrder.CustomerId = order.CustomerId;
        existingOrder.Customer = order.Customer;
        existingOrder.RestaurantId = order.RestaurantId;
        existingOrder.Restaurant = order.Restaurant;
        existingOrder.CreatedAt = order.CreatedAt;
        existingOrder.DeliveredAt = order.DeliveredAt;

        return existingOrder;
    }

    /// <inheritdoc />
    public override bool Delete(int id)
    {
        var order = GetById(id);

        if (order is null)
        {
            return false;
        }

        var relatedItems = TestData.OrderItems
            .Where(item => item.OrderId == id)
            .ToList();

        foreach (var item in relatedItems)
        {
            TestData.OrderItems.Remove(item);
        }

        order.Items.Clear();

        return base.Delete(id);
    }
}
