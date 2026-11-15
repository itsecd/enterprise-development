namespace Vet.Domain.Entities;

/// <summary>
/// Справочник специализаций врачей
/// </summary>
public sealed class Specialization
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название специальности
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Список видов животных, которых может принять врач данной специализации
    /// </summary>
    public List<SpecializationSpecies> Species { get; set; } = [];
}