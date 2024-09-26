using InstitutionStatistic.Query.Models.BaseModel;

namespace InstitutionStatistic.Query.Models;
/// <summary>
/// Реализация сущности кафедра
/// </summary>
public class Department : EntityWithName
{
    /// <summary>
    /// Факультет
    /// </summary>
    public Faculty? Faculty { get; set; }

    /// <summary>
    /// Группы
    /// </summary>
    public ICollection<Group> Groups { get; set; } = [];

}
