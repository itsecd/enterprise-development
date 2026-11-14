using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.Ef.Repositories;

/// <summary>
/// Репозиторий автомобилей в PostgreSQL
/// </summary>
public class EfCarRepository(CarWashDbContext context) : EfRepository<Car>(context), ICarRepository
{
    /// <inheritdoc />
    protected override IQueryable<Car> Query => Context.Cars.Include(car => car.Customer);

    /// <inheritdoc />
    public async Task<IReadOnlyList<Car>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default) =>
        await Query.AsNoTracking().Where(car => car.CustomerId == customerId).OrderBy(car => car.Id).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public Task<Customer?> GetCustomerAsync(int carId, CancellationToken cancellationToken = default) =>
        Context.Cars.AsNoTracking().Where(car => car.Id == carId).Select(car => car.Customer)
            .FirstOrDefaultAsync(cancellationToken);
}
