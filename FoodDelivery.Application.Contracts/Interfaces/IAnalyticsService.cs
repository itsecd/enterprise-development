using FoodDelivery.Application.Contracts.DTOs.Analytics;

namespace FoodDelivery.Application.Contracts.Interfaces;

/// <summary>
/// Определяет операции получения аналитической информации
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Асинхронно получает сводную информацию о заказах по категориям за период
    /// </summary>
    Task<IList<CategoryOrderSummaryDto>>
        GetOrdersByCategoryForPeriodAsync(
            DateTime periodStart,
            DateTime periodEnd);

    /// <summary>
    /// Асинхронно получает клиентов, делавших заказы в указанном ресторане
    /// </summary>
    Task<IList<CustomerOrderSummaryDto>>
        GetCustomersByRestaurantAsync(int restaurantId);

    /// <summary>
    /// Асинхронно получает клиента с максимальной общей суммой заказов
    /// </summary>
    Task<CustomerOrderSummaryDto?>
        GetCustomerWithMaxTotalOrderAmountAsync();

    /// <summary>
    /// Асинхронно получает заказы с минимальным временем доставки
    /// </summary>
    Task<IList<OrderDeliveryTimeDto>>
        GetOrdersWithMinDeliveryTimeAsync();

    /// <summary>
    /// Асинхронно получает топ ресторанов по количеству заказов
    /// </summary>
    Task<IList<RestaurantOrderCountDto>>
        GetTopRestaurantsByOrderCountAsync(int count);
}