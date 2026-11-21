using FoodDelivery.Application.Contracts.Customers;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Выполняет операции с клиентами
/// </summary>
/// <param name="repository">Репозиторий клиентов</param>
public sealed class CustomerService(ICustomerRepository repository)
    : CrudServiceBase<Customer, CustomerDto, CustomerRequest>
{
    /// <inheritdoc/>
    protected override IRepository<Customer> Repository => repository;

    /// <inheritdoc/>
    public override async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await repository.HasOrdersAsync(id, cancellationToken))
        {
            throw new ResourceConflictException(
                "Нельзя удалить клиента, у которого есть заказы");
        }

        await base.DeleteAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    protected override CustomerDto ToDto(Customer entity)
    {
        return new CustomerDto
        {
            Id = entity.Id,
            LastName = entity.LastName,
            FirstName = entity.FirstName,
            Patronymic = entity.Patronymic,
            Phone = entity.Phone,
            DeliveryAddress = entity.DeliveryAddress
        };
    }

    /// <inheritdoc/>
    protected override Task<Customer> ToEntityAsync(
        CustomerRequest request,
        int id,
        Customer? existing,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(new Customer
        {
            Id = id,
            LastName = request.LastName.Trim(),
            FirstName = request.FirstName.Trim(),
            Patronymic = string.IsNullOrWhiteSpace(request.Patronymic)
                ? null
                : request.Patronymic.Trim(),
            Phone = request.Phone.Trim(),
            DeliveryAddress = request.DeliveryAddress.Trim()
        });
    }
}
