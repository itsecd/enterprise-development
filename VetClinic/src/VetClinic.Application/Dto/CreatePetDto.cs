using System.ComponentModel.DataAnnotations;
using VetClinic.Domain.Enums;

namespace VetClinic.Application.Dto;

/// <summary>
/// Данные для создания питомца
/// </summary>
public class CreatePetDto
{
    /// <summary>
    /// Кличка питомца
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 1)]
    public required string Name { get; set; }

    /// <summary>
    /// Вид животного
    /// </summary>
    [Required]
    public AnimalSpecies? Species { get; set; }

    /// <summary>
    /// Идентификатор породы
    /// </summary>
    [Range(1, int.MaxValue)]
    public int BreedId { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    [Required]
    public DateOnly? BirthDate { get; set; }

    /// <summary>
    /// Вес в килограммах
    /// </summary>
    [Range(0.01, 1000.0)]
    public decimal WeightKg { get; set; }

    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    [Range(1, int.MaxValue)]
    public int OwnerId { get; set; }
}