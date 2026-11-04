using System.ComponentModel.DataAnnotations;

namespace Museum.Contracts.Dto;

/// <summary>
/// Хранит данные для создания и изменения посетителя
/// </summary>
public sealed class VisitorEditDto
{
    /// <summary>
    /// ФИО посетителя
    /// </summary>
    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string FullName { get; init; } = string.Empty;

    /// <summary>
    /// Номер телефона посетителя
    /// </summary>
    [Required]
    [Phone]
    public string Phone { get; init; } = string.Empty;

    /// <summary>
    /// Дата рождения посетителя
    /// </summary>
    public DateTime BirthDate { get; init; }
}
