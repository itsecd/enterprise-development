using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EfCore.Repositories;

/// <summary>Хранит блюда в MySQL</summary>
/// <param name="context">Контекст базы данных</param>
public sealed class DishRepository(FoodDeliveryDbContext context)
    : EfCoreRepository<Dish>(context), IDishRepository
{
    /// <inheritdoc/>
    protected override IQueryable<Dish> Query => Context.Dishes
        .Include(dish => dish.Category)
        .Include(dish => dish.Restaurant);

    /// <inheritdoc/>
    public Task<bool> IsUsedInOrdersAsync(int dishId, CancellationToken cancellationToken = default)
    {
        return Context.OrderItems.AnyAsync(item => item.DishId == dishId, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Dish>> GetByCategoryAsync(
        int categoryId, CancellationToken cancellationToken = default)
    {
        return await Query.Where(dish => dish.CategoryId == categoryId)
            .OrderBy(dish => dish.Id)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Dish>> GetByRestaurantAsync(
        int restaurantId, CancellationToken cancellationToken = default)
    {
        return await Query.Where(dish => dish.RestaurantId == restaurantId)
            .OrderBy(dish => dish.Id)
            .ToListAsync(cancellationToken);
    }
}
