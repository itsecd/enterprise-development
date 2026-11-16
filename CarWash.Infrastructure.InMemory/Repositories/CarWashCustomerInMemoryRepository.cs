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
    public Task<CarWashCustomer> Create(CarWashCustomer entity)
    {
        data.Customers.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<CarWashCustomer?> Read(int entityId)
    {
        return Task.FromResult<CarWashCustomer?>(
            data.Customers.FirstOrDefault(customer => customer.Id == entityId));
    }

    public Task<IList<CarWashCustomer>> ReadAll()
    {
        return Task.FromResult<IList<CarWashCustomer>>(
            [.. data.Customers]);
    }

    public Task<CarWashCustomer> Update(CarWashCustomer entity)
    {
        var existingCustomer = data.Customers.FirstOrDefault(
            customer => customer.Id == entity.Id)
            ?? throw new KeyNotFoundException(
                $"Клиент с идентификатором {entity.Id} не найден");

        existingCustomer.LastName = entity.LastName;
        existingCustomer.FirstName = entity.FirstName;
        existingCustomer.Patronymic = entity.Patronymic;
        existingCustomer.Phone = entity.Phone;

        return Task.FromResult(existingCustomer);
    }

    public async Task<bool> Delete(int entityId)
    {
        var customer = await Read(entityId);

        return customer is not null && data.Customers.Remove(customer);
    }
}