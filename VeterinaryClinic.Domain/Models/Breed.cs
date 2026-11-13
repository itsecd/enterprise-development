using VeterinaryClinic.Domain.Shared.Enums;

namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Порода животного
/// </summary>
public class Breed
{
    /// <summary>
    /// Идентификатор породы
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название породы
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Вид животного для этой породы
    /// </summary>
    public AnimalType AnimalType { get; set; }
}
