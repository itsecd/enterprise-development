using VetClinic.Domain.Repositories;

namespace VetClinic.Infrastructure.InMemory;

/// <summary>
/// Транзакция в памяти: данные лежат в коллекциях, операция выполняется сразу
/// </summary>
public sealed class InMemoryClinicTransaction : IClinicTransaction
{
    /// <inheritdoc/>
    public Task<T> ExecuteSerializableAsync<T>(Func<CancellationToken, Task<T>> operation, CancellationToken cancellationToken) =>
        operation(cancellationToken);
}
