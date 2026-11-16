using CarWash.Domain.Data;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Репозиторий заказов в памяти для тестов
/// </summary>
public class InMemoryOrderRepository(CarWashData data) : InMemoryRepository<Order>(data.Orders), IOrderRepository
{
    /// <inheritdoc />
    public Task<IReadOnlyList<Order>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<Order>>(Items.Values.Where(order => order.CustomerId == customerId).OrderBy(order => order.Id).ToList());

    /// <inheritdoc />
    public Task<IReadOnlyList<Order>> GetByCarIdAsync(int carId, CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<Order>>(Items.Values.Where(order => order.CarId == carId).OrderBy(order => order.Id).ToList());

    /// <inheritdoc />
    public Task<IReadOnlyList<Order>> GetByWashServiceIdAsync(int washServiceId, CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<Order>>(Items.Values.Where(order => order.WashServiceId == washServiceId).OrderBy(order => order.Id).ToList());

    /// <inheritdoc />
    public Task<Customer?> GetCustomerAsync(int orderId, CancellationToken cancellationToken = default) =>
        Task.FromResult(Items.GetValueOrDefault(orderId)?.Customer);

    /// <inheritdoc />
    public Task<Car?> GetCarAsync(int orderId, CancellationToken cancellationToken = default) =>
        Task.FromResult(Items.GetValueOrDefault(orderId)?.Car);

    /// <inheritdoc />
    public Task<WashService?> GetWashServiceAsync(int orderId, CancellationToken cancellationToken = default) =>
        Task.FromResult(Items.GetValueOrDefault(orderId)?.WashService);

    /// <inheritdoc />
    public Task<IReadOnlyList<Car>> GetCarsCurrentlyAtWashAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult<IReadOnlyList<Car>>(Items.Values.Where(order => order.IsInProgress).Select(order => order.Car)
            .DistinctBy(car => car.Id).OrderBy(car => car.Id).ToList());

    /// <inheritdoc />
    public Task<DateTime> GetBoxAvailableFromAsync(int boxNumber, CancellationToken cancellationToken = default)
    {
        var order = Items.Values.Where(order => order.BoxNumber == boxNumber && order.IsInProgress)
            .OrderBy(order => order.EndTime).FirstOrDefault();
        return Task.FromResult(order?.EndTime ?? DateTime.Now);
    }
}
