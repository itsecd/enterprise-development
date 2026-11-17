namespace Hotel.Application.Contracts;

/// <summary>
/// Интерфейс для DTO, содержащих идентификатор
/// </summary>
public interface IHasId<TKey>
{
    /// <summary>
    /// Идентификатор записи
    /// </summary>
    public TKey Id { get; }
}