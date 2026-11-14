using DryCleaning.Domain;
using DryCleaning.Domain.Enum;

namespace DryCleaning.Tests.Services;

/// <summary>Аналитические запросы по заказам.</summary>
public static class OrderQueries
{
    public static List<Order> GetInProgressOrders(IEnumerable<Order> orders) =>
        [.. orders
            .Where(o => o.Status == OrderStatus.InProgress)
            .OrderBy(o => o.AcceptDate)
            .ThenBy(o => o.Id)];

    public static List<Client> GetTop5Clients(
        IEnumerable<Order> orders, DateTime from, DateTime to) =>
        [.. orders
            .Where(o => o.AcceptDate >= from && o.AcceptDate < to)
            .Where(o => o.Status != OrderStatus.Cancelled)
            .GroupBy(o => o.Client.ClientId)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.First().Client.FullName)
            .ThenBy(g => g.Key)
            .Take(5)
            .Select(g => g.First().Client)];

    public static List<Client> GetClientsWithLongestProcessing(IEnumerable<Order> orders) =>
        [.. orders
            .Where(o => o.ProcessingDays == orders.Max(x => x.ProcessingDays))
            .Select(o => o.Client)
            .DistinctBy(c => c.ClientId)
            .OrderBy(c => c.FullName)
            .ThenBy(c => c.ClientId)];

    public static List<ItemCategory> GetCategoryPopularity(
        IEnumerable<Order> orders,
        IEnumerable<ItemCategory> categories,
        DateTime from, DateTime to,
        bool descending)
    {
        List<Order> relevant = [.. orders
            .Where(o => o.AcceptDate >= from && o.AcceptDate < to)
            .Where(o => o.Status != OrderStatus.Cancelled)];

        var ordered = descending
            ? categories.OrderByDescending(c => relevant.Count(o => o.Item.Category.CategoryId == c.CategoryId))
            : categories.OrderBy(c => relevant.Count(o => o.Item.Category.CategoryId == c.CategoryId));

        return [.. ordered
            .ThenBy(c => c.CategoryName)
            .ThenBy(c => c.CategoryId)
            .Take(5)];
    }

    public static Client? GetTopSpendingClient(IEnumerable<Order> orders) =>
        orders
            .Where(o => o.Status == OrderStatus.Issued)
            .GroupBy(o => o.Client.ClientId)
            .Select(g => new
            {
                Owner = g.First().Client,
                Total = g.Sum(o => o.Item.Category.Price)
            })
            .OrderByDescending(x => x.Total)
            .ThenBy(x => x.Owner.FullName)
            .ThenBy(x => x.Owner.ClientId)
            .FirstOrDefault()
            ?.Owner;
}
