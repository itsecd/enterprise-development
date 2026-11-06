using FoodDelivery.Application.Contracts.Customers;
using FoodDelivery.Application.Contracts.Orders;

namespace FoodDelivery.Application.Contracts.Analytics;

/// <summary>
/// Определяет прикладные аналитические запросы
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Возвращает пять ресторанов с наибольшим количеством заказов
    /// </summary>
    /// <returns>Рестораны и количество заказов</returns>
    IReadOnlyList<RestaurantOrderStatisticsDto> GetTopRestaurants();

    /// <summary>
    /// Возвращает все заказы с минимальным временем доставки
    /// </summary>
    /// <returns>Заказы с минимальным временем доставки</returns>
    IReadOnlyList<OrderDto> GetOrdersWithMinimumDeliveryTime();

    /// <summary>
    /// Возвращает клиентов ресторана, упорядоченных по ФИО
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Клиенты без повторений</returns>
    IReadOnlyList<CustomerDto> GetRestaurantCustomers(int restaurantId);

    /// <summary>
    /// Возвращает статистику всех категорий за включительный период
    /// </summary>
    /// <param name="start">Начало периода</param>
    /// <param name="end">Конец периода</param>
    /// <returns>Статистика заказов по категориям</returns>
    IReadOnlyList<CategoryOrderStatisticsDto> GetCategoryStatistics(
        DateTime start,
        DateTime end);

    /// <summary>
    /// Возвращает клиента с наибольшей суммой заказов
    /// </summary>
    /// <returns>Клиент и сумма или null, если заказов нет</returns>
    CustomerSpendingDto? GetTopSpendingCustomer();
}
