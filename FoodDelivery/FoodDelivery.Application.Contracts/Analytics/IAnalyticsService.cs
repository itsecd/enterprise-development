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
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<RestaurantOrderStatisticsDto>> GetTopRestaurantsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает все заказы с минимальным временем доставки
    /// </summary>
    /// <returns>Заказы с минимальным временем доставки</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<OrderDto>> GetOrdersWithMinimumDeliveryTimeAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает клиентов ресторана, упорядоченных по ФИО
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Клиенты без повторений</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<CustomerDto>> GetRestaurantCustomersAsync(int restaurantId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает статистику всех категорий за включительный период
    /// </summary>
    /// <param name="start">Начало периода</param>
    /// <param name="end">Конец периода</param>
    /// <returns>Статистика заказов по категориям</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<IReadOnlyList<CategoryOrderStatisticsDto>> GetCategoryStatisticsAsync(
        DateTime start,
        DateTime end,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Возвращает клиента с наибольшей суммой заказов
    /// </summary>
    /// <returns>Клиент и сумма его заказов</returns>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public Task<CustomerSpendingDto> GetTopSpendingCustomerAsync(CancellationToken cancellationToken = default);
}
