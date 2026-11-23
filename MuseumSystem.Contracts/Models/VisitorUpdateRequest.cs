using System.ComponentModel.DataAnnotations;

namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Данные для обновления посетителя
/// </summary>
public class VisitorUpdateRequest
{
    /// <summary>
    /// ФИО посетителя
    /// </summary>
    [Required]
    [MaxLength(200)]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Телефон посетителя
    /// </summary>
    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Дата рождения
    /// </summary>
    [Required]
    public DateOnly BirthDate { get; set; }
}