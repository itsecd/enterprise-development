using DryCleaner.Application.Contracts.DTOs.Customer;

namespace DryCleaner.Application.Contracts.Interfaces;

public interface ICustomerService
    : IApplicationService<CustomerDto, CustomerCreateUpdateDto, int>
{
}