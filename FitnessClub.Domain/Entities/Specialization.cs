using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Domain.Entities;

/// <summary>
/// Класс специализации (используется для тренера)
/// </summary>
public class Specialization : IEntity
{
    /// <summary>
    /// Идентифиактор специализации
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название специализации
    /// </summary>
    public required string Name { get; set; }
}
