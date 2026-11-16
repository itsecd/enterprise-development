using DryCleaning.Domain.Models;
using DryCleaning.Infrastructure.Data;

namespace DryCleaning.Infrastructure.Repositories;

public class EfCoreCategoryRepository(DryCleaningDbContext context) : ICategoryRepository
{
    public IEnumerable<Category> GetAll() => context.Categories.ToList();
    public Category? GetById(int id) => context.Categories.Find(id);

    public void Add(Category entity)
    {
        context.Categories.Add(entity);
        context.SaveChanges();
    }

    public void Update(Category entity)
    {
        context.Categories.Update(entity);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            context.Categories.Remove(entity);
            context.SaveChanges();
        }
    }
}
