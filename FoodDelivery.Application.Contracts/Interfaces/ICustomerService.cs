using FoodDelivery.Application.Contracts.DTOs;

namespace FoodDelivery.Application.Contracts.Interfaces;

/// <summary>
/// Прикладной сервис для работы с клиентами
/// </summary>
public interface ICustomerService
    : IApplicationService<CustomerDto, CustomerCreateUpdateDto, int>
{
}