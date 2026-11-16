using CarWash.Domain;
using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core репозиторий для процедур автомойки
/// </summary>
public class CarWashProcedureEfCoreRepository(CarWashDbContext context)
    : IRepository<CarWashProcedure, int>
{
    public async Task<CarWashProcedure> Create(CarWashProcedure entity)
    {
        entity.Id = 0;
        context.CarWashProcedures.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(int entityId)
        => await context.CarWashProcedures
            .Where(procedure => procedure.Id == entityId)
            .ExecuteDeleteAsync() > 0;

    public async Task<CarWashProcedure?> Read(int entityId)
        => await context.CarWashProcedures
            .Include(procedure => procedure.Orders)
            .FirstOrDefaultAsync(procedure => procedure.Id == entityId);

    public async Task<IList<CarWashProcedure>> ReadAll()
        => await context.CarWashProcedures.ToListAsync();

    public async Task<CarWashProcedure> Update(CarWashProcedure entity)
    {
        var existing = await context.CarWashProcedures
            .FirstOrDefaultAsync(procedure => procedure.Id == entity.Id)
            ?? throw new KeyNotFoundException(
                $"Процедура с Id {entity.Id} не найдена");

        context.Entry(existing).CurrentValues.SetValues(entity);

        await context.SaveChangesAsync();
        return existing;
    }
}