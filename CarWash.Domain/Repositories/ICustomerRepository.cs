using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Репозиторий клиентов
/// </summary>
public interface ICustomerRepository : IRepository<Customer>
{
    /// <summary>
    /// Получить автомобили клиента
    /// </summary>
    Task<IReadOnlyList<Car>> GetCarsAsync(int customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить пять клиентов с наибольшим количеством посещений
    /// </summary>
    Task<IReadOnlyList<(Customer Customer, int VisitCount)>> GetTop5CustomersByVisitsAsync(CancellationToken cancellationToken = default);
}
