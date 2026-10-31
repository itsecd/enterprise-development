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
    public void Create(CarWashProcedure entity)
    {
        data.Procedures.Add(entity);
    }

    public CarWashProcedure Read(int entityId)
    {
        return data.Procedures.FirstOrDefault(procedure => procedure.Id == entityId)
        ?? throw new KeyNotFoundException(
            $"Процедура с идентификатором {entityId} не найдена.");
    }

    public List<CarWashProcedure> ReadAll()
    {
        return [.. data.Procedures];
    }

    public void Update(CarWashProcedure entity)
    {
        var existingProcedure = Read(entity.Id);

        existingProcedure.Name = entity.Name;
        existingProcedure.CarCategory = entity.CarCategory;
        existingProcedure.Price = entity.Price;
        existingProcedure.Duration = entity.Duration;
    }

    public void Delete(int entityId)
    {
        var procedure = Read(entityId);
        data.Procedures.Remove(procedure);
    }
}