using DryCleaner.Application.Contracts.Analytic;
using DryCleaner.Application.Contracts.Order;
using DryCleaner.Application.Contracts.ServiceInterfaces;
using DryCleaner.Application.Mappers;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

/// <summary>
/// Предоставляет сервис для аналитических запросов к данным сервиса.
/// </summary>
/// <param name="analyticRepository">Репозиторий для выполнения аналитических запросов.</param>
public class AnalyticService(
    IAnalyticRepository analyticRepository) : IAnalyticService
{
    /// <summary>
    /// Возвращает заказы, находящиеся в обработке, упорядоченные по дате приёма.
    /// </summary>
    /// <returns>Список заказов в виде OrderDto.</returns>
    public async Task<IList<OrderDto>> GetInProgressOrders()
    {
        var orders = await analyticRepository.GetInProgressOrders();

        return OrderMapper.Map(orders);
    }

    /// <summary>
    /// Возвращает топ-5 самых активных клиентов за период.
    /// </summary>
    public async Task<IList<ClientOrdersCountDto>> GetTop5MostActiveClients(
        DateTime dateFrom,
        DateTime dateTo)
    {
        var clients = await analyticRepository.GetTop5MostActiveClients(
            dateFrom,
            dateTo);

        return clients
            .Select(x => AnalyticMapper.Map(x.Client, x.OrdersCount))
            .ToList();
    }

    /// <summary>
    /// Возвращает клиентов со средним сроком выполнения их заказов,
    /// упорядоченных по имени.
    /// </summary>
    public async Task<IList<ClientAverageProcessingDaysDto>> GetClientsAverageProcessingDays()
    {
        var clients = await analyticRepository.GetClientsAverageProcessingDays();

        return clients
            .Select(x => AnalyticMapper.Map(x.Client, x.AverageProcessingDays))
            .ToList();
    }

    /// <summary>
    /// Возвращает 5 самых популярных категорий изделий за указанный период.
    /// </summary>
    public async Task<CategoriesPopularityDto> GetTop5MostPopularCategories(
        DateTime dateFrom,
        DateTime dateTo)
    {
        var categories = await analyticRepository.GetTop5MostPopularCategories(
            dateFrom,
            dateTo);

        var result = categories
            .Select(x => AnalyticMapper.Map(x.Category, x.OrdersCount))
            .ToList();

        return new CategoriesPopularityDto(result);
    }

    /// <summary>
    /// Возвращает 5 самых непопулярных категорий изделий за указанный период.
    /// </summary>
    public async Task<CategoriesPopularityDto> GetTop5LeastPopularCategories(
        DateTime dateFrom,
        DateTime dateTo)
    {
        var categories = await analyticRepository.GetTop5LeastPopularCategories(
            dateFrom,
            dateTo);

        var result = categories
            .Select(x => AnalyticMapper.Map(x.Category, x.OrdersCount))
            .ToList();

        return new CategoriesPopularityDto(result);
    }

    /// <summary>
    /// Возвращает клиента, потратившего самую большую сумму за всё время.
    /// </summary>
    public async Task<IList<ClientTotalSpentDto>> GetClientWhoSpentTheMostMoney()
    {
        var clients = await analyticRepository.GetClientWhoSpentTheMostMoney();

        return clients
            .Select(x => AnalyticMapper.Map(x.Client, x.TotalSpent))
            .ToList();
    }
}