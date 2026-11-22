using DryCleaner.Domain.Entities;

namespace DryCleaner.Domain.Interfaces;

/// <summary>
/// Определяет методы для выполнения аналитических запросов.
/// </summary>
public interface IAnalyticRepository
{
    /// <summary>
    /// Возвращает заказы, находящиеся в обработке, упорядоченные по дате приёма.
    /// </summary>
    public Task<IList<Order>> GetInProgressOrders();

    /// <summary>
    /// Возвращает клиентов с количеством заказов за указанный период.
    /// </summary>
    public Task<IList<(Client Client, int OrdersCount)>> GetTop5MostActiveClients(
        DateTime dateFrom,
        DateTime dateTo);

    /// <summary>
    /// Возвращает клиентов со средним сроком выполнения заказов.
    /// </summary>
    public Task<IList<(Client Client, double AverageProcessingDays)>> GetClientsAverageProcessingDays();

    /// <summary>
    /// Возвращает самые популярные категории изделий за указанный период.
    /// </summary>
    public Task<IList<(ProductCategory Category, int OrdersCount)>> GetTop5MostPopularCategories(
        DateTime dateFrom,
        DateTime dateTo);

    /// <summary>
    /// Возвращает самые непопулярные категории изделий за указанный период.
    /// </summary>
    public Task<IList<(ProductCategory Category, int OrdersCount)>> GetTop5LeastPopularCategories(
        DateTime dateFrom,
        DateTime dateTo);

    /// <summary>
    /// Возвращает клиентов с наибольшей суммой расходов.
    /// </summary>
    public Task<IList<(Client Client, decimal TotalSpent)>> GetClientWhoSpentTheMostMoney();
}