using InstitutionStatistic.Query.Models.BaseModel;

namespace InstitutionStatistic.Query.Models;
/// <summary>
/// Реализация сущности кафедра
/// </summary>
public class Department: EntityWithName
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="name"></param>
    /// <param name="faculty"></param>
    /// <param name="groups"></param>
    public Department(string name, Faculty faculty, ICollection<Group> groups): base(name)
    {
        Faculty = faculty;
        Groups = groups;
    }

    /// <summary>
    /// Факультет
    /// </summary>
    required public Faculty Faculty { get; set; }

    /// <summary>
    /// Группы
    /// </summary>
    required public ICollection<Group> Groups { get; set; }

}
