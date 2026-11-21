using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EfCore.Repositories;

/// <summary>Хранит клиентов в MySQL</summary>
/// <param name="context">Контекст базы данных</param>
public sealed class CustomerRepository(FoodDeliveryDbContext context)
    : EfCoreRepository<Customer>(context), ICustomerRepository
{
    /// <inheritdoc/>
    public Task<bool> HasOrdersAsync(int customerId, CancellationToken cancellationToken = default)
    {
        return Context.Orders.AnyAsync(order => order.CustomerId == customerId, cancellationToken);
    }
}
