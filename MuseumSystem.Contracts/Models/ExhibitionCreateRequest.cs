using System.ComponentModel.DataAnnotations;
using MuseumSystem.Shared.Enums;

namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Данные для создания новой выставки
/// </summary>
public class ExhibitionCreateRequest
{
    /// <summary>
    /// Название выставки
    /// </summary>
    [Required(ErrorMessage = "Название обязательно")]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Тематика выставки
    /// </summary>
    [Required]
    public ExhibitionTheme Theme { get; set; }

    /// <summary>
    /// Номер зала
    /// </summary>
    [Range(1, 100)]
    public int HallNumber { get; set; }

    /// <summary>
    /// Дата начала выставки
    /// </summary>
    [Required]
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Дата окончания выставки
    /// </summary>
    [Required]
    public DateOnly EndDate { get; set; }
}