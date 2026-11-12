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
    IList<OrderInProgressDto> GetOrdersInProgress();

    /// <summary>
    /// Получает топ-5 клиентов по числу сданных изделий за выбранный период
    /// </summary>
    IList<CustomerOrderCountDto> GetTop5CustomersByItems(DateOnly periodStart, DateOnly periodEnd);

    /// <summary>
    /// Получает клиентов, чьи заказы обрабатывались дольше всего
    /// </summary>
    IList<OrderProcessingTimeDto> GetCustomersWithLongestProcessing();

    /// <summary>
    /// Получает топ-5 популярных категорий изделий
    /// </summary>
    IList<CategoryPopularityDto> GetTop5PopularCategories(DateOnly start, DateOnly end);

    /// <summary>
    /// Получает топ-5 непопулярных категорий изделий
    /// </summary>
    IList<CategoryPopularityDto> GetTop5UnpopularCategories(DateOnly start, DateOnly end);

    /// <summary>
    /// Получает клиента с наибольшей суммой заказов
    /// </summary>
    TotalSpentCustomerDto? GetCustomerWithMaxTotalSpent();
}