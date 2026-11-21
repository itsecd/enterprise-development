using System.Data;
using FoodDelivery.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EfCore;

/// <summary>Сохраняет связи заказа и блюда при параллельных изменениях</summary>
/// <param name="context">Контекст базы данных</param>
public sealed class OrderWriteTransaction(FoodDeliveryDbContext context) : IOrderWriteTransaction
{
    /// <inheritdoc/>
    public async Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> operation, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(
            IsolationLevel.Serializable, cancellationToken);

        var result = await operation(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return result;
    }
}
