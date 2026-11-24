namespace Hotel.Domain.Interfaces;

/// <summary>
/// Защищает операции, одновременно затрагивающие связанные коллекции
/// </summary>
public interface IRelationshipLock
{
    /// <summary>
    /// Ожидает завершения текущей операции и возвращает объект освобождения блокировки
    /// </summary>
    public Task<IDisposable> AcquireAsync();
}
