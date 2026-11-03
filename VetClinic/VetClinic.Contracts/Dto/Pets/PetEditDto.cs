using System.ComponentModel.DataAnnotations;
using VetClinic.Shared;

namespace VetClinic.Contracts.Dto.Pets;

/// <summary>
/// Данные питомца для создания и изменения
/// </summary>
public class PetEditDto
{
    /// <summary>
    /// Кличка питомца
    /// </summary>
    [Required]
    [MaxLength(50)]
    public required string Nickname { get; init; }

    /// <summary>
    /// Биологический вид животного
    /// </summary>
    [Required]
    [EnumDataType(typeof(AnimalSpecies))]
    public required AnimalSpecies Species { get; init; }

    /// <summary>
    /// Идентификатор породы, null если порода не указана
    /// </summary>
    [Range(1, int.MaxValue)]
    public int? BreedId { get; init; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    [Required]
    public required DateTime BirthDate { get; init; }

    /// <summary>
    /// Вес в килограммах
    /// </summary>
    [Required]
    [Range(0.01, 1000)]
    public required double Weight { get; init; }

    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public required int OwnerId { get; init; }
}
