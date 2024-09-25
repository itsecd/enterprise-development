namespace InstitutionStatistic.Query.Models.BaseModel;

/// <summary>
/// Базовый класс для сущности
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// ctor
    /// </summary>
    protected Entity() 
    {
        Id = Guid.NewGuid();
        Version = DateTime.Now;
    }

    /// <summary>
    /// Id
    /// </summary>
    required public Guid Id { get; set; }

    /// <summary>
    /// Version
    /// </summary>
    required public DateTime Version { get; set; }
}
