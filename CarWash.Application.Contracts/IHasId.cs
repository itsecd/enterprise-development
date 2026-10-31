namespace CarWash.Application.Contracts;

/// <summary>
/// Интерфейс для сущностей, имеющих идентификатор
/// </summary>
/// <typeparam name="TKey">Тип идентификатора</typeparam>
public interface IHasId<out TKey> where TKey : struct
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    TKey Id { get; }
}