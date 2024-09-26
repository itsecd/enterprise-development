using InstitutionStatistic.Query.Enums;
using InstitutionStatistic.Query.Models.BaseModel;
using System.Diagnostics.CodeAnalysis;

namespace InstitutionStatistic.Query.Models;

/// <summary>
/// Реализация ректора
/// </summary>
public class Rector(
        ScientificDegree scientificDegree,
        AcademicRank academicRank) : Entity
{
    /// <summary>
    /// ФИО
    /// </summary>
    required public string FullName { get; set; }

    /// <summary>
    /// Научная степень
    /// </summary>
    public ScientificDegree? Degree { get; set; } = scientificDegree;

    /// <summary>
    /// Звание
    /// </summary>
    public AcademicRank? Rank { get; set; } = academicRank;

}
