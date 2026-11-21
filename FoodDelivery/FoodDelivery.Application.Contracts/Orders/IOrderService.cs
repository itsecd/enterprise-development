using FoodDelivery.Application.Contracts.Common;

namespace FoodDelivery.Application.Contracts.Orders;

/// <summary>
/// Определяет прикладные операции с заказами
/// </summary>
public interface IOrderService : ICrudService<OrderDto, OrderRequest>
{
    /// <summary>
    /// Возвращает заказы клиента
    /// </summary>
    /// <param name="customerId">Идентификатор клиента</param>
    /// <returns>Заказы клиента</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<OrderDto>> GetByCustomerAsync(int customerId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает заказы ресторана
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Заказы ресторана</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<OrderDto>> GetByRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default);
}
