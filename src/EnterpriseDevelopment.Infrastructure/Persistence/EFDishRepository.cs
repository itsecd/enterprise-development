using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseDevelopment.Infrastructure.Persistence;

/// <summary>
/// Репозиторий блюд на основе Entity Framework Core
/// </summary>
public class EfDishRepository : EfRepository<Dish>, IDishRepository
{
    private readonly ApplicationDbContext _context;

    public EfDishRepository(ApplicationDbContext context)
        : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает все блюда со связанными данными
    /// </summary>
    public override IEnumerable<Dish> GetAll()
    {
        return _context.Dishes
            .Include(dish => dish.Category)
            .Include(dish => dish.Restaurant)
            .ToList();
    }

    /// <summary>
    /// Получает блюдо по идентификатору со связанными данными
    /// </summary>
    public override Dish? GetById(int id)
    {
        return _context.Dishes
            .Include(dish => dish.Category)
            .Include(dish => dish.Restaurant)
            .FirstOrDefault(dish => dish.Id == id);
    }
}
