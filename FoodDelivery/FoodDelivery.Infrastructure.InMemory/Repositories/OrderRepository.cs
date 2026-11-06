using FoodDelivery.Domain.Exceptions;
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
    private readonly Dictionary<int, int> _lastItemIds = [];

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

            if (!_lastItemIds.TryGetValue(orderId, out var lastId))
            {
                lastId = order.Items
                    .Select(position => position.Id)
                    .DefaultIfEmpty()
                    .Max();
            }

            var nextId = checked(lastId + 1);

            var position = new OrderItem
            {
                Id = nextId,
                Dish = item.Dish,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            };

            EnsureAmountFits(order, null, position.UnitPrice, position.Quantity);
            order.Items.Add(position);
            _lastItemIds[orderId] = nextId;

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

            EnsureAmountFits(order!, position, position.UnitPrice, quantity);
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

    private void EnsureAmountFits(
        Order order,
        OrderItem? previous,
        decimal price,
        int quantity)
    {
        if (price <= 0 || quantity <= 0)
        {
            throw new OrderAmountException(
                "Цена и количество должны быть положительными");
        }

        try
        {
            var previousAmount = previous is null
                ? 0m
                : previous.UnitPrice * previous.Quantity;

            var newAmount = checked(price * quantity);

            _ = checked(order.TotalAmount - previousAmount + newAmount);

            _ = checked(
                data.Orders.Sum(item => item.TotalAmount)
                - previousAmount
                + newAmount);
        }
        catch (OverflowException)
        {
            throw new OrderAmountException(
                "Сумма операции превышает допустимый диапазон decimal");
        }
    }
}
