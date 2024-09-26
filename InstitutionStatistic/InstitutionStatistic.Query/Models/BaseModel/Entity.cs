namespace InstitutionStatistic.Query.Models.BaseModel;

/// <summary>
/// Базовый класс для сущности
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Id
    /// </summary>
    required public Guid Id { get; init; }
    
    /// <summary>
    /// Version
    /// </summary>
    required public DateTime Version { get; init; }
}
