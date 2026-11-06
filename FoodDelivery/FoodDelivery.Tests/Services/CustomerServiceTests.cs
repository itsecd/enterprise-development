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
    public void Update_PreservesCustomerReferenceInOrder()
    {
        var data = new FoodDeliveryData();
        var repository = new CustomerRepository(data);
        var service = new CustomerService(repository);
        var order = data.Orders.First();
        var originalCustomer = order.Customer;

        var updatedCustomer = service.Update(
            originalCustomer.Id,
            new CustomerRequest
            {
                LastName = "Обновлённая фамилия",
                FirstName = originalCustomer.FirstName,
                Patronymic = originalCustomer.Patronymic,
                Phone = originalCustomer.Phone,
                DeliveryAddress = originalCustomer.DeliveryAddress
            });

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
    public void Delete_ThrowsConflictWhenCustomerHasOrders()
    {
        var data = new FoodDeliveryData();
        var repository = new CustomerRepository(data);
        var service = new CustomerService(repository);
        var customer = data.Orders.First().Customer;

        Assert.Throws<ResourceConflictException>(
            () => service.Delete(customer.Id));

        Assert.Same(customer, repository.GetById(customer.Id));
    }
}
