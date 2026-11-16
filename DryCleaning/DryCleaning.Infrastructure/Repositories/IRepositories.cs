using DryCleaning.Domain.Models;

namespace DryCleaning.Infrastructure.Repositories;

public interface IRepository<T> where T : class
{
    public IEnumerable<T> GetAll();
    public T? GetById(int id);
    public void Add(T entity);
    public void Update(T entity);
    public void Delete(int id);
}

public interface ICategoryRepository : IRepository<Category> { }
public interface IClientRepository : IRepository<Client> { }
public interface IItemRepository : IRepository<Item> { }
public interface IOrderRepository : IRepository<Order> { }
