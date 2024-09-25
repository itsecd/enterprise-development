namespace InstitutionStatistic.Query.Models.BaseModel;

/// <summary>
/// Базовый класс для сущности с именем
/// </summary>
public abstract class EntityWithName
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="name"></param>
    public EntityWithName(string name) 
    {
        Name = name;
        Id = Guid.NewGuid();
        Version = DateTime.Now;
    }

    /// <summary>
    /// ctor
    /// </summary>
    public EntityWithName()
    { 
        Id = Guid.NewGuid();
        Version = DateTime.Now;
    }

    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Version
    /// </summary>
    public DateTime Version { get; set; }
    
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }
}
