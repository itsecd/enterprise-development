using CarWash.Domain;
using CarWash.Domain.Data;
using CarWash.Domain.Entities;

namespace CarWash.Infrastructure.InMemory.Repositories;

/// <summary>
/// Имплементация репозитория для вида процедуры
/// </summary>
public class CarWashProcedureInMemoryRepository(CarWashData data)
    : IRepository<CarWashProcedure, int>
{
    public Task<CarWashProcedure> Create(CarWashProcedure entity)
    {
        data.Procedures.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<CarWashProcedure?> Read(int entityId)
    {
        return Task.FromResult<CarWashProcedure?>(
            data.Procedures.FirstOrDefault(procedure => procedure.Id == entityId));
    }

    public Task<IList<CarWashProcedure>> ReadAll()
    {
        return Task.FromResult<IList<CarWashProcedure>>(
            [.. data.Procedures]);
    }

    public Task<CarWashProcedure> Update(CarWashProcedure entity)
    {
        var existingProcedure = data.Procedures.FirstOrDefault(
            procedure => procedure.Id == entity.Id)
            ?? throw new KeyNotFoundException(
                $"Процедура с идентификатором {entity.Id} не найдена");

        existingProcedure.Name = entity.Name;
        existingProcedure.CarCategory = entity.CarCategory;
        existingProcedure.Price = entity.Price;
        existingProcedure.Duration = entity.Duration;

        return Task.FromResult(existingProcedure);
    }

    public async Task<bool> Delete(int entityId)
    {
        var procedure = await Read(entityId);

        return procedure is not null && data.Procedures.Remove(procedure);
    }
}