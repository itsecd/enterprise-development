using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;

namespace CarWash.Contracts.Services;

/// <summary>
/// Операции с заказами
/// </summary>
public interface IOrderService : ICrudService<OrderRequest, OrderResponse>
{
    /// <summary>
    /// Получить клиента заказа
    /// </summary>
    Task<CustomerResponse> GetCustomerAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить автомобиль заказа
    /// </summary>
    Task<CarResponse> GetCarAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить услугу заказа
    /// </summary>
    Task<WashServiceResponse> GetWashServiceAsync(int id, CancellationToken cancellationToken = default);
}
