using CarWash.Domain.Models;

namespace CarWash.Domain.Repositories;

/// <summary>
/// Репозиторий заказов
/// </summary>
public interface IOrderRepository : IRepository<Order>
{
    /// <summary>
    /// Получить заказы клиента
    /// </summary>
    Task<IReadOnlyList<Order>> GetByCustomerIdAsync(int customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить заказы автомобиля
    /// </summary>
    Task<IReadOnlyList<Order>> GetByCarIdAsync(int carId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить заказы с указанной услугой
    /// </summary>
    Task<IReadOnlyList<Order>> GetByWashServiceIdAsync(int washServiceId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить клиента заказа
    /// </summary>
    Task<Customer?> GetCustomerAsync(int orderId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить автомобиль заказа
    /// </summary>
    Task<Car?> GetCarAsync(int orderId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить услугу заказа
    /// </summary>
    Task<WashService?> GetWashServiceAsync(int orderId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить автомобили, которые сейчас находятся на мойке
    /// </summary>
    Task<IReadOnlyList<Car>> GetCarsCurrentlyAtWashAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Определить время освобождения бокса
    /// </summary>
    Task<DateTime> GetBoxAvailableFromAsync(int boxNumber, CancellationToken cancellationToken = default);
}
