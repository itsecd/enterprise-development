using FitnessClub.Shared.Enums;
namespace FitnessClub.Domain.Entities;
/// <summary>
/// Класс для представления человека
/// </summary>
public abstract class Person : Entity
{
    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string PassportNumber { get; set; }
    /// <summary>
    /// Полное имя
    /// </summary>
    public required string FullName { get; set; }
    /// <summary>
    /// Пол
    /// </summary>
    public Gender Gender { get; set; }
    /// <summary>
    /// Дата рождения
    /// </summary>
    public required DateOnly BirthDate { get; set; }
    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? PhoneNumber { get; set; }
}