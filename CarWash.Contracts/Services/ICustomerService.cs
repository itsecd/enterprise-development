using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;

namespace CarWash.Contracts.Services;

/// <summary>
/// Операции с клиентами
/// </summary>
public interface ICustomerService : ICrudService<CustomerRequest, CustomerResponse>
{
    /// <summary>
    /// Получить автомобили клиента
    /// </summary>
    Task<IReadOnlyList<CarResponse>> GetCarsAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить заказы клиента
    /// </summary>
    Task<IReadOnlyList<OrderResponse>> GetOrdersAsync(int id, CancellationToken cancellationToken = default);
}
