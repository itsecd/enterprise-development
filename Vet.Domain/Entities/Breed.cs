using Vet.Domain.Enums;

namespace Vet.Domain.Entities;

/// <summary>
/// Справочник пород
/// </summary>
public sealed class Breed
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Вид животного
    /// </summary>
    public required Species Species { get; set; }

    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }
}