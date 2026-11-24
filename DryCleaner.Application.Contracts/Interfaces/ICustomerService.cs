using DryCleaner.Application.Contracts.DTOs.Customer;

namespace DryCleaner.Application.Contracts.Interfaces;

/// <summary>
/// Определяет стандартные операции CRUD для работы с клиентами химчистки
/// </summary>
public interface ICustomerService
    : IApplicationService<CustomerDto, CustomerCreateUpdateDto, int>
{
}