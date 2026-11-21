using FoodDelivery.Domain.Analytics;
using FoodDelivery.Domain.Models;

namespace FoodDelivery.Domain.Repositories;

/// <summary>
/// Определяет аналитические запросы службы доставки
/// </summary>
public interface IAnalyticsRepository
{
    /// <summary>
    /// Возвращает пять ресторанов с наибольшим количеством заказов
    /// </summary>
    /// <returns>Рестораны и количество заказов</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<RestaurantOrderStatistics>> GetTopRestaurantsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает все заказы с минимальным временем доставки
    /// </summary>
    /// <returns>Заказы с минимальным временем доставки</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<Order>> GetOrdersWithMinimumDeliveryTimeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает клиентов ресторана, упорядоченных по ФИО
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Клиенты без повторений</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<Customer>> GetRestaurantCustomersAsync(int restaurantId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает статистику заказов по категориям за включительный период
    /// </summary>
    /// <param name="start">Начало периода</param>
    /// <param name="end">Конец периода</param>
    /// <returns>Статистика всех категорий</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<CategoryOrderStatistics>> GetCategoryStatisticsAsync(
        DateTime start,
        DateTime end,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает клиента с наибольшей суммой заказов
    /// </summary>
    /// <returns>Клиент и сумма или null, если заказов нет</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<CustomerSpending?> GetTopSpendingCustomerAsync(CancellationToken cancellationToken = default);
}
