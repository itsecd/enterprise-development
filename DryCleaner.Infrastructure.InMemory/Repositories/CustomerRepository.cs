using DryCleaner.Domain.Repositories;
using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализация репозитория для работы с клиентами в памяти
/// </summary>
public class CustomerRepository()
    : InMemoryRepository<Customer>(DataSeed.Customers.ToList())
{
    /// <inheritdoc />
    public override Customer? Update(Customer entity)
    {
        var existing = GetById(entity.Id);
        if (existing is null)
        {
            return null;
        }

        existing.FirstName = entity.FirstName;
        existing.Patronymic = entity.Patronymic;
        existing.LastName = entity.LastName;
        existing.PhoneNumber = entity.PhoneNumber;

        return existing;
    }
}