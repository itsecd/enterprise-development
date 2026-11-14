using CarWash.Domain.Data;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Infrastructure.Repositories;

/// <summary>
/// Репозиторий клиентов в памяти для тестов
/// </summary>
public class InMemoryCustomerRepository(CarWashData data, IOrderRepository orders, ICarRepository cars)
    : InMemoryRepository<Customer>(data.Customers), ICustomerRepository
{
    /// <inheritdoc />
    public Task<IReadOnlyList<Car>> GetCarsAsync(int customerId, CancellationToken cancellationToken = default) =>
        cars.GetByCustomerIdAsync(customerId, cancellationToken);

    /// <inheritdoc />
    public async Task<IReadOnlyList<(Customer Customer, int VisitCount)>> GetTop5CustomersByVisitsAsync(CancellationToken cancellationToken = default)
    {
        var visits = (await orders.GetAllAsync(cancellationToken))
            .GroupBy(order => order.CustomerId).ToDictionary(group => group.Key, group => group.Count());
        return Items.Values.Select(customer => (Customer: customer, VisitCount: visits.GetValueOrDefault(customer.Id)))
            .OrderByDescending(item => item.VisitCount).ThenBy(item => item.Customer.Id).Take(5).ToList();
    }
}
