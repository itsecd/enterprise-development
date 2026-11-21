using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Domain.Entities;

/// <summary>
/// Специализация тренера фитнес-клуба.
/// </summary>
public class Specialization : IEntity
{
    /// <summary>
    /// ID специализации.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название специализации.
    /// </summary>
    /// <example>Персональный тренинг</example>
    public required string Name { get; set; }

    /// <summary>
    /// Описание специализации.
    /// </summary>
    public string? Description { get; set; }
}