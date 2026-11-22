namespace BeautySalon.Domain.Interfaces;

/// <summary>
/// Интерфейс для доменных сущностей с уникальным идентификатором
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Уникальный идентификатор сущности
    /// </summary>
    public int Id { get; set; }
}