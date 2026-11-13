using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Реализует прикладные операции над покупателями.
/// </summary>
public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    /// <inheritdoc />
    public async Task<CustomerDto> CreateAsync(CustomerCreateUpdateDto dto)
    {
        var customer = new Customer
        {
            Id = 0,
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            Patronymic = dto.Patronymic,
            Phone = dto.Phone,
            DeliveryAddress = dto.DeliveryAddress
        };

        var createdCustomer = await customerRepository.AddAsync(customer);

        return MapToDto(createdCustomer);
    }

    /// <inheritdoc />
    public async Task<CustomerDto?> GetAsync(int id)
    {
        var customer = await customerRepository.GetByIdAsync(id);

        return customer is null ? null : MapToDto(customer);
    }

    /// <inheritdoc />
    public async Task<IList<CustomerDto>> GetAllAsync()
    {
        var customers = await customerRepository.GetAllAsync();

        return customers
            .Select(MapToDto)
            .ToList();
    }

    /// <inheritdoc />
    public async Task<CustomerDto> UpdateAsync(CustomerCreateUpdateDto dto, int id)
    {
        var customer = new Customer
        {
            Id = id,
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            Patronymic = dto.Patronymic,
            Phone = dto.Phone,
            DeliveryAddress = dto.DeliveryAddress
        };

        var updatedCustomer = await customerRepository.UpdateAsync(customer);

        if (updatedCustomer is null)
        {
            throw new KeyNotFoundException($"Покупатель с идентификатором {id} не найден");
        }

        return MapToDto(updatedCustomer);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        return await customerRepository.DeleteAsync(id);
    }

    private static CustomerDto MapToDto(Customer customer)
    {
        return new CustomerDto(
            customer.Id,
            customer.LastName,
            customer.FirstName,
            customer.Patronymic,
            customer.Phone,
            customer.DeliveryAddress);
    }
}