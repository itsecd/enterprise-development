using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoServiceLab.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий заказов (EF Core)
/// </summary>
public class ServiceOrderEfCoreRepository(AutoServiceDbContext context) : IServiceOrderRepository
{
    /// <inheritdoc />
    public async Task<ServiceOrder?> GetAsync(int id) =>
        await context.ServiceOrders
            .Include(o => o.Client)
            .Include(o => o.Car)
            .Include(o => o.OrderWorks)
                .ThenInclude(ow => ow.WorkType)
            .Include(o => o.OrderMechanics)
                .ThenInclude(om => om.Mechanic)
            .FirstOrDefaultAsync(o => o.Id == id);

    /// <inheritdoc />
    public async Task<IReadOnlyList<ServiceOrder>> GetAllAsync() =>
        await context.ServiceOrders
            .Include(o => o.Client)
            .Include(o => o.Car)
            .Include(o => o.OrderWorks)
                .ThenInclude(ow => ow.WorkType)
            .Include(o => o.OrderMechanics)
                .ThenInclude(om => om.Mechanic)
            .ToListAsync();

    /// <inheritdoc />
    public async Task AddAsync(ServiceOrder entity)
    {
        await context.ServiceOrders.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(ServiceOrder entity)
    {
        context.ServiceOrders.Update(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var order = await GetAsync(id);
        if (order == null) return false;

        context.ServiceOrders.Remove(order);
        await context.SaveChangesAsync();
        return true;
    }
}
