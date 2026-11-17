using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Repositories;
using DryCleaner.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EfCore.Repositories;

public class CustomerRepository(DryCleanerDbContext context) : IRepository<Customer>
{
    public async Task<IList<Customer>> GetAllAsync()
    {
        return await context.Customers.AsNoTracking().ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await context.Customers.FindAsync(id);
    }

    public async Task<Customer> AddAsync(Customer entity)
    {
        context.Customers.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Customer?> UpdateAsync(Customer entity)
    {
        var existing = await context.Customers.FindAsync(entity.Id);
        if (existing == null) return null;

        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Customers.FindAsync(id);
        if (entity == null) return false;

        context.Customers.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}