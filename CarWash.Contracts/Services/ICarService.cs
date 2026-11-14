using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;

namespace CarWash.Contracts.Services;

/// <summary>
/// Операции с автомобилями
/// </summary>
public interface ICarService : ICrudService<CarRequest, CarResponse>
{
    /// <summary>
    /// Получить владельца автомобиля
    /// </summary>
    Task<CustomerResponse> GetCustomerAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить заказы автомобиля
    /// </summary>
    Task<IReadOnlyList<OrderResponse>> GetOrdersAsync(int id, CancellationToken cancellationToken = default);
}
