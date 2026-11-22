using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using DryCleaner.Infrastructure.EFCore;
using DryCleaner.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core-реализация репозитория для выполнения аналитических запросов.
/// </summary>
/// <param name="context">Контекст базы данных.</param>
public class AnalyticEfCoreRepository(DryCleanerDbContext context) : IAnalyticRepository
{
    /// <summary>
    /// Возвращает заказы, находящиеся в обработке, упорядоченные по дате приёма.
    /// </summary>
    public async Task<IList<Order>> GetInProgressOrders()
    {
        return await context.Orders
            .AsNoTracking()
            .Include(order => order.Client)
            .Include(order => order.Product)
                .ThenInclude(product => product.Category)
            .Where(order => order.Status == OrderStatus.InProgress)
            .OrderBy(order => order.AcceptedAt)
            .ToListAsync();
    }

    /// <summary>
    /// Возвращает топ-5 клиентов по количеству заказов за период.
    /// </summary>
    public async Task<IList<(Client Client, int OrdersCount)>> GetTop5MostActiveClients(
        DateTime dateFrom,
        DateTime dateTo)
    {
        var result = await context.Orders
            .AsNoTracking()
            .Where(order =>
                order.AcceptedAt >= dateFrom &&
                order.AcceptedAt <= dateTo)
            .GroupBy(order => new
            {
                order.Client.Id,
                order.Client.Name,
                order.Client.Phone
            })
            .Select(group => new
            {
                group.Key.Id,
                group.Key.Name,
                group.Key.Phone,
                OrdersCount = group.Count()
            })
            .OrderByDescending(x => x.OrdersCount)
            .Take(5)
            .ToListAsync();

        return result
            .Select(x => (
                new Client
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone
                },
                x.OrdersCount))
            .ToList();
    }

    /// <summary>
    /// Возвращает клиентов со средним сроком выполнения заказов.
    /// </summary>
    public async Task<IList<(Client Client, double AverageProcessingDays)>> GetClientsAverageProcessingDays()
    {
        var result = await context.Orders
            .AsNoTracking()
            .GroupBy(order => new
            {
                order.Client.Id,
                order.Client.Name,
                order.Client.Phone
            })
            .Select(group => new
            {
                group.Key.Id,
                group.Key.Name,
                group.Key.Phone,
                AverageProcessingDays = group.Average(order => order.ProcessingDays)
            })
            .OrderBy(x => x.Name)
            .ToListAsync();

        return result
            .Select(x => (
                new Client
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone
                },
                x.AverageProcessingDays))
            .ToList();
    }

    /// <summary>
    /// Возвращает топ-5 самых популярных категорий изделий за период.
    /// </summary>
    public async Task<IList<(ProductCategory Category, int OrdersCount)>> GetTop5MostPopularCategories(
        DateTime dateFrom,
        DateTime dateTo)
    {
        return await GetCategoriesPopularity(dateFrom, dateTo, true);
    }

    /// <summary>
    /// Возвращает топ-5 самых непопулярных категорий изделий за период.
    /// </summary>
    public async Task<IList<(ProductCategory Category, int OrdersCount)>> GetTop5LeastPopularCategories(
        DateTime dateFrom,
        DateTime dateTo)
    {
        return await GetCategoriesPopularity(dateFrom, dateTo, false);
    }

    /// <summary>
    /// Возвращает клиентов с наибольшей суммой расходов за всё время.
    /// </summary>
    public async Task<IList<(Client Client, decimal TotalSpent)>> GetClientWhoSpentTheMostMoney()
    {
        var groupedQuery = context.Orders
            .AsNoTracking()
            .GroupBy(order => new
            {
                order.Client.Id,
                order.Client.Name,
                order.Client.Phone
            })
            .Select(group => new
            {
                group.Key.Id,
                group.Key.Name,
                group.Key.Phone,
                TotalSpent = group.Sum(order => order.Product.Category.CleaningPrice)
            });

        var maxSpent = await groupedQuery
            .Select(x => (decimal?)x.TotalSpent)
            .MaxAsync();

        if (maxSpent is null)
            return [];

        var result = await groupedQuery
            .Where(x => x.TotalSpent == maxSpent.Value)
            .ToListAsync();

        return result
            .Select(x => (
                new Client
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone
                },
                x.TotalSpent))
            .ToList();
    }

    /// <summary>
    /// Возвращает категории изделий с количеством заказов за указанный период.
    /// </summary>
    /// <param name="dateFrom">Начало периода.</param>
    /// <param name="dateTo">Конец периода.</param>
    /// <param name="mostPopular">
    /// Определяет направление сортировки: true — по убыванию,
    /// false — по возрастанию.
    /// </param>
    private async Task<IList<(ProductCategory Category, int OrdersCount)>> GetCategoriesPopularity(
        DateTime dateFrom,
        DateTime dateTo,
        bool mostPopular)
    {
        var query = context.ProductCategories
            .AsNoTracking()
            .Select(category => new
            {
                Category = category,
                OrdersCount = context.Orders.Count(order =>
                    order.Product.Category.Id == category.Id &&
                    order.AcceptedAt >= dateFrom &&
                    order.AcceptedAt <= dateTo)
            });

        var result = mostPopular
            ? await query
                .OrderByDescending(x => x.OrdersCount)
                .Take(5)
                .ToListAsync()
            : await query
                .OrderBy(x => x.OrdersCount)
                .Take(5)
                .ToListAsync();

        return result
            .Select(x => (x.Category, x.OrdersCount))
            .ToList();
    }
}