using InstitutionStatistic.Query.Enums;
using InstitutionStatistic.Query.Models.BaseModel;

namespace InstitutionStatistic.Query.Models;

/// <summary>
/// Реализация сущности институт
/// </summary>
public class Institution: EntityWithName
{
    #region ctors
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="name"></param>
    /// <param name="registrationNumber"></param>
    /// <param name="address"></param>
    /// <param name="rector"></param>
    /// <param name="faculties"></param>
    /// <param name="buildingOwnership"></param>
    /// <param name="institutionOwnership"></param>
    public Institution(
        string name,
        string registrationNumber,
        string address,
        Rector rector,
        ICollection<Faculty> faculties,
        BuildingOwnership buildingOwnership,
        InstitutionOwnership institutionOwnership): base(name)
    {
        Name = name;
        RegistrationNumber = registrationNumber;
        Address = address;
        Rector = rector;
        Faculties = faculties;
        BuildingOwnership = buildingOwnership;
        InstitutionOwnership = institutionOwnership;
    }

    /// <summary>
    /// ctor
    /// </summary>
    public Institution() { }
    #endregion

    #region Properties
    /// <summary>
    /// Регистрационный номер
    /// </summary>
    public string RegistrationNumber { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public string Address { get; set; } //адрес можно сделать отдельной сущностью, но для лабы это перебор

    /// <summary>
    /// Ректор
    /// </summary>
    public virtual Rector Rector { get; set; }
    
    /// <summary>
    /// Факультеты
    /// </summary>
    public virtual ICollection<Faculty> Faculties { get; set; }

    /// <summary>
    /// Собственность зданий
    /// </summary>
    public BuildingOwnership BuildingOwnership { get; set; }

    /// <summary>
    /// Собственность учреждения
    /// </summary>
    public InstitutionOwnership InstitutionOwnership { get; set; }
    #endregion

}
