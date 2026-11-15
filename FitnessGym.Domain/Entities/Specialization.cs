using FitnessGym.Domain.Enums;

namespace FitnessGym.Domain.Entities;

/// <summary>
/// Справочная сущность, описывающая направление подготовки тренера
/// </summary>
public sealed class Specialization
{
    /// <summary>
    /// Уникальный идентификатор специализации
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Тип специализации
    /// </summary>
    public required string Name { get; set; }
}
