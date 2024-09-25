using InstitutionStatistic.Query.Models.BaseModel;

namespace InstitutionStatistic.Query.Models;

/// <summary>
/// Реализация сущности специальность
/// </summary>
public class Speciality: EntityWithName
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="name"></param>
    /// <param name="code"></param>
    /// <param name="groups"></param>
    public Speciality(string name, string code, ICollection<Group> groups) : base(name)
    {
        Code = code;
        Groups = groups;
    }

    /// <summary>
    /// Код спцеаильности
    /// </summary>
    required public string Code { get; set; }

    /// <summary>
    /// Группы
    /// </summary>
    required public virtual ICollection<Group> Groups { get; set; }
}
