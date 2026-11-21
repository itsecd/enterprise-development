using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>Выполняет операцию над коллекциями под общей блокировкой</summary>
/// <param name="data">Данные приложения</param>
public sealed class InMemoryOrderWriteTransaction(FoodDeliveryData data) : IOrderWriteTransaction
{
    /// <inheritdoc/>
    public Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> operation, CancellationToken cancellationToken)
    {
        lock (InMemorySynchronization.For(data))
        {
            // Репозитории коллекций возвращают уже завершённые операции.
            return operation(cancellationToken);
        }
    }
}
