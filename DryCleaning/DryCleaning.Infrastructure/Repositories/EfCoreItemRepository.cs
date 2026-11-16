using DryCleaning.Domain.Models;
using DryCleaning.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DryCleaning.Infrastructure.Repositories;

public class EfCoreItemRepository(DryCleaningDbContext context) : IItemRepository
{
    public IEnumerable<Item> GetAll() => context.Items.Include(i => i.Category).ToList();
    public Item? GetById(int id) => context.Items.Include(i => i.Category).FirstOrDefault(i => i.Id == id);

    public void Add(Item entity) 
    { 
        context.Items.Add(entity); 
        context.SaveChanges(); 
    }
    public void Update(Item entity) 
    { 
        context.Items.Update(entity); 
        context.SaveChanges(); 
    }
    public void Delete(int id) 
    { 
        var e = GetById(id); 
        if (e != null) 
        { 
            context.Items.Remove(e); 
            context.SaveChanges(); 
        } 
    }
}
