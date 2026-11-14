using Vet.Domain.Enums;

namespace Vet.Domain.Entities;

/// <summary>
/// Питомец
/// </summary>
public sealed class Pet
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Кличка
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Вид
    /// </summary>
    public required Species Species { get; set; }

    /// <summary>
    /// Порода
    /// </summary>
    public required Breed Breed { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly? BirthDate { get; set; }

    /// <summary>
    /// Масса тела
    /// </summary>
    public double? BodyWeight { get; set; }

    /// <summary>
    /// Владелец
    /// </summary>
    public required Owner Owner { get; set; }

    /// <summary>
    /// Список приёмов
    /// </summary>
    public List<Appointment> Appointments { get; set; } = [];
}