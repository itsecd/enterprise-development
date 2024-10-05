using InstitutionStatistic.Domain.Models.BaseModel;

namespace InstitutionStatistic.Domain.Models;

/// <summary>
/// Реализация сущности специальность
/// </summary>
public class Speciality() : EntityWithName
{
    /// <summary>
    /// Код спцеаильности
    /// </summary>
    required public string Code { get; init; }

    /// <summary>
    /// Группы
    /// </summary>
    public virtual ICollection<Group> Groups { get; set; } = [];
}
