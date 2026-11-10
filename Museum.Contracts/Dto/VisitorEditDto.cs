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
    public required string FullName { get; init; }

    /// <summary>
    /// Номер телефона посетителя
    /// </summary>
    [Required]
    [Phone]
    public required string Phone { get; init; }

    /// <summary>
    /// Дата рождения посетителя
    /// </summary>
    public DateTime BirthDate { get; init; }
}
