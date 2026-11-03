using VetClinic.Shared;

namespace VetClinic.Domain.Models;

/// <summary>
/// Питомец
/// </summary>
public class Pet
{
    /// <summary>
    /// Уникальный идентификатор питомца
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Кличка питомца
    /// </summary>
    public required string Nickname { get; set; }

    /// <summary>
    /// Биологический вид животного
    /// </summary>
    public AnimalSpecies Species { get; set; }

    /// <summary>
    /// Порода
    /// </summary>
    public Breed? Breed { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Вес в килограммах
    /// </summary>
    public double Weight { get; set; }

    /// <summary>
    /// Владелец питомца
    /// </summary>
    public Owner? Owner { get; set; }
}
