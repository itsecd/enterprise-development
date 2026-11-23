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
    public override async Task<Customer?> UpdateAsync(Customer entity)
    {
        var existing = await GetByIdAsync(entity.Id);
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