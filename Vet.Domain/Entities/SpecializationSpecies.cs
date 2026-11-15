using Vet.Domain.Enums;

namespace Vet.Domain.Entities;

/// <summary>
/// Вид животного, относящийся к специализации врача
/// </summary>
public sealed class SpecializationSpecies
{
    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    public int SpecializationId { get; set; }

    /// <summary>
    /// Вид животного
    /// </summary>
    public Species Species { get; set; }
}