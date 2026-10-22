using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Реализует прикладные операции над покупателями
/// </summary>
public class CustomerService(
    ICustomerRepository customerRepository)
    : ICustomerService
{
    /// <inheritdoc />
    public CustomerDto Create(CustomerCreateUpdateDto dto)
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

        var createdCustomer = customerRepository.Add(customer);

        return MapToDto(createdCustomer);
    }

    /// <inheritdoc />
    public CustomerDto? Get(int id)
    {
        var customer = customerRepository.GetById(id);

        return customer is null ? null : MapToDto(customer);
    }

    /// <inheritdoc />
    public IList<CustomerDto> GetAll()
    {
        return customerRepository.GetAll()
            .Select(MapToDto)
            .ToList();
    }

    /// <inheritdoc />
    public CustomerDto Update(CustomerCreateUpdateDto dto, int id)
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

        var updatedCustomer = customerRepository.Update(customer);

        if (updatedCustomer is null)
        {
            throw new KeyNotFoundException(
                $"Покупатель с идентификатором {id} не найден");
        }

        return MapToDto(updatedCustomer);
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        return customerRepository.Delete(id);
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
