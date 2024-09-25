using InstitutionStatistic.Query.Models.BaseModel;

namespace InstitutionStatistic.Query.Models;

/// <summary>
/// Реализация сущности группа
/// </summary>
public class Group: Entity
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="number"></param>
    /// <param name="department"></param>
    /// <param name="speciality"></param>
    public Group(string number, Department department, Speciality speciality ): base()
    {
        Number = number;
        Department = department;
        Speciality = speciality;
    }

    /// <summary>
    /// Номер группы
    /// </summary>
    required public string Number { get; set; }

    /// <summary>
    /// Кафедра
    /// </summary>
    required public virtual Department Department { get; set; }

    /// <summary>
    /// Специальность
    /// </summary>
    required public virtual Speciality Speciality { get; set; }

}
