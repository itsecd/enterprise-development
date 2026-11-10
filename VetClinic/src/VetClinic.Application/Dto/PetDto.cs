using VetClinic.Domain.Enums;

namespace VetClinic.Application.DTOs;

/// <summary>
/// DTO питомца
/// </summary>
public class PetDto
{
    /// <summary>
    /// Идентификатор питомца
    /// </summary>
    public int Id { get; set; }

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
    /// Название породы
    /// </summary>
    public string? BreedName { get; set; }

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

    /// <summary>
    /// ФИО владельца
    /// </summary>
    public string? OwnerName { get; set; }
}