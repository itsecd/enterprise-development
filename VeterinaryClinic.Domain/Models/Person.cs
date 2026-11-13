namespace VeterinaryClinic.Domain.Models;

/// <summary>
/// Общие данные о человеке
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Отчество, если есть
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Контактный телефон
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}
