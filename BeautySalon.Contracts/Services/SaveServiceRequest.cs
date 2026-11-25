using System.ComponentModel.DataAnnotations;

namespace BeautySalon.Contracts.Services;

/// <summary>
/// Данные услуги для создания или обновления
/// </summary>
public sealed class SaveServiceRequest
{
    /// <summary>
    /// Название услуги
    /// </summary>
    [Required]
    [StringLength(150)]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Категория услуги
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Category { get; init; } = string.Empty;

    /// <summary>
    /// Стоимость в рублях
    /// </summary>
    [Required]
    [Range(typeof(decimal), "0", "1000000", ErrorMessage = "Стоимость должна быть от 0 до 1 000 000 рублей")]
    public decimal? Price { get; init; }

    /// <summary>
    /// Продолжительность в минутах
    /// </summary>
    [Required]
    [Range(1, 1440, ErrorMessage = "Длительность должна быть от 1 до 1440 минут")]
    public int? DurationMinutes { get; init; }
}