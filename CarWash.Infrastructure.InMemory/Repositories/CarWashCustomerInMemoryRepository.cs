using CarWash.Domain;
using CarWash.Domain.Data;
using CarWash.Domain.Entities;

namespace CarWash.Infrastructure.InMemory.Repositories;

/// <summary>
/// Имплементация репозитория для клиентов
/// </summary>
public class CarWashCustomerInMemoryRepository(CarWashData data)
    : IRepository<CarWashCustomer, int>
{
    public void Create(CarWashCustomer entity)
    {
        data.Customers.Add(entity);
    }

    public CarWashCustomer Read(int entityId)
    {
        return data.Customers.FirstOrDefault(customer => customer.Id == entityId)
         ?? throw new KeyNotFoundException(
             $"Клиент с идентификатором {entityId} не найден.");
    }

    public List<CarWashCustomer> ReadAll()
    {
        return [.. data.Customers];
    }

    public void Update(CarWashCustomer entity)
    {
        var existingCustomer = Read(entity.Id);

        existingCustomer.LastName = entity.LastName;
        existingCustomer.FirstName = entity.FirstName;
        existingCustomer.Patronymic = entity.Patronymic;
        existingCustomer.Phone = entity.Phone;
    }

    public void Delete(int entityId)
    {
        var customer = Read(entityId);
        data.Customers.Remove(customer);
    }
}