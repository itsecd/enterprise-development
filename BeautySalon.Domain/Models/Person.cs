using BeautySalon.Enums;

namespace BeautySalon.Domain.Models;

/// <summary>
/// Базовый класс для клиентов и мастеров
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Уникальный идентификатор персоны
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// ФИО
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    public required Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateOnly BirthDate { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Строковое представление
    /// </summary>
    public override string ToString() =>
    $"Full name: {FullName}, Gender: {Gender}, Birth date: {BirthDate:dd.MM.yyyy}, Phone: {PhoneNumber}";
}