namespace MuseumSystem.Domain.Interfaces;

/// <summary>
/// Базовый интерфейс для всех сущностей доменной области
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Уникальный идентификатор сущности
    /// </summary>
    public int Id { get; set; }
}