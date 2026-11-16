using DryCleaning.Domain.Models;
using DryCleaning.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DryCleaning.Infrastructure.Repositories;

public class EfCoreOrderRepository(DryCleaningDbContext context) : IOrderRepository
{
    public IEnumerable<Order> GetAll() => context.Orders
        .Include(o => o.Client)
        .Include(o => o.Item)
        .ThenInclude(i => i.Category)
        .ToList();

    public Order? GetById(int id) => context.Orders
        .Include(o => o.Client)
        .Include(o => o.Item)
        .ThenInclude(i => i.Category)
        .FirstOrDefault(o => o.Id == id);

    public void Add(Order entity) 
    { 
        context.Orders.Add(entity); 
        context.SaveChanges(); 
    }
    public void Update(Order entity) 
    { 
        context.Orders.Update(entity); 
        context.SaveChanges(); 
    }
    public void Delete(int id) 
    { 
        var e = GetById(id); 
        if (e != null) 
        { 
            context.Orders.Remove(e); 
            context.SaveChanges(); 
        } 
    }
}
