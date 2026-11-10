using VetClinic.Domain.Enums;

namespace VetClinic.Application.DTOs;

/// <summary>
/// Данные для создания питомца
/// </summary>
public class CreatePetDto
{
    /// <summary>
    /// Кличка питомца
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Вид животного
    /// </summary>
    public AnimalSpecies Species { get; set; }

    /// <summary>
    /// Идентификатор породы
    /// </summary>
    public int BreedId { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Вес в килограммах
    /// </summary>
    public decimal WeightKg { get; set; }

    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public int OwnerId { get; set; }
}