using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseDevelopment.Infrastructure.Persistence;

/// <summary>
/// Репозиторий заказов на основе Entity Framework Core
/// </summary>
public class EfOrderRepository : EfRepository<Order>, IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public EfOrderRepository(ApplicationDbContext context)
        : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Получает все заказы со связанными данными
    /// </summary>
    public override IEnumerable<Order> GetAll()
    {
        return _context.Orders
            .Include(order => order.Client)
            .Include(order => order.Restaurant)
            .Include(order => order.Dishes)
            .ThenInclude(dish => dish.Category)
            .ToList();
    }

    /// <summary>
    /// Получает заказ по идентификатору со связанными данными
    /// </summary>
    public override Order? GetById(int id)
    {
        return _context.Orders
            .Include(order => order.Client)
            .Include(order => order.Restaurant)
            .Include(order => order.Dishes)
            .ThenInclude(dish => dish.Category)
            .FirstOrDefault(order => order.Id == id);
    }
}
