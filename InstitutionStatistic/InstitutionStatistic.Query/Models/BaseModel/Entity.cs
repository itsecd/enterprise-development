namespace InstitutionStatistic.Query.Models.BaseModel;

/// <summary>
/// Базовый класс для сущности
/// </summary>
public abstract class Entity
{
    #region ctors
    /// <summary>
    /// ctor
    /// </summary>
    protected Entity() 
    {
        Id = Guid.NewGuid();
        Version = DateTime.Now;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Version
    /// </summary>
    public DateTime Version { get; set; }
    #endregion
}
