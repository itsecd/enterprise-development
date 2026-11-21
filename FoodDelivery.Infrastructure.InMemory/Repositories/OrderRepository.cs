using FoodDelivery.Domain.Repositories;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализует репозиторий заказов
/// </summary>
public class OrderRepository()
    : InMemoryRepository<Order>(TestData.Orders),
      IOrderRepository
{
    /// <inheritdoc />
    public override async Task<Order?> UpdateAsync(Order order)
    {
        var existingOrder = await GetByIdAsync(order.Id);

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
    public override async Task<bool> DeleteAsync(int id)
    {
        var order = await GetByIdAsync(id);

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

        return await base.DeleteAsync(id);
    }

	/// <inheritdoc />
	public Task<IList<Order>> GetNotDeliveredAsync()
	{
		var notDeliveredOrders = TestData.Orders
			.Where(order => order.DeliveredAt == null)
			.ToList();

		return Task.FromResult<IList<Order>>(notDeliveredOrders);
	}
}