using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Репозиторий автомобилей
/// </summary>
public interface ICarRepository : IRepository<Car>
{
    /// <summary>
    /// Получить автомобили владельца
    /// </summary>
    Task<IReadOnlyList<Car>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить владельца автомобиля
    /// </summary>
    Task<Customer?> GetCustomerAsync(int carId, CancellationToken cancellationToken = default);
}
