using InstitutionStatistic.Query.Models.BaseModel;

namespace InstitutionStatistic.Query.Models;

/// <summary>
/// Реализация сущности факультет
/// </summary>
public class Faculty : EntityWithName
{

    /// <summary>
    /// Институт
    /// </summary>
    public virtual Institution? Institution { get; set; }

    /// <summary>
    /// Кафедры
    /// </summary>
    public virtual ICollection<Department> Departments { get; set; } = [];

}
