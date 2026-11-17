using DryCleaner.Application.Contracts.DTOs.Analytics;

namespace DryCleaner.Application.Contracts.Interfaces;

/// <summary>
/// Определяет операции получения аналитической информации
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получает заказы в обработке, упорядоченные по дате приема
    /// </summary>
    Task<IList<OrderInProgressDto>> GetOrdersInProgressAsync();

    /// <summary>
    /// Получает топ-5 клиентов по числу сданных изделий за выбранный период
    /// </summary>
    Task<IList<CustomerOrderCountDto>> GetTop5CustomersByItemsAsync(DateOnly periodStart, DateOnly periodEnd);

    /// <summary>
    /// Получает клиентов, чьи заказы обрабатывались дольше всего
    /// </summary>
    Task<IList<OrderProcessingTimeDto>> GetCustomersWithLongestProcessingAsync();

    /// <summary>
    /// Получает топ-5 популярных категорий изделий
    /// </summary>
    Task<IList<CategoryPopularityDto>> GetTop5PopularCategoriesAsync(DateOnly start, DateOnly end);

    /// <summary>
    /// Получает топ-5 непопулярных категорий изделий
    /// </summary>
    Task<IList<CategoryPopularityDto>> GetTop5UnpopularCategoriesAsync(DateOnly start, DateOnly end);

    /// <summary>
    /// Получает клиента с наибольшей суммой заказов
    /// </summary>
    Task<TotalSpentCustomerDto?> GetCustomerWithMaxTotalSpentAsync();
}