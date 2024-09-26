namespace InstitutionStatistic.Query.Models.BaseModel;

/// <summary>
/// Базовый класс для сущности с именем
/// </summary>
public abstract class EntityWithName : Entity
{ 
    /// <summary>
    /// Name
    /// </summary>
    required public string Name { get; init; }
}
