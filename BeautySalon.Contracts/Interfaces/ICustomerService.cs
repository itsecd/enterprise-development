using BeautySalon.Contracts.Customers;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Контракт сервиса CustomerService
/// </summary>
public interface ICustomerService : ICrudService<SaveCustomerRequest, CustomerResponse>
{
}
