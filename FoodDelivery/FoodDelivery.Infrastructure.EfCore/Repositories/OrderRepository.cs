using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EfCore.Repositories;

/// <summary>Хранит заказы и их позиции в MySQL</summary>
/// <param name="context">Контекст базы данных</param>
public sealed class OrderRepository(FoodDeliveryDbContext context)
    : EfCoreRepository<Order>(context), IOrderRepository
{
    /// <inheritdoc/>
    protected override IQueryable<Order> Query => Context.Orders
        .Include(order => order.Customer)
        .Include(order => order.Restaurant)
        .Include(order => order.Items);

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Order>> GetByCustomerAsync(
        int customerId, CancellationToken cancellationToken = default)
    {
        return await Query.Where(order => order.CustomerId == customerId)
            .OrderBy(order => order.Id)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Order>> GetByRestaurantAsync(
        int restaurantId, CancellationToken cancellationToken = default)
    {
        return await Query.Where(order => order.RestaurantId == restaurantId)
            .OrderBy(order => order.Id)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<OrderItem>?> GetItemsAsync(
        int orderId, CancellationToken cancellationToken = default)
    {
        if (!await Context.Orders.AnyAsync(order => order.Id == orderId, cancellationToken))
        {
            return null;
        }

        return await Context.OrderItems.Include(item => item.Dish)
            .Where(item => item.OrderId == orderId)
            .OrderBy(item => item.Id)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public Task<OrderItem?> GetItemAsync(
        int orderId, int itemId, CancellationToken cancellationToken = default)
    {
        return Context.OrderItems.Include(item => item.Dish)
            .FirstOrDefaultAsync(item => item.OrderId == orderId && item.Id == itemId, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<OrderItem?> AddItemAsync(
        int orderId, OrderItem item, CancellationToken cancellationToken = default)
    {
        if (await Context.Orders.FindAsync([orderId], cancellationToken) is null)
        {
            return null;
        }

        item.OrderId = orderId;
        Context.OrderItems.Add(item);
        await Context.SaveChangesAsync(cancellationToken);
        return item;
    }

    /// <inheritdoc/>
    public async Task<OrderItem?> UpdateItemAsync(
        int orderId, int itemId, int quantity, CancellationToken cancellationToken = default)
    {
        var item = await GetItemAsync(orderId, itemId, cancellationToken);
        if (item is null)
        {
            return null;
        }

        item.Quantity = quantity;
        await Context.SaveChangesAsync(cancellationToken);
        return item;
    }

    /// <inheritdoc/>
    public async Task DeleteItemAsync(
        int orderId, int itemId, CancellationToken cancellationToken = default)
    {
        var item = await Context.OrderItems.FirstOrDefaultAsync(
            item => item.OrderId == orderId && item.Id == itemId, cancellationToken);

        if (item is null)
        {
            return;
        }

        Context.OrderItems.Remove(item);
        await Context.SaveChangesAsync(cancellationToken);
    }
}
