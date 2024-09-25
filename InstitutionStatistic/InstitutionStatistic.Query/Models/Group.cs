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
    public Group(string number, Department department, Speciality speciality ) 
    {
        Number = number;
        Department = department;
        Speciality = speciality;
    }
    
    /// <summary>
    /// ctor
    /// </summary>
    public Group() { }

    /// <summary>
    /// Номер группы
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// Кафедра
    /// </summary>
    public virtual Department Department { get; set; }

    /// <summary>
    /// Специальность
    /// </summary>
    public virtual Speciality Speciality { get; set; }

}
