using InstitutionStatistic.Query.Enums;
using InstitutionStatistic.Query.Models.BaseModel;

namespace InstitutionStatistic.Query.Models;

/// <summary>
/// Реализация ректора
/// </summary>
public class Rector: Entity
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="fullName"></param>
    /// <param name="scientificDegree"></param>
    /// <param name="academicRank"></param>
    public Rector(
        string fullName,
        ScientificDegree scientificDegree,
        AcademicRank academicRank): base()
    {
        FullName = fullName;
        Degree = scientificDegree;
        Rank = academicRank;
    }


    /// <summary>
    /// ФИО
    /// </summary>
    required public string FullName { get; set; }

    /// <summary>
    /// Научная степень
    /// </summary>
    required public ScientificDegree Degree { get; set; }

    /// <summary>
    /// Звание
    /// </summary>
    required public AcademicRank Rank { get; set; }

}
