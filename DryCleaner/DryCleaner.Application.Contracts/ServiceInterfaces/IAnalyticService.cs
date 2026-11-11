using DryCleaner.Application.Contracts.Analytic;
using DryCleaner.Application.Contracts.Order;

namespace DryCleaner.Application.Contracts.ServiceInterfaces;

/// <summary>
/// Определяет контракт для аналитических запросов к данным сервиса
/// </summary>
public interface IAnalyticService
{
    /// <summary>
    /// Возвращает заказы, находящиеся в обработке, упорядоченные по дате приема.
    /// </summary>
    /// <returns>Список заказов в виде OrderDto.</returns>
    public Task<IList<OrderDto>> GetInProgressOrders();

    /// <summary>
    /// Возвращает топ 5 самых активных клиентов за период.
    /// </summary>
    /// <param name="dateFrom">Начало периода.</param>
    /// <param name="dateTo">Конец периода.</param>
    /// <returns>Список клиентов с количеством заказов.</returns>
    public Task<IList<ClientOrdersCountDto>> GetTop5MostActiveClients(DateTime dateFrom, DateTime dateTo);

    /// <summary>
    /// Возвращает клиентов со средним сроком выполнения их заказов, упорядоченных по имени.
    /// </summary>
    /// <returns>Список клиентов со средним сроком выполнения заказов.</returns>
    public Task<IList<ClientAverageProcessingDaysDto>> GetClientsAverageProcessingDays();


    /// <summary>
    /// Возвращает 5 самых популярных категорий изделий за указанный период.
    /// </summary>
    /// <param name="dateFrom">Начало периода.</param>
    /// <param name="dateTo">Конец периода.</param>
    /// <returns>Список самых популярных категорий</returns>
    public Task<CategoriesPopularityDto> GetTop5MostPopularCategories(DateTime dateFrom, DateTime dateTo);


    /// <summary>
    /// Возвращает 5 самых не популярных категорий изделий за указанный период.
    /// </summary>
    /// <param name="dateFrom">Начало периода.</param>
    /// <param name="dateTo">Конец периода.</param>
    /// <returns>Список самых не популярных категорий</returns>
    public Task<CategoriesPopularityDto> GetTop5LeastPopularCategories(DateTime dateFrom, DateTime dateTo);


    /// <summary>
    /// Возвращает клиента, потратившего самую большую сумму за весь период работы сервиса.
    /// </summary>
    /// <returns>Клиент с потраченной суммой или null, если заказов нет.</returns>
    public Task<IList<ClientTotalSpentDto>> GetClientWhoSpentTheMostMoney();
}
