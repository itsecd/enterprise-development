using FoodDelivery.Domain.Analytics;
using FoodDelivery.Domain.Models;

namespace FoodDelivery.Domain.Repositories;

/// <summary>
/// Определяет аналитические запросы службы доставки
/// </summary>
public interface IAnalyticsRepository : IDataSession
{
    /// <summary>
    /// Возвращает пять ресторанов с наибольшим количеством заказов
    /// </summary>
    /// <returns>Рестораны и количество заказов</returns>
    IReadOnlyList<RestaurantOrderStatistics> GetTopRestaurants();

    /// <summary>
    /// Возвращает все заказы с минимальным временем доставки
    /// </summary>
    /// <returns>Заказы с минимальным временем доставки</returns>
    IReadOnlyList<Order> GetOrdersWithMinimumDeliveryTime();

    /// <summary>
    /// Возвращает клиентов ресторана, упорядоченных по ФИО
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Клиенты без повторений</returns>
    IReadOnlyList<Customer> GetRestaurantCustomers(int restaurantId);

    /// <summary>
    /// Возвращает статистику заказов по категориям за включительный период
    /// </summary>
    /// <param name="start">Начало периода</param>
    /// <param name="end">Конец периода</param>
    /// <returns>Статистика всех категорий</returns>
    IReadOnlyList<CategoryOrderStatistics> GetCategoryStatistics(
        DateTime start,
        DateTime end);

    /// <summary>
    /// Возвращает клиента с наибольшей суммой заказов
    /// </summary>
    /// <returns>Клиент и сумма или null, если заказов нет</returns>
    CustomerSpending? GetTopSpendingCustomer();
}
