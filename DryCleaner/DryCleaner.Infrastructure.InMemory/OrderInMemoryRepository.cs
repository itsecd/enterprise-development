using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;


namespace DryCleaner.Infrastructure.InMemory;

/// <summary>
/// Репозиторий для управления данными заказов в оперативной памяти.
/// </summary>
public class OrderInMemoryRepository : IRepository<Order, int>
{
    /// <summary>
    /// Список заказов.
    /// </summary>
    private readonly List<Order> _orders = DataSeed.Orders;

    /// <summary>
    /// Последний сгенерированный идентификатор.
    /// </summary>
    private int _lastId = DataSeed.Orders.Count > 0
    ? DataSeed.Orders.Max(order => order.Id)
    : 0;

    /// <summary>
    /// Добавляет заказ в коллекцию и возвращает добавленный объект.
    /// </summary>
    /// <param name="entity">Заказ для добавления.</param>
    /// <returns>Задача, представляющая асинхронную операцию, с добавленным заказом в качестве результата.</returns>
    public Task<Order> Create(Order entity)
    {
        entity.Id = ++_lastId;
        _orders.Add(entity);
        return Task.FromResult(entity);
    }


    /// <summary>
    /// Удаляет заказ по идентификатору.
    /// </summary>
    /// <param name="entityId">Идентификатор удаляемого заказа.</param>
    /// <returns>Значение true, если заказ успешно удален; в противном случае — false.</returns>
    public Task<bool> Delete(int entityId)
    {
        var order = Read(entityId).Result;
        if (order == null)
            return Task.FromResult(false);

        var result = _orders.Remove(order);
        return Task.FromResult(result);

    }

    /// <summary>
    /// Возвращает заказ с указанным идентификатором или null, если заказ не найден.
    /// </summary>
    /// <param name="entityId">Идентификатор заказа.</param>
    /// <returns>Задача, представляющая асинхронную операцию, результатом которой является объект Order или null.</returns>
    public Task<Order?> Read(int entityId) => Task.FromResult(_orders.FirstOrDefault(o => o.Id == entityId));

    /// <summary>
    /// Возвращает список всех заказов.
    /// </summary>
    /// <returns>Задача, представляющая асинхронную операцию, результатом которой является cписок заказов.</returns>
    public Task<IList<Order>> ReadAll() => Task.FromResult<IList<Order>>([.. _orders]);

    /// <summary>
    /// Обновляет данные заказа или создает нового, если заказ не найден.
    /// </summary>
    /// <param name="entity">Заказ с обновленными данными.</param>
    /// <returns>Задача, представляющая асинхронную операцию, результатом которой является обновленный или созданный заказ.</returns>
    public Task<Order> Update(Order entity)
    {
        var order = Read(entity.Id).Result;

        if (order == null)
        {
            return Create(entity);
        }

        order.Client = entity.Client;
        order.Product = entity.Product;
        order.AcceptedAt = entity.AcceptedAt;
        order.ProcessingDays = entity.ProcessingDays;
        order.Status = entity.Status;
        return Task.FromResult(order);
    }
}
