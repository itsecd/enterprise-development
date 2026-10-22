using FoodDelivery.Application.Contracts.DTOs;

namespace FoodDelivery.Application.Contracts.Interfaces;

public interface ICustomerService
    : IApplicationService<CustomerDto, CustomerCreateUpdateDto, int>
{
}