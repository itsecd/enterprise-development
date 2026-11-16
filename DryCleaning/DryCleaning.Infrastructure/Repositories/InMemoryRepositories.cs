using DryCleaning.Domain.Data;
using DryCleaning.Domain.Models;

namespace DryCleaning.Infrastructure.Repositories;

public class InMemoryCategoryRepository : ICategoryRepository
{
    private readonly List<Category> _categories = DataSeed.GetCategories();

    public IEnumerable<Category> GetAll() => _categories;
    public Category? GetById(int id) => _categories.FirstOrDefault(c => c.Id == id);

    public void Add(Category entity)
    {
        entity.Id = _categories.Any() ? _categories.Max(c => c.Id) + 1 : 1;
        _categories.Add(entity);
    }

    public void Update(Category entity)
    {
        var index = _categories.FindIndex(c => c.Id == entity.Id);
        if (index != -1) _categories[index] = entity;
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null) _categories.Remove(entity);
    }
}

public class InMemoryClientRepository : IClientRepository
{
    private readonly List<Client> _clients = DataSeed.GetClients();

    public IEnumerable<Client> GetAll() => _clients;
    public Client? GetById(int id) => _clients.FirstOrDefault(c => c.Id == id);

    public void Add(Client entity)
    {
        entity.Id = _clients.Any() ? _clients.Max(c => c.Id) + 1 : 1;
        _clients.Add(entity);
    }

    public void Update(Client entity)
    {
        var index = _clients.FindIndex(c => c.Id == entity.Id);
        if (index != -1) _clients[index] = entity;
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null) _clients.Remove(entity);
    }
}

public class InMemoryItemRepository : IItemRepository
{
    private readonly List<Item> _items = DataSeed.GetItems();

    public IEnumerable<Item> GetAll() => _items;
    public Item? GetById(int id) => _items.FirstOrDefault(i => i.Id == id);

    public void Add(Item entity)
    {
        entity.Id = _items.Any() ? _items.Max(i => i.Id) + 1 : 1;
        _items.Add(entity);
    }

    public void Update(Item entity)
    {
        var index = _items.FindIndex(i => i.Id == entity.Id);
        if (index != -1) _items[index] = entity;
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null) _items.Remove(entity);
    }
}

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = DataSeed.GetOrders();

    public IEnumerable<Order> GetAll() => _orders;
    public Order? GetById(int id) => _orders.FirstOrDefault(o => o.Id == id);

    public void Add(Order entity)
    {
        entity.Id = _orders.Any() ? _orders.Max(o => o.Id) + 1 : 1;
        _orders.Add(entity);
    }

    public void Update(Order entity)
    {
        var index = _orders.FindIndex(o => o.Id == entity.Id);
        if (index != -1) _orders[index] = entity;
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null) _orders.Remove(entity);
    }
}