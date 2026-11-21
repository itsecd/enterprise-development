using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий заказов в базе данных
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class OrderRepository(CarWashDbContext context) : EfRepository<Order>(context), IOrderRepository
{
    /// <inheritdoc />
    protected override IQueryable<Order> Query() => Entities
        .Include(o => o.Client).ThenInclude(c => c.Cars)
        .Include(o => o.Car)
        .Include(o => o.Service);
}
