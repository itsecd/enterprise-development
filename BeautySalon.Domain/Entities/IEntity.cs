namespace BeautySalon.Domain.Entities;

/// <summary>
/// Сущность с идентификатором
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    int Id { get; set; }
}
