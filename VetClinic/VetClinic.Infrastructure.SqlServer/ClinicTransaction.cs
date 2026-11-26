using System.Data;
using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Repositories;

namespace VetClinic.Infrastructure.SqlServer;

/// <summary>
/// Управляет транзакциями общего контекста репозиториев
/// </summary>
/// <param name="context">Контекст текущего запроса</param>
public sealed class ClinicTransaction(VetClinicDbContext context) : IClinicTransaction
{
    /// <inheritdoc/>
    /// <remarks>
    /// Отслеживаемые сущности сбрасываются, чтобы проверки читали базу заново.
    /// Уровень Serializable удерживает блокировки до записи, поэтому проверка и сохранение не расходятся.
    /// Если связь потеряна во время Commit, повтор операции опасен, поэтому ошибка пробрасывается дальше.
    /// </remarks>
    public Task<T> ExecuteSerializableAsync<T>(
        Func<CancellationToken, Task<T>> operation, CancellationToken cancellationToken)
    {
        if (context.ChangeTracker.HasChanges())
        {
            throw new InvalidOperationException("Перед выполнением операции в контексте есть несохранённые изменения");
        }

        return context.Database.CreateExecutionStrategy().ExecuteAsync(async token =>
        {
            context.ChangeTracker.Clear();

            var commitStarted = false;
            try
            {
                await using var transaction = await context.Database.BeginTransactionAsync(IsolationLevel.Serializable, token);
                var result = await operation(token);
                commitStarted = true;
                await transaction.CommitAsync(token);
                return result;
            }
            catch (Exception exception) when (commitStarted && exception is not OperationCanceledException)
            {
                throw new InvalidOperationException("Не удалось подтвердить завершение транзакции", exception);
            }
        }, cancellationToken);
    }
}
