using CarWash.Domain;
using CarWash.Domain.Data;
using CarWash.Domain.Entities;

namespace CarWash.Infrastructure.InMemory.Repositories;

/// <summary>
/// Имплементация репозитория для заказов
/// </summary>
public class OrderInMemoryRepository(CarWashData data)
    : IRepository<Order, int>
{
    public void Create(Order entity)
    {
        data.Orders.Add(entity);
    }

    public Order Read(int entityId)
    {
        return data.Orders.FirstOrDefault(order => order.Id == entityId)
        ?? throw new KeyNotFoundException(
            $"Заказ с идентификатором {entityId} не найден.");
    }

    public List<Order> ReadAll()
    {
        return [.. data.Orders];
    }

    public void Update(Order entity)
    {
        var existingOrder = Read(entity.Id);

        existingOrder.CarId = entity.CarId;
        existingOrder.Car = entity.Car;
        existingOrder.ProcedureId = entity.ProcedureId;
        existingOrder.Procedure = entity.Procedure;
        existingOrder.StartTime = entity.StartTime;
        existingOrder.WashBoxNumber = entity.WashBoxNumber;
    }

    public void Delete(int entityId)
    {
        var order = Read(entityId);
        data.Orders.Remove(order);
    }
}