using FoodDelivery.Domain.Repositories;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализует репозиторий покупателей
/// </summary>
public class CustomerRepository()
    : InMemoryRepository<Customer>(TestData.Customers)
{
    /// <inheritdoc />
    public override async Task<Customer?> UpdateAsync(Customer customer)
    {
        var existingCustomer = await GetByIdAsync(customer.Id);

        if (existingCustomer is null)
        {
            return null;
        }

        existingCustomer.LastName = customer.LastName;
        existingCustomer.FirstName = customer.FirstName;
        existingCustomer.Patronymic = customer.Patronymic;
        existingCustomer.Phone = customer.Phone;
        existingCustomer.DeliveryAddress = customer.DeliveryAddress;

        return existingCustomer;
    }
}