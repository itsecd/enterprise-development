using System.ComponentModel.DataAnnotations;
using VetClinic.Shared;

namespace VetClinic.Contracts.Dto.Specializations;

/// <summary>
/// Данные специализации врача для создания и изменения
/// </summary>
public class SpecializationEditDto
{
    /// <summary>
    /// Название специализации
    /// </summary>
    [Required]
    [MaxLength(100)]
    public required string Name { get; init; }

    /// <summary>
    /// Вид животного, с которым работает специализация, null если специализация общая
    /// </summary>
    [EnumDataType(typeof(AnimalSpecies))]
    public AnimalSpecies? FocusSpecies { get; init; }
}
