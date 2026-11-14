using DryCleaning.Domain;
using DryCleaning.Domain.Interfaces;
using DryCleaning.Domain.Seeds;

namespace DryCleaning.Infrastructure.InMemory.Repositories;

public class OrderRepository(TestData data) : IOrderRepository
{
    private readonly List<Order> _orders = [.. data.Orders];
    private int _nextId = 13;

    public Task<IReadOnlyList<Order>> GetAllAsync() => Task.FromResult<IReadOnlyList<Order>>(_orders);
    public Task<IReadOnlyList<Order>> GetByClientIdAsync(int clientId) =>
        Task.FromResult<IReadOnlyList<Order>>(_orders.Where(o => o.Client.ClientId == clientId).ToList());
    public Task<Order?> GetByIdAsync(int id) => Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));

    public Task<Order> AddAsync(Order order)
    {
        order.Id = _nextId++;
        _orders.Add(order);
        return Task.FromResult(order);
    }

    public Task<bool> UpdateAsync(Order order)
    {
        var index = _orders.FindIndex(o => o.Id == order.Id);
        if (index == -1) return Task.FromResult(false);
        _orders[index] = order;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        var o = _orders.FirstOrDefault(x => x.Id == id);
        if (o is null) return Task.FromResult(false);
        _orders.Remove(o);
        return Task.FromResult(true);
    }

    public Task<bool> HasOrdersForClientAsync(int clientId) =>
        Task.FromResult(_orders.Any(o => o.Client.ClientId == clientId));
    public Task<bool> HasOrdersForItemAsync(int itemId) =>
        Task.FromResult(_orders.Any(o => o.Item.ItemId == itemId));
}
