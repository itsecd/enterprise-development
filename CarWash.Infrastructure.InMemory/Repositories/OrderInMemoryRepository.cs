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
    public Task<Order> Create(Order entity)
    {
        data.Orders.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<Order?> Read(int entityId)
    {
        return Task.FromResult<Order?>(
            data.Orders.FirstOrDefault(order => order.Id == entityId));
    }

    public Task<IList<Order>> ReadAll()
    {
        return Task.FromResult<IList<Order>>(
            [.. data.Orders]);
    }

    public Task<Order> Update(Order entity)
    {
        var existingOrder = data.Orders.FirstOrDefault(
            order => order.Id == entity.Id)
            ?? throw new KeyNotFoundException(
                $"Заказ с идентификатором {entity.Id} не найден");

        existingOrder.CarId = entity.CarId;
        existingOrder.Car = entity.Car;
        existingOrder.ProcedureId = entity.ProcedureId;
        existingOrder.Procedure = entity.Procedure;
        existingOrder.StartTime = entity.StartTime;
        existingOrder.WashBoxNumber = entity.WashBoxNumber;

        return Task.FromResult(existingOrder);
    }

    public async Task<bool> Delete(int entityId)
    {
        var order = await Read(entityId);

        return order is not null && data.Orders.Remove(order);
    }
}