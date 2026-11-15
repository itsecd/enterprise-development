namespace FitnesClub.Domain.Interfaces;

/// <summary>
/// Базовый контракт доменной сущности с уникальным идентификатором.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Уникальный идентификатор сущности.
    /// </summary>
    public Guid Id { get; set; }
}