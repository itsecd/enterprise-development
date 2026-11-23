namespace DryCleaner.Domain.Entities;

/// <summary>
/// Интерфейс для сущностей предметной области
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    int Id { get; set; }
}