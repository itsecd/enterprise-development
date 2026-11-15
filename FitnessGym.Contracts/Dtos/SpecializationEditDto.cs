using System.ComponentModel.DataAnnotations;

namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для создания специализации
/// </summary>
/// <param name="Name">Название специализации (обязательное поле, до 100 символов)</param>
public record SpecializationEditDto(
    [Required, MaxLength(100)]
    string Name);