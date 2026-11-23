namespace VetClinic.Domain.Repositories;

/// <summary>
/// Выполняет проверку связанных записей и их изменение в одной транзакции
/// </summary>
public interface IClinicTransaction
{
    /// <summary>
    /// Выполняет отдельную операцию с уровнем изоляции Serializable в контексте без несохранённых изменений
    /// </summary>
    /// <typeparam name="T">Результат операции</typeparam>
    /// <param name="operation">Проверка и запись в одной транзакции</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<T> ExecuteSerializableAsync<T>(Func<CancellationToken, Task<T>> operation, CancellationToken cancellationToken);
}
