using DryCleaner.Application.Contracts.DTOs.Item;
using DryCleaner.Application.Contracts.DTOs.ItemCategory;
using DryCleaner.Application.Contracts.DTOs.Customer;
using DryCleaner.Application.Contracts.DTOs.Order;
using DryCleaner.Application.Contracts.Interfaces;
using DryCleaner.Domain.Repositories;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Application.Services;

/// <summary>
/// Прикладные операции над клиентами
/// </summary>
public class CustomerService(IRepository<Customer> customerRepository) : ICustomerService
{
    /// <inheritdoc />
    public async Task<CustomerDto> CreateAsync(CustomerCreateUpdateDto dto)
    {
        var customer = new Customer
        {
            Id = 0,
            FirstName = dto.FirstName,
            Patronymic = dto.Patronymic,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber
        };

        var created = await customerRepository.AddAsync(customer);

        return new CustomerDto(
            created.Id,
            created.FirstName,
            created.Patronymic,
            created.LastName,
            created.PhoneNumber);
    }

    /// <inheritdoc />
    public async Task<CustomerDto?> GetAsync(int id)
    {
        var customer = await customerRepository.GetByIdAsync(id);
        if (customer is null) return null;

        return new CustomerDto(
            customer.Id,
            customer.FirstName,
            customer.Patronymic,
            customer.LastName,
            customer.PhoneNumber);
    }

    /// <inheritdoc />
    public async Task<IList<CustomerDto>> GetAllAsync()
    {
        var customers = await customerRepository.GetAllAsync();
        return customers.Select(c => new CustomerDto(c.Id, c.FirstName, c.Patronymic, c.LastName, c.PhoneNumber)).ToList();
    }

    /// <inheritdoc />
    public async Task<CustomerDto> UpdateAsync(CustomerCreateUpdateDto dto, int id)
    {
        var customer = new Customer
        {
            Id = id,
            FirstName = dto.FirstName,
            Patronymic = dto.Patronymic,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber
        };

        var updated = await customerRepository.UpdateAsync(customer);
        if (updated is null)
        {
            throw new KeyNotFoundException($"Клиент с идентификатором {id} не найден");
        }

        return new CustomerDto(
            updated.Id,
            updated.FirstName,
            updated.Patronymic,
            updated.LastName,
            updated.PhoneNumber);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        return await customerRepository.DeleteAsync(id);
    }
}