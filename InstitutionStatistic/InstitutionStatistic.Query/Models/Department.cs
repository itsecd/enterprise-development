using InstitutionStatistic.Query.Models.BaseModel;

namespace InstitutionStatistic.Query.Models;
/// <summary>
/// Реализация сущности кафедра
/// </summary>
public class Department: EntityWithName
{
    #region ctors
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
    /// ctor
    /// </summary>
    public Department() { }
    #endregion

    #region Fields
    /// <summary>
    /// 
    /// </summary>
    public Faculty Faculty { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public ICollection<Group> Groups { get; set; }
    #endregion

}
