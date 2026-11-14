using CarWash.Domain.Models;
using CarWash.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.Ef.Repositories;

/// <summary>
/// Репозиторий заказов и аналитики боксов в PostgreSQL
/// </summary>
public class EfOrderRepository(CarWashDbContext context) : EfRepository<Order>(context), IOrderRepository
{
    /// <inheritdoc />
    protected override IQueryable<Order> Query => Context.Orders
        .Include(order => order.Customer).Include(order => order.Car).Include(order => order.WashService);

    /// <inheritdoc />
    public async Task<IReadOnlyList<Order>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default) =>
        await Query.AsNoTracking().Where(order => order.CustomerId == customerId).OrderBy(order => order.Id).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IReadOnlyList<Order>> GetByCarIdAsync(int carId, CancellationToken cancellationToken = default) =>
        await Query.AsNoTracking().Where(order => order.CarId == carId).OrderBy(order => order.Id).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IReadOnlyList<Order>> GetByWashServiceIdAsync(int washServiceId, CancellationToken cancellationToken = default) =>
        await Query.AsNoTracking().Where(order => order.WashServiceId == washServiceId).OrderBy(order => order.Id).ToListAsync(cancellationToken);

    /// <inheritdoc />
    public Task<Customer?> GetCustomerAsync(int orderId, CancellationToken cancellationToken = default) =>
        Context.Orders.AsNoTracking().Where(order => order.Id == orderId).Select(order => order.Customer).FirstOrDefaultAsync(cancellationToken);

    /// <inheritdoc />
    public Task<Car?> GetCarAsync(int orderId, CancellationToken cancellationToken = default) =>
        Context.Orders.AsNoTracking().Where(order => order.Id == orderId).Select(order => order.Car).FirstOrDefaultAsync(cancellationToken);

    /// <inheritdoc />
    public Task<WashService?> GetWashServiceAsync(int orderId, CancellationToken cancellationToken = default) =>
        Context.Orders.AsNoTracking().Where(order => order.Id == orderId).Select(order => order.WashService).FirstOrDefaultAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<IReadOnlyList<Car>> GetCarsCurrentlyAtWashAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.Now;
        return await Context.Cars.AsNoTracking().Where(car => Context.Orders.Any(order => order.CarId == car.Id
                && order.StartTime <= now && order.StartTime.AddMinutes(order.WashService.DurationMinutes) > now))
            .OrderBy(car => car.Id).ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<DateTime> GetBoxAvailableFromAsync(int boxNumber, CancellationToken cancellationToken = default)
    {
        var now = DateTime.Now;
        var end = await Context.Orders.Where(order => order.BoxNumber == boxNumber && order.StartTime <= now
                && order.StartTime.AddMinutes(order.WashService.DurationMinutes) > now)
            .Select(order => (DateTime?)order.StartTime.AddMinutes(order.WashService.DurationMinutes))
            .OrderBy(value => value).FirstOrDefaultAsync(cancellationToken);
        return end ?? now;
    }
}
