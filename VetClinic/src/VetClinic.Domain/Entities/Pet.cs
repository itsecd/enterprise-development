using VetClinic.Domain.Enums;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Domain.Entities;

/// <summary>
/// Питомец
/// </summary>
public class Pet : IEntity
{
    /// <summary>
    /// Уникальный идентификатор питомца
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Кличка животного
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Биологический вид животного
    /// </summary>
    public AnimalSpecies Species { get; set; }

    /// <summary>
    /// Идентификатор породы
    /// </summary>
    public int BreedId { get; set; }

    /// <summary>
    /// Порода
    /// </summary>
    public Breed? Breed { get; set; }

    /// <summary>
    /// Дата рождения питомца
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Вес питомца в килограммах
    /// </summary>
    public decimal WeightKg { get; set; }

    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public int OwnerId { get; set; }

    /// <summary>
    /// Владельец
    /// </summary>
    public Owner? Owner { get; set; }

    /// <summary>
    /// Список записей на прием для данного питомца
    /// </summary>
    public List<Appointment> Appointments { get; set; } = [];
}