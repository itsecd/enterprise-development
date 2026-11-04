using DryCleaner.Domain.Repositories;
using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализация репозитория для работы с заказами в памяти
/// </summary>
public class OrderRepository()
    : InMemoryRepository<Order>(DataSeed.Orders.ToList())
{
    /// <inheritdoc />
    public override Order? Update(Order entity)
    {
        var existing = GetById(entity.Id);
        if (existing is null)
        {
            return null;
        }

        existing.CustomerId = entity.CustomerId;
        existing.Customer = entity.Customer;
        existing.ItemId = entity.ItemId;
        existing.Item = entity.Item;
        existing.AcceptanceDate = entity.AcceptanceDate;
        existing.CompletionDays = entity.CompletionDays;
        existing.Status = entity.Status;

        return existing;
    }
}