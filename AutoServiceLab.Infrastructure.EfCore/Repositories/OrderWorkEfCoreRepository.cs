using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoServiceLab.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий связей заказов и работ (EF Core)
/// </summary>
public class OrderWorkEfCoreRepository(AutoServiceDbContext context) : IOrderWorkRepository
{
    /// <inheritdoc />
    public async Task<OrderWork?> GetAsync(int id) =>
        await context.OrderWorks
        .Include(ow => ow.WorkType)
        .FirstOrDefaultAsync(ow => ow.Id == id);

    /// <inheritdoc />
    public async Task<IReadOnlyList<OrderWork>> GetAllAsync() =>
        await context.OrderWorks
        .Include(ow => ow.WorkType)
        .ToListAsync();

    /// <inheritdoc />
    public async Task AddAsync(OrderWork entity)
    {
        await context.OrderWorks.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(OrderWork entity)
    {
        context.OrderWorks.Update(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        if (entity == null) return false;

        context.OrderWorks.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}
