using FoodDelivery.Application.Contracts.Customers;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Data;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Services;

/// <summary>
/// Проверяет прикладные операции с клиентами
/// </summary>
public sealed class CustomerServiceTests
{
    /// <summary>
    /// Проверяет обновление клиента без потери ссылки из заказа
    /// </summary>
    [Fact]
    public async Task Update_PreservesCustomerReferenceInOrder()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var data = new FoodDeliveryData();
        var repository = new CustomerRepository(data);
        var service = new CustomerService(repository);
        var order = data.Orders.First();
        var originalCustomer = order.Customer;

        var updatedCustomer = await service.UpdateAsync(
            originalCustomer.Id,
            new CustomerRequest
            {
                LastName = "Обновлённая фамилия",
                FirstName = originalCustomer.FirstName,
                Patronymic = originalCustomer.Patronymic,
                Phone = originalCustomer.Phone,
                DeliveryAddress = originalCustomer.DeliveryAddress
            }, cancellationToken);

        Assert.NotNull(updatedCustomer);
        Assert.Same(originalCustomer, order.Customer);
        Assert.Same(originalCustomer, repository.GetById(originalCustomer.Id));
        Assert.Equal("Обновлённая фамилия", order.Customer.LastName);
        Assert.Equal(order.Customer.LastName, updatedCustomer.LastName);
    }

    /// <summary>
    /// Проверяет запрет удаления клиента с заказами
    /// </summary>
    [Fact]
    public async Task Delete_ThrowsConflictWhenCustomerHasOrders()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var data = new FoodDeliveryData();
        var repository = new CustomerRepository(data);
        var service = new CustomerService(repository);
        var customer = data.Orders.First().Customer;

        await Assert.ThrowsAsync<ResourceConflictException>(
            async () => await service.DeleteAsync(customer.Id, cancellationToken));

        Assert.Same(customer, repository.GetById(customer.Id));
    }
}
