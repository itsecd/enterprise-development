namespace InstitutionStatistic.Query.Models.BaseModel;

/// <summary>
/// Базовый класс для сущности с именем
/// </summary>
public abstract class EntityWithName: Entity
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="name"></param>
    public EntityWithName(string name): base()
    {
        Name = name;
    }
    
    /// <summary>
    /// Name
    /// </summary>
    required public string Name { get; set; }
}
