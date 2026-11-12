using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.InMemory.Repositories;

/// <summary>
/// Хранение сущностей Customer в памяти
/// </summary>
/// <param name="storage">Общее хранилище салона</param>
public sealed class CustomerRepository(SalonStorage storage) : ICustomerRepository
{
    /// <inheritdoc />
    public IReadOnlyList<Customer> GetAll()
    {
        lock (storage.SyncRoot)
        {
            return storage.Customers.Select(EntityCopies.Copy).ToList();
        }
    }

    /// <inheritdoc />
    public Customer? GetById(int id)
    {
        lock (storage.SyncRoot)
        {
            var entity = storage.Customers.FirstOrDefault(item => item.Id == id);
            return entity is null ? null : EntityCopies.Copy(entity);
        }
    }

    /// <inheritdoc />
    public Customer Add(Customer entity)
    {
        lock (storage.SyncRoot)
        {
            var saved = EntityCopies.Copy(entity);

            saved.Id = storage.NextId<Customer>();
            storage.Customers.Add(saved);
            return EntityCopies.Copy(saved);
        }
    }

    /// <inheritdoc />
    public bool Update(Customer entity)
    {
        lock (storage.SyncRoot)
        {
            var current = storage.Customers.FirstOrDefault(item => item.Id == entity.Id);
            if (current is null)
            {
                return false;
            }

            var saved = EntityCopies.Copy(entity);

            current.LastName = saved.LastName;
            current.FirstName = saved.FirstName;
            current.Patronymic = saved.Patronymic;
            current.Gender = saved.Gender;
            current.DateOfBirth = saved.DateOfBirth;
            current.PhoneNumber = saved.PhoneNumber;
            return true;
        }
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        lock (storage.SyncRoot)
        {
            if (storage.Appointments.Any(item => item.Customer.Id == id))
            {
                throw new InvalidOperationException("Объект используется в записях");
            }

            return storage.Customers.RemoveAll(item => item.Id == id) > 0;
        }
    }
}