using InstitutionStatistic.Query.Enums;
using InstitutionStatistic.Query.Models.BaseModel;

namespace InstitutionStatistic.Query.Models;

/// <summary>
/// Реализация сущности институт
/// </summary>
public class Institution(
        Rector rector,
        BuildingOwnership buildingOwnership,
        InstitutionOwnership institutionOwnership) : EntityWithName
{
    /// <summary>
    /// Регистрационный номер
    /// </summary>
    required public string RegistrationNumber { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    required public string Address { get; set; } //адрес можно сделать отдельной сущностью, но для лабы это перебор

    /// <summary>
    /// Ректор
    /// </summary>
    public virtual Rector? Rector { get; set; } = rector;

    /// <summary>
    /// Факультеты
    /// </summary>
    public virtual ICollection<Faculty> Faculties { get; set; } = [];

    /// <summary>
    /// Собственность зданий
    /// </summary>
    public BuildingOwnership? BuildingOwnership { get; set; } = buildingOwnership;

    /// <summary>
    /// Собственность учреждения
    /// </summary>
    public InstitutionOwnership? InstitutionOwnership { get; set; } = institutionOwnership;
}
