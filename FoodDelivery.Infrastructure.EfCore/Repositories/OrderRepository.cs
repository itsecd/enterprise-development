using FoodDelivery.Domain.Repositories;
using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EfCore.Repositories;

/// <summary>
/// Реализует репозиторий заказов с использованием efcore
/// </summary>
public class OrderRepository(FoodDeliveryDbContext dbContext)
    : EfRepository<Order>(dbContext),
      IOrderRepository
{
    /// <inheritdoc />
    public override async Task<Order?> GetByIdAsync(int id)
    {
        return await DbContext.Orders
            .Include(order => order.Customer)
            .Include(order => order.Restaurant)
            .Include(order => order.Items)
                .ThenInclude(item => item.Dish)
            .FirstOrDefaultAsync(order => order.Id == id);
    }

    /// <inheritdoc />
    public override async Task<IList<Order>> GetAllAsync()
    {
        return await DbContext.Orders
            .Include(order => order.Customer)
            .Include(order => order.Restaurant)
            .Include(order => order.Items)
                .ThenInclude(item => item.Dish)
            .ToListAsync();
    }

    /// <inheritdoc />
    public override async Task<bool> DeleteAsync(int id)
    {
        var order = await DbContext.Orders
            .Include(order => order.Items)
            .FirstOrDefaultAsync(order => order.Id == id);

        if (order is null)
        {
            return false;
        }

        DbContext.Orders.Remove(order);
        await DbContext.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task<IList<Order>> GetNotDeliveredAsync()
    {
        return await DbContext.Orders
            .Include(order => order.Customer)
            .Include(order => order.Restaurant)
            .Include(order => order.Items)
                .ThenInclude(item => item.Dish)
            .Where(order => order.DeliveredAt == null)
            .ToListAsync();
    }
}