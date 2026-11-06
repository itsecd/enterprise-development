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
    IReadOnlyList<OrderDto> GetByCustomer(int customerId);

    /// <summary>
    /// Возвращает заказы ресторана
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Заказы ресторана</returns>
    IReadOnlyList<OrderDto> GetByRestaurant(int restaurantId);
}
