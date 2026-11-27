namespace VetClinic.Domain.Interfaces;

/// <summary>
/// Базовый интерфейс сущности с идентификатором
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    public int Id { get; set; }
}