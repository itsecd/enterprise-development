using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Хранит заказы и их позиции в памяти
/// </summary>
/// <param name="data">Данные приложения</param>
public sealed class OrderRepository(FoodDeliveryData data)
    : InMemoryRepository<Order>(
        data.Orders,
        order => order.Id,
        InMemorySynchronization.For(data)),
        IOrderRepository
{
    private int _lastItemId = data.Orders
        .SelectMany(order => order.Items)
        .Select(item => item.Id)
        .DefaultIfEmpty()
        .Max();

    /// <inheritdoc/>
    public IReadOnlyList<Order> GetByCustomer(int customerId)
    {
        lock (SyncRoot)
        {
            return data.Orders
                .Where(order => order.Customer.Id == customerId)
                .ToArray();
        }
    }

    /// <inheritdoc/>
    public IReadOnlyList<Order> GetByRestaurant(int restaurantId)
    {
        lock (SyncRoot)
        {
            return data.Orders
                .Where(order => order.Restaurant.Id == restaurantId)
                .ToArray();
        }
    }

    /// <inheritdoc/>
    public IReadOnlyList<OrderItem>? GetItems(int orderId)
    {
        lock (SyncRoot)
        {
            var order = data.Orders.FirstOrDefault(item => item.Id == orderId);

            return order?.Items.ToArray();
        }
    }

    /// <inheritdoc/>
    public OrderItem? GetItem(int orderId, int itemId)
    {
        lock (SyncRoot)
        {
            var order = data.Orders.FirstOrDefault(item => item.Id == orderId);

            return order?.Items.FirstOrDefault(item => item.Id == itemId);
        }
    }

    /// <inheritdoc/>
    public OrderItem? AddItem(int orderId, OrderItem item)
    {
        lock (SyncRoot)
        {
            var order = data.Orders.FirstOrDefault(entity => entity.Id == orderId);

            if (order is null)
            {
                return null;
            }

            var nextId = checked(_lastItemId + 1);

            var position = new OrderItem
            {
                Id = nextId,
                OrderId = orderId,
                DishId = item.Dish.Id,
                Dish = item.Dish,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            };

            order.Items.Add(position);
            _lastItemId = nextId;

            return position;
        }
    }

    /// <inheritdoc/>
    public OrderItem? UpdateItem(int orderId, int itemId, int quantity)
    {
        lock (SyncRoot)
        {
            var order = data.Orders.FirstOrDefault(item => item.Id == orderId);
            var position = order?.Items.FirstOrDefault(item => item.Id == itemId);

            if (position is null)
            {
                return null;
            }

            position.Quantity = quantity;

            return position;
        }
    }

    /// <inheritdoc/>
    public void DeleteItem(int orderId, int itemId)
    {
        lock (SyncRoot)
        {
            var order = data.Orders.FirstOrDefault(item => item.Id == orderId);

            order?.Items.RemoveAll(item => item.Id == itemId);
        }
    }

    /// <inheritdoc/>
    protected override void SetId(Order entity, int id)
    {
        entity.Id = id;
    }

    /// <inheritdoc/>
    protected override void UpdateEntity(
        Order existingEntity,
        Order updatedEntity)
    {
        existingEntity.Customer = updatedEntity.Customer;
        existingEntity.Restaurant = updatedEntity.Restaurant;
        existingEntity.CreatedAt = updatedEntity.CreatedAt;
        existingEntity.DeliveredAt = updatedEntity.DeliveredAt;
    }

    /// <inheritdoc/>
    public Task<IReadOnlyList<Order>> GetByCustomerAsync(int customerId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(GetByCustomer(customerId));
    }

    /// <inheritdoc/>
    public Task<IReadOnlyList<Order>> GetByRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(GetByRestaurant(restaurantId));
    }

    /// <inheritdoc/>
    public Task<IReadOnlyList<OrderItem>?> GetItemsAsync(int orderId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(GetItems(orderId));
    }

    /// <inheritdoc/>
    public Task<OrderItem?> GetItemAsync(int orderId, int itemId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(GetItem(orderId, itemId));
    }

    /// <inheritdoc/>
    public Task<OrderItem?> AddItemAsync(int orderId, OrderItem item, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(AddItem(orderId, item));
    }

    /// <inheritdoc/>
    public Task<OrderItem?> UpdateItemAsync(int orderId, int itemId, int quantity, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(UpdateItem(orderId, itemId, quantity));
    }

    /// <inheritdoc/>
    public Task DeleteItemAsync(int orderId, int itemId, CancellationToken cancellationToken = default)
    {
        DeleteItem(orderId, itemId);
        return Task.CompletedTask;
    }
}
