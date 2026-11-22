using System.ComponentModel.DataAnnotations;
using BeautySalon.Shared.Enums;

namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Услуга салона (для создания и изменения)
/// </summary>
public record ServiceEditDto
{
    /// <summary>
    /// Название услуги
    /// </summary>
    [Required, MaxLength(100)]
    public required string Name { get; init; }

    /// <summary>
    /// Категория услуги
    /// </summary>
    [EnumDataType(typeof(Category))]
    public required Category Category { get; init; }

    /// <summary>
    /// Цена услуги
    /// </summary>
    [Range(typeof(decimal), "0", "1000000")]
    public required decimal Price { get; init; }

    /// <summary>
    /// Продолжительность услуги (от 5 минут до 12 часов), например «01:30:00»
    /// </summary>
    [Range(typeof(TimeSpan), "00:05:00", "12:00:00")]
    public required TimeSpan Duration { get; init; }
}
