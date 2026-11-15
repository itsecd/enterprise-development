namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Данные посетителя для ответа
/// </summary>
public class VisitorResponse
{
    /// <summary>
    /// Уникальный идентификатор посетителя
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ФИО посетителя
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Телефон посетителя
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly BirthDate { get; set; }
}