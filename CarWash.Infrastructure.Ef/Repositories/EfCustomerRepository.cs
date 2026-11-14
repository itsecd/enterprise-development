using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.Ef.Repositories;

/// <summary>
/// Репозиторий клиентов и аналитики посещений в PostgreSQL
/// </summary>
public class EfCustomerRepository(CarWashDbContext context) : EfRepository<Customer>(context), ICustomerRepository
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<Car>> GetCarsAsync(int customerId, CancellationToken cancellationToken = default) =>
        await Context.Cars.AsNoTracking().Where(car => car.CustomerId == customerId)
            .OrderBy(car => car.Id).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IReadOnlyList<(Customer Customer, int VisitCount)>> GetTop5CustomersByVisitsAsync(CancellationToken cancellationToken = default)
    {
        var result = await Context.Customers.AsNoTracking()
            .Select(customer => new
            {
                Customer = customer,
                VisitCount = Context.Orders.Count(order => order.CustomerId == customer.Id)
            })
            .OrderByDescending(item => item.VisitCount).ThenBy(item => item.Customer.Id)
            .Take(5).ToListAsync(cancellationToken);
        return result.Select(item => (item.Customer, item.VisitCount)).ToList();
    }
}
