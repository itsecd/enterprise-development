using FoodDelivery.Application.Contracts.DTOs.Analytics;

namespace FoodDelivery.Application.Contracts.Interfaces;

/// <summary>
/// Определяет операции получения аналитической информации
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получает сводную информацию о заказах по категориям за период
    /// </summary>
    IList<CategoryOrderSummaryDto> GetOrdersByCategoryForPeriod(DateTime periodStart, DateTime periodEnd);

    /// <summary>
    /// Получает клиентов, делавших заказы в указанном ресторане
    /// </summary>
    IList<CustomerOrderSummaryDto> GetCustomersByRestaurant(int restaurantId);

    /// <summary>
    /// Получает клиента с максимальной общей суммой заказов
    /// </summary>
    CustomerOrderSummaryDto? GetCustomerWithMaxTotalOrderAmount();

    /// <summary>
    /// Получает заказы с минимальным временем доставки
    /// </summary>
    IList<OrderDeliveryTimeDto> GetOrdersWithMinDeliveryTime();

    /// <summary>
    /// Получает топ ресторанов по количеству заказов
    /// </summary>
    IList<RestaurantOrderCountDto> GetTopRestaurantsByOrderCount(int count);
}