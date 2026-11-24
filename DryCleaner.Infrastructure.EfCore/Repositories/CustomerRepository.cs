using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Repositories;
using DryCleaner.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EfCore.Repositories;

/// <summary>
/// Реализация репозитория клиента с использованием EfCore
/// </summary>
public class CustomerRepository(DryCleanerDbContext context) : IRepository<Customer>
{
    /// <inheritdoc />
    public async Task<IList<Customer>> GetAllAsync()
    {
        return await context.Customers.AsNoTracking().ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await context.Customers.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<Customer> AddAsync(Customer entity)
    {
        context.Customers.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<Customer?> UpdateAsync(Customer entity)
    {
        var existing = await context.Customers.FindAsync(entity.Id);
        if (existing == null) return null;

        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Customers.FindAsync(id);
        if (entity == null) return false;

        try
        {
            context.Customers.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("Невозможно удалить клиента, так как у него есть заказы");
        }
    }
}