using InstitutionStatistic.Query.Enums;
using InstitutionStatistic.Query.Models.BaseModel;

namespace InstitutionStatistic.Query.Models;

/// <summary>
/// Реализация ректора
/// </summary>
public class Rector: Entity
{
    #region ctors
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="fullName"></param>
    /// <param name="scientificDegree"></param>
    /// <param name="academicRank"></param>
    public Rector(
        string fullName,
        ScientificDegree scientificDegree,
        AcademicRank academicRank)
    {
        FullName = fullName;
        Degree = scientificDegree;
        Rank = academicRank;
    }

    /// <summary>
    /// ctor
    /// </summary>
    public Rector() { }
    #endregion

    #region Properties
    /// <summary>
    /// ФИО
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Научная степень
    /// </summary>
    public ScientificDegree Degree { get; set; }

    /// <summary>
    /// Звание
    /// </summary>
    public AcademicRank Rank { get; set; }
    #endregion

}
