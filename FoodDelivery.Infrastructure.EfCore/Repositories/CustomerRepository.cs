using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Infrastructure.EfCore.Repositories;

/// <summary>
/// Реализует репозиторий покупателей с использованием efcore
/// </summary>
public class CustomerRepository(FoodDeliveryDbContext dbContext)
    : EfRepository<Customer>(dbContext),
      ICustomerRepository
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

        await DbContext.SaveChangesAsync();

        return existingCustomer;
    }
}