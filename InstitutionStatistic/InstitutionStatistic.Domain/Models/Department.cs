using InstitutionStatistic.Domain.Models.BaseModel;

namespace InstitutionStatistic.Domain.Models;
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
