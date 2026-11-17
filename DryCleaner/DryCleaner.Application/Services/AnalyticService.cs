using DryCleaner.Application.Contracts.Analytic;
using DryCleaner.Application.Contracts.Order;
using DryCleaner.Application.Contracts.ServiceInterfaces;
using DryCleaner.Application.Mappers;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using DryCleaner.Shared.Enums;

namespace DryCleaner.Application.Services;

/// <summary>
/// Предоставляет сервис для аналитических запросов к данным сервиса.
/// </summary>
/// <param name="orderRepository">Репозиторий для работы с сущностями заказов.</param>
/// <param name="categoryRepository">Репозиторий для работы с сущностями категорий изделий.</param>
public class AnalyticService(
    IRepository<Order, int> orderRepository,
    IRepository<ProductCategory, int> categoryRepository) : IAnalyticService
{
    /// <summary>
    /// Возвращает заказы, находящиеся в обработке, упорядоченные по дате приема.
    /// </summary>
    /// <returns>Список заказов в виде OrderDto.</returns>
    public async Task<IList<OrderDto>> GetInProgressOrders()
    {
        var orders = await orderRepository.ReadAll();

        var result = orders
            .Where(order => order.Status == OrderStatus.InProgress)
            .OrderBy(order => order.AcceptedAt)
            .ToList();

        return OrderMapper.Map(result);
    }

    /// <summary>
    /// Возвращает топ 5 самых активных клиентов за период.
    /// </summary>
    /// <param name="dateFrom">Начало периода.</param>
    /// <param name="dateTo">Конец периода.</param>
    /// <returns>Список клиентов с количеством заказов.</returns>
    public async Task<IList<ClientOrdersCountDto>> GetTop5MostActiveClients(DateTime dateFrom, DateTime dateTo)
    {
        var orders = await orderRepository.ReadAll();

        return orders
            .Where(order =>
                order.AcceptedAt >= dateFrom &&
                order.AcceptedAt <= dateTo)
            .GroupBy(order => order.Client.Id)
            .Select(group => new
            {
                Client = group.First().Client,
                OrdersCount = group.Count()
            })
            .OrderByDescending(x => x.OrdersCount)
            .Take(5)
            .Select(x => AnalyticMapper.Map(x.Client, x.OrdersCount))
            .ToList();
    }

    /// <summary>
    /// Возвращает клиентов со средним сроком выполнения их заказов, упорядоченных по имени.
    /// </summary>
    /// <returns>Список клиентов со средним сроком выполнения заказов.</returns>
    public async Task<IList<ClientAverageProcessingDaysDto>> GetClientsAverageProcessingDays()
    {
        var orders = await orderRepository.ReadAll();

        return orders
            .GroupBy(order => order.Client.Id)
            .Select(group => new
            {
                Client = group.First().Client,
                AverageProcessingDays = group.Average(order => order.ProcessingDays)
            })
            .OrderBy(x => x.Client.Name)
            .Select(x => AnalyticMapper.Map(x.Client, x.AverageProcessingDays))
            .ToList();
    }

    /// <summary>
    /// Возвращает 5 самых популярных категорий изделий за указанный период.
    /// </summary>
    /// <param name="dateFrom">Начало периода.</param>
    /// <param name="dateTo">Конец периода.</param>
    /// <returns>Список самых популярных категорий</returns>
    public async Task<CategoriesPopularityDto> GetTop5MostPopularCategories(DateTime dateFrom, DateTime dateTo)
    {
        var orders = await orderRepository.ReadAll();
        var categories = await categoryRepository.ReadAll();

        var result = categories
            .GroupJoin(
                orders.Where(order =>
                    order.AcceptedAt >= dateFrom &&
                    order.AcceptedAt <= dateTo),
                category => category.Id,
                order => order.Product.Category.Id,
                (category, categoryOrders) => new
                {
                    Category = category,
                    OrdersCount = categoryOrders.Count()
                })
            .OrderByDescending(x => x.OrdersCount)
            .Take(5)
            .Select(x => AnalyticMapper.Map(x.Category, x.OrdersCount))
            .ToList();

        return new CategoriesPopularityDto(result);
    }


    /// <summary>
    /// Возвращает 5 самых не популярных категорий изделий за указанный период.
    /// </summary>
    /// <param name="dateFrom">Начало периода.</param>
    /// <param name="dateTo">Конец периода.</param>
    /// <returns>Список самых не популярных категорий</returns>
    public async Task<CategoriesPopularityDto> GetTop5LeastPopularCategories(DateTime dateFrom, DateTime dateTo)
    {
        var orders = await orderRepository.ReadAll();
        var categories = await categoryRepository.ReadAll();

        var result = categories
            .GroupJoin(
                orders.Where(order =>
                    order.AcceptedAt >= dateFrom &&
                    order.AcceptedAt <= dateTo),
                category => category.Id,
                order => order.Product.Category.Id,
                (category, categoryOrders) => new
                {
                    Category = category,
                    OrdersCount = categoryOrders.Count()
                })
            .OrderBy(x => x.OrdersCount)
            .Take(5)
            .Select(x => AnalyticMapper.Map(x.Category, x.OrdersCount))
            .ToList();

        return new CategoriesPopularityDto(result);
    }


    /// <summary>
    /// Возвращает клиента, потратившего самую большую сумму за весь период работы сервиса.
    /// </summary>
    /// <returns>Клиент с потраченной суммой или null, если заказов нет.</returns>
    public async Task<IList<ClientTotalSpentDto>> GetClientWhoSpentTheMostMoney()
    {
        var orders = await orderRepository.ReadAll();

        var grouped = orders
            .GroupBy(order => order.Client.Id)
            .Select(group => new
            {
                Client = group.First().Client,
                TotalSpent = group.Sum(order =>
                    order.Product.Category.CleaningPrice)
            })
            .ToList();

        if (grouped.Count == 0)
            return [];

        var maxSpent = grouped.Max(x => x.TotalSpent);

        var result = grouped
            .Where(x => x.TotalSpent == maxSpent)
            .Select(x => AnalyticMapper.Map(x.Client, x.TotalSpent))
            .ToList();

        return result;
    }
}