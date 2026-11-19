using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий сущностей Customer
/// </summary>
/// <param name="storage">Общее хранилище салона</param>
public sealed class CustomerRepository(SalonStorage storage)
    : InMemoryRepository<Customer>(storage, storage.Customers, EntityCopies.Copy), ICustomerRepository
{
    /// <inheritdoc />
    protected override void ApplyChanges(Customer current, Customer saved)
    {
        current.LastName = saved.LastName;
        current.FirstName = saved.FirstName;
        current.Patronymic = saved.Patronymic;
        current.Gender = saved.Gender;
        current.DateOfBirth = saved.DateOfBirth;
        current.PhoneNumber = saved.PhoneNumber;
    }

    /// <inheritdoc />
    protected override void CheckDeletion(Customer entity)
    {
        if (Storage.Appointments.Any(item => item.Customer.Id == entity.Id))
        {
            throw new InvalidOperationException("Объект используется в записях");
        }
    }
}
