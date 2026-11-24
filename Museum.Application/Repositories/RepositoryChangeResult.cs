namespace Museum.Application.Repositories;

/// <summary>
/// Результат изменения связанной записи в памяти.
/// </summary>
public enum RepositoryChangeResult
{
    /// <summary>Операция выполнена.</summary>
    Success,
    /// <summary>Изменяемая запись не найдена.</summary>
    NotFound,
    /// <summary>Связанная запись не найдена.</summary>
    InvalidReference,
    /// <summary>Такая связь уже существует.</summary>
    Conflict
}
