using System.ComponentModel.DataAnnotations;

namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для создания специализации
/// </summary>
public record SpecializationEditDto(
    [Required, MaxLength(100)]
    string Name);