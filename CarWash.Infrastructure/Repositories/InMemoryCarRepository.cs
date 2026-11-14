using CarWash.Domain.Data;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Репозиторий автомобилей в памяти для тестов
/// </summary>
public class InMemoryCarRepository(CarWashData data) : InMemoryRepository<Car>(data.Cars), ICarRepository
{
    /// <inheritdoc />
    public Task<IReadOnlyList<Car>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<Car>>(Items.Values.Where(car => car.CustomerId == customerId).OrderBy(car => car.Id).ToList());

    /// <inheritdoc />
    public Task<Customer?> GetCustomerAsync(int carId, CancellationToken cancellationToken = default) =>
        Task.FromResult(Items.GetValueOrDefault(carId)?.Customer);

    /// <inheritdoc />
    public override async Task AddAsync(Car entity, CancellationToken cancellationToken = default)
    {
        await base.AddAsync(entity, cancellationToken);
        entity.Customer.Cars = [.. entity.Customer.Cars, entity];
    }

    /// <inheritdoc />
    public override Task UpdateAsync(Car entity, CancellationToken cancellationToken = default)
    {
        var current = Items.GetValueOrDefault(entity.Id);
        if (current is null)
            return Task.CompletedTask;
        if (current.CustomerId != entity.CustomerId)
        {
            current.Customer.Cars = current.Customer.Cars.Where(car => car.Id != current.Id).ToList();
            entity.Customer.Cars = [.. entity.Customer.Cars, current];
        }
        current.LicensePlate = entity.LicensePlate;
        current.Brand = entity.Brand;
        current.Customer = entity.Customer;
        current.CustomerId = entity.CustomerId;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public override Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (Items.TryRemove(id, out var car))
            car.Customer.Cars = car.Customer.Cars.Where(item => item.Id != id).ToList();
        return Task.CompletedTask;
    }
}
