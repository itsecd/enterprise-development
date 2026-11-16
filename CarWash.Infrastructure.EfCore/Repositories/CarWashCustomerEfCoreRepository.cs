using CarWash.Domain;
using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core репозиторий для клиентов автомойки
/// </summary>
public class CarWashCustomerEfCoreRepository(CarWashDbContext context)
    : IRepository<CarWashCustomer, int>
{
    public async Task<CarWashCustomer> Create(CarWashCustomer entity)
    {
        entity.Id = 0;
        context.CarWashCustomers.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(int entityId)
        => await context.CarWashCustomers
            .Where(customer => customer.Id == entityId)
            .ExecuteDeleteAsync() > 0;

    public async Task<CarWashCustomer?> Read(int entityId)
        => await context.CarWashCustomers
            .Include(customer => customer.Cars)
            .FirstOrDefaultAsync(customer => customer.Id == entityId);

    public async Task<IList<CarWashCustomer>> ReadAll()
        => await context.CarWashCustomers.ToListAsync();

    public async Task<CarWashCustomer> Update(CarWashCustomer entity)
    {
        var existing = await context.CarWashCustomers
            .FirstOrDefaultAsync(customer => customer.Id == entity.Id)
            ?? throw new KeyNotFoundException(
                $"Клиент с Id {entity.Id} не найден");

        context.Entry(existing).CurrentValues.SetValues(entity);

        await context.SaveChangesAsync();
        return existing;
    }
}