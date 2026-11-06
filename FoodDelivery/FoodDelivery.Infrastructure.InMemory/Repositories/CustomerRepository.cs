using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Хранит клиентов в памяти
/// </summary>
/// <param name="data">Данные приложения</param>
public sealed class CustomerRepository(FoodDeliveryData data)
    : InMemoryRepository<Customer>(
        data.Customers,
        customer => customer.Id,
        InMemorySynchronization.For(data)),
        ICustomerRepository
{
    /// <inheritdoc/>
    public bool HasOrders(int customerId)
    {
        return this.Execute<bool>(() =>
        {
            return data.Orders.Any(order => order.Customer.Id == customerId);
        });
    }

    /// <inheritdoc/>
    protected override void SetId(Customer entity, int id)
    {
        entity.Id = id;
    }

    /// <inheritdoc/>
    protected override void UpdateEntity(
        Customer existingEntity,
        Customer updatedEntity)
    {
        existingEntity.LastName = updatedEntity.LastName;
        existingEntity.FirstName = updatedEntity.FirstName;
        existingEntity.Patronymic = updatedEntity.Patronymic;
        existingEntity.Phone = updatedEntity.Phone;
        existingEntity.DeliveryAddress = updatedEntity.DeliveryAddress;
    }
}
