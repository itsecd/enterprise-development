using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EfCore.Repositories;

/// <summary>Хранит рестораны в MySQL</summary>
/// <param name="context">Контекст базы данных</param>
public sealed class RestaurantRepository(FoodDeliveryDbContext context)
    : EfCoreRepository<Restaurant>(context), IRestaurantRepository
{
    /// <inheritdoc/>
    public Task<bool> HasDishesAsync(int restaurantId, CancellationToken cancellationToken = default)
    {
        return Context.Dishes.AnyAsync(dish => dish.RestaurantId == restaurantId, cancellationToken);
    }

    /// <inheritdoc/>
    public Task<bool> HasOrdersAsync(int restaurantId, CancellationToken cancellationToken = default)
    {
        return Context.Orders.AnyAsync(order => order.RestaurantId == restaurantId, cancellationToken);
    }
}
