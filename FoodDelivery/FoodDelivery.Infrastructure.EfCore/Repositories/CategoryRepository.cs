using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EfCore.Repositories;

/// <summary>Хранит категории блюд в MySQL</summary>
/// <param name="context">Контекст базы данных</param>
public sealed class CategoryRepository(FoodDeliveryDbContext context)
    : EfCoreRepository<Category>(context), ICategoryRepository
{
    /// <inheritdoc/>
    public Task<bool> HasDishesAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        return Context.Dishes.AnyAsync(dish => dish.CategoryId == categoryId, cancellationToken);
    }
}
